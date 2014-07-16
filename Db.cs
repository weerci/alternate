using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CoreCommon;
using System.Security.Principal;

namespace Alternative
{

    /// <summary>
    /// Класс предоставляющий доступ к базе данных Mssql. Синглетон. 
    /// Перед использованием необходимо инициализировать соедининене с базой: WFSql.Db.Connect(Data Source, dataBase, User ID, Password);
    /// </summary>
    public sealed class WFSql : IDisposable
    {
        
        /// <summary>
        /// Получение экзепляра класса для работы с БД
        /// </summary>
        public static WFSql DB
        {
            get
            {
                if (_db == null)
                {
                    lock (_syncRoot)
                    {
                        if (_db == null)
                            _db = new WFSql();
                    }
                }
                return _db;
            }
        }

        #region Methods

        /// <summary>
        /// Соединение с базой данных MSSQL с проверкой подлинности MSSQL
        /// </summary>
        /// <param name="dataSource">Сервер базы данных</param>
        /// <param name="dataBase">Имя базы данных</param>
        /// <param name="connectedUser">Логин пользователя</param>
        /// <param name="password">Пароль</param>
        public void Connect(string dataSource, string dataBase, string connectedUser, string password)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = dataSource;
                builder.InitialCatalog = dataBase;
                //builder.IntegratedSecurity = true;
                builder.UserID = connectedUser;
                builder.Password = password;
                builder.MultipleActiveResultSets = true;
                if (this.IsActive())
                    this.Close();
                _connect = new SqlConnection(builder.ConnectionString);
                _connect.Open();

            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        
        /// <summary>
        /// Соединение с базой данных MSSQL с проверкой подлинности Windows
        /// </summary>
        /// <param name="serverName">Сервер базы данных</param>
        /// <param name="dataBase">Имя базы данных</param>
        public void ConnectWin(string serverName, string dataBase)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = serverName;
                builder.InitialCatalog = dataBase;
                builder.UserID = WindowsIdentity.GetCurrent().Name;
                builder.IntegratedSecurity = true;
                builder.MultipleActiveResultSets = true;
                if (this.IsActive())
                    this.Close();
                _connect = new SqlConnection(builder.ConnectionString);
                _connect.Open();
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        
        /// <summary>
        /// Закрытие соединения с базой данных
        /// </summary>
        public void Close()
        {
            if (_connect != null)
            {
//                SqlWork.SetUserNumberConnection(_loginOuter, 0);
                _connect.Close();
            }
        }
        
        /// <summary>
        /// Проверка активности соединения с базой данных
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return (_connect != null) && (_connect.State != System.Data.ConnectionState.Closed);
        }
        
