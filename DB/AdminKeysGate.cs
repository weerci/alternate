using CoreCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Alternative.DB
{
    /// <summary>
    /// Класс предоставляет доступ к функциям администрирования криптографии
    /// </summary>
    public class AdminCriptoGate
    {
        /// <summary>
        /// Задает значение публичного ключа для типографии
        /// </summary>
        /// <param name="name">Имя файла сохранения для ключа</param>
        /// <param name="path">Путь к создаваемому ключу</param>
        /// <param name="publicKey">Публичный ключ</param>
        public void SetPressAsyncKey(string name, string path, out string publicKey)
        {
            string sql = "pla.sc_create_pres_async_key";
            SqlParameter prmRes;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);
                prmRes = WFSql.DB.AddOutParameter("@key", SqlDbType.VarChar, sqlCmd);
                prmRes.Size = 512;

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch 
            {
                WFSql.DB.Rollback();
                throw;
            }

            publicKey = prmRes.Value.ToString();
        }
        /// <summary>
        /// Возвращает значение публичного ключа типографии
        /// </summary>
        /// <param name="name">Имя ключа типографии</param>
        /// <param name="path">Путь к ключу типографии</param>
        /// <returns>Значение публичного ключа</returns>
        public string GetPressPublicKey(out string name, out string path)
        {
            string sql = "pla.sc_get_press_async_key";
            SqlParameter resName, resPath, resKey;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                resName = WFSql.DB.AddOutParameter("@name", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resPath = WFSql.DB.AddOutParameter("@path", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resKey = WFSql.DB.AddOutParameter("@key", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
            name = resName.Value.ToString();
            path = resPath.Value.ToString();
            return resKey.Value.ToString();
        }
        /// <summary>
        /// Редактирует значения имени и пути внешнего ключа
        /// </summary>
        /// <param name="name">Имя файла публичного ключа</param>
        /// <param name="path">Путь к файлу</param>
        public void EditPressKey(string name, string path)
        {
            string sql = "pla.sc_edit_press_key";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }
        
        /// <summary>
        /// Задает знаение публичного ключа для платежной системы
        /// </summary>
        /// <param name="name">Имя файла сохранения для ключа</param>
        /// <param name="path">Путь к созадваемому ключу</param>
        /// <param name="publicKey">Публичный ключ</param>
        public void SetSaleAsyncKey(string name, string path, out string publicKey)
        {
            string sql = "pla.sc_create_sale_async_key";
            SqlParameter prmRes;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);
                prmRes = WFSql.DB.AddOutParameter("@key", SqlDbType.VarChar, sqlCmd);
                prmRes.Size = 512;

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }

            publicKey = prmRes.Value.ToString();
        
        }
        /// <summary>
        /// Возвращает значение публичного ключа платежной системы
        /// </summary>
        /// <param name="name">Имя ключа платежной системы</param>
        /// <param name="path">Путь к ключу платежной системы</param>
        /// <returns></returns>
        public string GetSalePublicKey(out string name, out string path)
        {
            string sql = "pla.sc_get_sale_async_key";
            SqlParameter resName, resPath, resKey;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                resName = WFSql.DB.AddOutParameter("@name", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resPath = WFSql.DB.AddOutParameter("@path", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resKey = WFSql.DB.AddOutParameter("@key", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
            name = resName.Value.ToString();
            path = resPath.Value.ToString();
            return resKey.Value.ToString();
        }
        /// <summary>
        /// Редактирует значения имени и пути внешнего ключа платежной системы
        /// </summary>
        /// <param name="name">Имя файла публичного ключа</param>
        /// <param name="path">Путь к файлу</param>
        public void EditSaleKey(string name, string path)
        {
            string sql = "pla.sc_edit_sale_async_key";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Задает знаение ключевого слова для алгоритма md5
        /// </summary>
        /// <param name="name">Имя файла сохранения для ключевого слова</param>
        /// <param name="path">Путь к созадваемому слову</param>
        /// <param name="publicKey">Ключевое слово</param>
        public void SetHashWord(string name, string path, out string publicKey)
        {
            string sql = "pla.sc_create_md5_word";
            SqlParameter prmRes;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);
                prmRes = WFSql.DB.AddOutParameter("@word", SqlDbType.VarChar, sqlCmd);
                prmRes.Size = 512;

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }

            publicKey = prmRes.Value.ToString();

        }
        /// <summary>
        /// Возвращает значение ключевого слова
        /// </summary>
        /// <param name="name">Имя файла ключевого слова</param>
        /// <param name="path">Путь к файлу ключевого слова</param>
        /// <returns></returns>
        public string GetHashWord(out string name, out string path)
        {
            string sql = "pla.sc_get_md5_word";
            SqlParameter resName, resPath, resWord;

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                resName = WFSql.DB.AddOutParameter("@name", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resPath = WFSql.DB.AddOutParameter("@path", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);
                resWord = WFSql.DB.AddOutParameter("@word", SqlDbType.VarChar, sqlCmd, AltCommon.VARCHAR_SIZE_1);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
            name = resName.Value.ToString();
            path = resPath.Value.ToString();
            return resWord.Value.ToString();
        }
        /// <summary>
        /// Редактирует значения имени и пути ключевого слова
        /// </summary>
        /// <param name="name">Имя файла ключевого слова</param>
        /// <param name="path">Путь к файлу ключевого слова</param>
        public void EditHashWord(string name, string path)
        {
            string sql = "pla.sc_edit_md5_word";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);
                WFSql.DB.AddInParameter("@path", SqlDbType.VarChar, path, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }

    }
}
