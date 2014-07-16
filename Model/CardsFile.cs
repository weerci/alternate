using Alternative.DB;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative.Model
{
    /// <summary>
    /// Класс обеспечивает функционал проверки и загрузки файла карточек 
    /// </summary>
    public class CardsFile
    {
        #region Constructors

        public CardsFile(string path, Project pr)
        {
            _path = path;
            _pr = pr;
            PrepareDT();
            Open(_path);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает таблицу содержащую файл с карточками
        /// </summary>
        public DataTable DT { get { return _dt; } }

        /// <summary>
        /// Флаг, показывающий состояние набора данных: true - проверен, false - не проверен
        /// </summary>
        public bool IsChecked { get { return _isChecked; } }

        /// <summary>
        /// Возвращает лог обработки файла карточек
        /// </summary>
        public void ShowResultCheck()
        {
            File.WriteAllLines(Path.GetTempPath() + "\\CardLog.txt", _logCheck.ToArray());
            Process.Start("notepad.exe", Path.GetTempPath() + "\\CardLog.txt");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Загружает проверенный файл в базу данных
        /// </summary>
        public void LoadToDb()
        {
            List<Card> lc = new List<Card>();
            foreach (DataRow item in _dt.Rows)
            {
                Card card = new Card();
                card.N_NUMBER = Convert.ToInt64(item["NUM"], CultureInfo.InvariantCulture);
                card.S_PIN = item["PIN"].ToString().Replace(" ", "");
                card.M_FACE = Convert.ToDouble(item["NOM"], CultureInfo.InvariantCulture);
                card.D_FROM = Convert.ToDateTime(item["DATE_S"]);
                card.D_END =  Convert.ToDateTime(item["DATE_E"]);
                card.F_PROJECT = _pr.LINK;
                card.F_STATUS = 1;

                lc.Add(card);
            }
            
            try
            {
                CardGate.Insert(lc);
            }
            catch (Exception err)
            {
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53206")
                    throw new EAltMessage(err.Message);
                else
                    throw EAlternate.CreateException(err, new EAltModel(ELoadFromFile));
            }
        }

        /// <summary>
        /// Проверяет загружаемый файл на соответствие критериям правильного файла загрузки карточек
        /// </summary>
        public bool Check(bool showResult = true)
        {
            _logCheck.AddLog("Создание log-файла обработки файла с карточками");
            _logCheck.AddLog("Путь к файлу:" + _path);

            CheckFileColumn();
            CheckData();
            CheckUniqueDTfromBase();


            if (_hasError)
            {
                _logCheck.AddLog("Обработка файла завершилась с ошибками.");
                _isChecked = false;
                ShowResultCheck();
                return false;
            }
            else
            {
                _logCheck.AddLog("Файл успешно обработан. Ошибок не обнаружено.");
                if (showResult) ShowResultCheck();
                return true;
            }
        }

        /// <summary>
        /// Открывается новый файл карточек
        /// </summary>
        /// <param name="path">Путь к файлу карточек</param>
        public void Open(string path)
        {
            try
            {
                _dirtyFile = File.ReadAllLines(path).ToList();
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        
        #endregion

        #region Helper

        /// <summary>
        /// Проверка наличия в файле имен столбцов прописанных в настройках импорта файла  Properties.Settings.Default.CardColumns
        /// </summary>
        private void CheckFileColumn()
        {
            try
            {
                _logCheck.AddLog("Проверка наличия всех необходимых столбцов.");

                string[] res = _dt.Columns.Cast<DataColumn>().Select(n => n.ColumnName.ToUpper()).Except(_dirtyFile[0].ToUpper().Split(';')).ToArray();
                if (res.Count() > 0)
                {
                    _logCheck.AddLog(String.Format(ErrorMsg.EPrFileCardHeader, res.Aggregate((n, next) => n + "," + next)), 1);
                    _hasError = true;
                }
                else
                    _logCheck.AddLog("Результат проверки соответствия столбцов: успешно.");
            }
            catch (Exception err)
            {
                _hasError = true;
                _logCheck.AddLog("Ошибка проверки соответствия столбцов списка."+ Environment.NewLine + err.Message);
            }
        }
        /// <summary>
        /// Проверка данных из файла карточек
        /// </summary>
        private void CheckData()
        {
            try
            {
                bool hasError = false;
                _logCheck.AddLog("Проверка данных в строках файла карточек.");
                List<string> s = _dirtyFile[0].ToUpper().Split(';').ToList();
                _dirtyFile.RemoveAt(0);
                int indexStr = 1;
                foreach (string rowDirty in _dirtyFile)
                {
                    indexStr++;
                    string[] rowDirtyColumn = rowDirty.ToUpper().Split(';');
                    DataRow dr = _dt.NewRow();

                    #region Создается новая строка

                    foreach (DataColumn rowDt in _dt.Columns)
                    {
                        int indexColumn = s.IndexOf(rowDt.ColumnName.ToUpper());
                        if (indexColumn >= 0)
                            try
                            {
                                dr[rowDt.ColumnName] = rowDirtyColumn[indexColumn];
                            }
                            catch (Exception err)
                            {
                                hasError = true;
                                _logCheck.AddLog(err.Message, indexStr);
                            }
                        else
                        {
                            hasError = true;
                            _logCheck.AddLog(String.Format("Не найден столбец {0} в строке {1}", rowDt.ColumnName, rowDirty));
                        }
                    }

                    #endregion

                    #region Добавление строки в таблицу

                    try
                    {
                        _dt.Rows.Add(dr);
                    }
                    catch (Exception err)
                    {
                        hasError = true;
                        _logCheck.AddLog(err.Message, indexStr);
                    }

                    #endregion
                }

                if (!hasError)
                    _logCheck.AddLog("Проверка данных. Результат: успешно.");
                else
                {
                    _hasError = hasError;
                    _logCheck.AddLog("Ошибка проверки данных в строках файла карточек.");
                }
            }
            catch (Exception err)
            {
                _logCheck.AddLog(err.Message);
                _hasError = true;
                _logCheck.AddLog("Ошибка проверки данных в строках файла карточек.");
            }
        }
        /// <summary>
        /// Проверяется уникальность номера карточки для данных в базе
        /// </summary>
        private void CheckUniqueDTfromBase()
        {
            try
            {
                _logCheck.AddLog("Проверка дубликатов номеров карточек в базе данных.");

                DataTable dt = CardGate.List(_pr.LINK);
                string[] s1 = dt.Rows.Cast<DataRow>().Select(n => n["N_NUMBER"].ToString()).ToArray();
                string[] s2 = _dt.Rows.Cast<DataRow>().Select(n => n["NUM"].ToString()).ToArray();

                string[] arr = s1.Intersect(s2).ToArray();
                if (arr.Count() > 0)
                    throw new Exception(String.Format("Найдены дубликаты в базе данных, повторяющиеся номера: {0}", arr.Aggregate((n, next)=> n + "," + next)));
                _logCheck.AddLog("Проверка дубликатов номеров карточек в базе данных. Результат: успешно.");
            }
            catch (Exception err)
            {
                _logCheck.AddLog(err.Message);
                _hasError = true;
                _logCheck.AddLog("Ошибка проверки дубликатов номеров карточек в базе данных. Найдены дубликаты");
            }
        }
        /// <summary>
        /// Подготавливается таблица для заливки данных из файла
        /// </summary>
        private void PrepareDT()
        {
            _dt.Constraints.Clear();
            _dt.Columns.Clear();

            foreach (string item in Properties.Settings.Default.CardColumns)
            {
                string[] s = item.Split(';').ToArray();
                DataColumn dc = _dt.Columns.Add(s[0], System.Type.GetType(s[1]));
                dc.Unique = s[2] == "unique";
            }
        }

        #endregion

        #region Fields


        private string _path; // Путь к разбираемому файлу
        private DataTable _dt = new DataTable(); 
        private bool _isChecked = false; 
        private List<string> _logCheck = new List<string>(); // лог проверки файла карточек
        private List<string> _dirtyFile = new List<string>(); // Набор строк, содержащий не обработанную выгрузку из предложенного карточек
        private bool _hasError = false;
        private Project _pr;

        #endregion

        #region Errors

        const string ELoadFromFile = "Не удалось загрузить данные из файла.";

        #endregion
    }

}