        /// <summary>
        /// Старт новой транзакции
        /// </summary>
        public void StartTransaction()
        {
            try
            {
                _trans = _connect.BeginTransaction();
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltDb(EDbErrors));
            }
        }
        
        /// <summary>
        /// Проверка на наличие активной транзакции
        /// </summary>
        /// <returns></returns>
        public bool InTransaction()
        {
            return _trans != null;
        }
        
        /// <summary>
        /// Поддтверждение транзакции
        /// </summary>
        public void Commit()
        {
            if (_trans == null)
                throw EAlternate.CreateException(new EAltDb("WfOracle.Db._trans is null"));
            
            _trans.Commit();
            _trans.Dispose();
            _trans = null;
        }
        
        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void Rollback()
        {
            if (_trans == null)
                throw EAlternate.CreateException(new EAltDb("WfOracle.Db._trans is null"));

            _trans.Rollback();
            _trans.Dispose();
            _trans = null;
        }
        
        /// <summary>
        /// Добавление входного параметра
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="sqlType">Тип параметра</param>
        /// <param name="value">Значение параметра</param>
        /// <param name="command">Инструкция T-SQL или хранимая процедура</param>
        /// <param name="IsNull">Определяет, может ли параметр принимать значение Null</param>
        public void AddInParameter(string name, SqlDbType sqlType, object value, SqlCommand command, bool IsNull)
        {
            SqlParameter prm = new SqlParameter(name, sqlType);
            prm.IsNullable = IsNull;
            if (!IsNull && value == null)
                throw EAlternate.CreateException(new EAltDb(ErrorMsg.EDbNullParam));
            if (IsNull && value == null)
                prm.Value = DBNull.Value;
            else
                prm.Value = value;
            command.Parameters.Add(prm);
        }
        
        /// <summary>
        /// Добавление выходного параметра
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="sqlType">Тип параметра</param>
        /// <param name="command">Инструкция T-SQL или хранимая процедура</param>
        /// <returns>Значение выходного параметра</returns>
        public SqlParameter AddOutParameter(string name, SqlDbType sqlType, SqlCommand command, int size = 0)
        {
            SqlParameter prm = new SqlParameter(name, sqlType);
            prm.Direction = ParameterDirection.Output;
            if (size != 0)
                prm.Size = size;
            command.Parameters.Add(prm);
            return prm;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает текущую коннекцию
        /// </summary>
        public SqlConnection SqlConnection
        {
            get { return _connect; }
        }
        
        /// <summary>
        /// Возвращает текущую транзакцию
        /// </summary>
        public SqlTransaction SqlTransaction
        {
            get { return _trans; }
        }

        /// <summary>
        /// Если коннекция активна, возвращает наименование сервера, если нет ""
        /// </summary>
        public string ServerName { get { return _connect != null && _connect.State == ConnectionState.Open? _connect.DataSource: "";} }
        
        /// <summary>
        /// Если коннекция активна, возвращает имя базы данных, если нет ""
        /// </summary>
        public string DbName { get { return _connect != null && _connect.State == ConnectionState.Open ? _connect.Database : ""; } }
        
        /// <summary>
        /// Если коннекция активна, возвращает логин пользователя, если нет ""
        /// </summary>
        public string Login 
        { 
            get
            {
                if (_connect != null && _connect.State == ConnectionState.Open)
                {
                    foreach (string s in _connect.ConnectionString.Split(';'))
                    { 
                        int i = s.IndexOf("User ID=");
                        if (i > -1) return s.Substring(i + 8); 
                    }
                }
                return "";
            } 
        }

        /// <summary>
        /// Если коннекция активана, возвращает имя компьютера, с которого приконнекался пользователь, если нет ""
        /// </summary>
        public string UserHost { get { return _connect != null && _connect.State == ConnectionState.Open ? _connect.WorkstationId : ""; } }

        /// <summary>
        /// Имя текущего пользователя базы из таблицы users
        /// </summary>
        public string UserLogin;

        /// <summary>
        /// Идентификатор коннекции к базе данных
        /// </summary>
        public string UserConnect_id;

        #endregion

        #region Private

        private static volatile WFSql _db;
        private static object _syncRoot = new Object();
        private SqlTransaction _trans;
        private SqlConnection _connect;

        // Constructors
        private WFSql(){ }

        //Dispose
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connect.Dispose();
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Errors

        const string EDbErrors = "Ошибка обращения к базе данных.";

        #endregion
    }

    /// <summary>
    /// Класс представляет простейшие методы для работы с базой данных
    /// </summary>
    public sealed class SqlWork
    {
        /// <summary>
        /// Простой запрос без параметров, данные сохраяются в dataTable
        /// </summary>
        /// <param name="sql">Текст запроса</param>
        /// <param name="dataTable">DataTable, в котором сохраняются полученные результаты</param>
        /// <param name="id">Уникальный идентификатор</param>
        public static void SqlSimple(string sql, DataTable dataTable, string id)
        {
            SqlCommand cmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dataTable.Clear();
            da.Fill(dataTable);
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[id] };
        }
        
        /// <summary>
        /// Выборка по выбранному id
        /// </summary>
        /// <param name="sql">Текст запроса</param>
        /// <param name="dataTable">DataTable, в котором сохраняются полученные результаты</param>
        /// <param name="id">Id, по которому получаются данные</param>
        /// <param name="value"></param>
        public static void SqlById(string sql, DataTable dataTable, string id, int value)
        {
            SqlCommand cmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
            WFSql.DB.AddInParameter(id, SqlDbType.Int, value, cmd, false);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dataTable.Clear();
            da.Fill(dataTable);
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[id] };
        }
        
        /// <summary>
        /// Удаление записей из базы по переданным id
        /// </summary>
        /// <param name="sql">Текст запроса</param>
        /// <param name="ids">Массив id</param>
        /// <param name="id">Уникальный идентификатор</param>
        public static void SqlDeleteSimple(string sql, int[] ids, string id)
        {
            SqlCommand cmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
            cmd.CommandType = CommandType.Text;

            foreach (int i in ids)
            {
                cmd.Parameters.Clear();
                WFSql.DB.AddInParameter(id, SqlDbType.Int, i, cmd, false);
                cmd.ExecuteNonQuery();
            }
        }
       
        public static void SetUserNumberConnection(string user, int toSave = 1)
        {
            string sql = "adm.set_connect_id";
            SqlParameter connect_id;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@user_name", SqlDbType.NVarChar, user, sqlCmd, false);
                WFSql.DB.AddInParameter("@is_null", SqlDbType.NVarChar, toSave, sqlCmd, false);
                connect_id = WFSql.DB.AddOutParameter("@connection_id", SqlDbType.UniqueIdentifier, sqlCmd);

                sqlCmd.ExecuteNonQuery();
                
                WFSql.DB.Commit();

                WFSql.DB.UserLogin = user;
                WFSql.DB.UserConnect_id = connect_id.Value.ToString();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err);
            }

        }

        // Constructors
        private SqlWork() { }

       
    }

 }
