using CoreCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alternative.DB;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Alternative.Model
{
    /// <summary>
    /// Класс инкапсулирующий работу со справочниками
    /// </summary>
    public class Dictionary
    {
        /// <summary>
        /// Конструктор создает экземпляр справочника в соответствии с его типом
        /// </summary>
        /// <param name="dicType">Тип справочника</param>
        public Dictionary(string dicType)
        {
            _dicType = dicType;
        }
        /// <summary>
        /// Данные загружаются в набор данных DT и связываются с переданным DataGridView
        /// </summary>
        public void Open(DataGridView dgv)
        {
            Open();
            dgv.DataSource = _dt;
            SetCaptionColumn(dgv);

        }
        /// <summary>
        /// Загружаются данные
        /// </summary>
        public void Open()
        {
            try
            {
                switch (_dicType)
                {
                    case DicType.DESIGN:
                        _gate.Open(DicType.DESIGN, _dt);
                        break;
                    case DicType.PAYMENTS:
                        _gate.Open(DicType.PAYMENTS, _dt);
                        break;
                    case DicType.JURISTIC:
                        _gate.Open(DicType.JURISTIC, _dt);
                        break;
                    case DicType.STATUS:
                        _gate.Open(DicType.STATUS, _dt);
                        break;
                    case DicType.OPERATION:
                        _gate.Open(DicType.OPERATION, _dt);
                        break;
                    case DicType.USERS:
                        _gate.Open(DicType.USERS, _dt);
                        break;
                    case DicType.LOGINS:
                        _gate.Open(DicType.LOGINS, _dt);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EDcOpenDictionary));
            }
        }
        /// <summary>
        /// Свойство возвращает набор данных справочника
        /// </summary>
        public DataTable DT
        {
            get { return _dt; }

        }
   
        /// <summary>
        /// Добавление элементов в справочник платежных систем
        /// </summary>
        /// <param name="dt">Добавляемые элементы</param>
        public static void InsertPayment(DataTable dt)
        {
            try
            {
                new DictionaryGate().InsertPayment(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Обновление данных в справочнике платежных систем
        /// </summary>
        /// <param name="dt">Обновляемые элементы</param>
        public static void UpdatePayment(DataTable dt)
        {
            try
            {
                new DictionaryGate().UpdatePayment(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Удаление элементов из спрвочника платежных систем
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void DeletePayment(DataTable dt) 
        {
            try
            {
                new DictionaryGate().DeletePayment(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }

        /// <summary>
        /// Добавление элементов в справочник юр.лиц
        /// </summary>
        /// <param name="dt">Добавляемые элементы</param>
        public static void InsertJuristic(DataTable dt)
        {
            try
            {
                new DictionaryGate().InsertJuristic(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Обновление данных в справочнике юр.лиц
        /// </summary>
        /// <param name="dt">Обновляемые элементы</param>
        public static void UpdateJuristic(DataTable dt)
        {
            try
            {
                new DictionaryGate().UpdateJuristic(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }      
        }
        /// <summary>
        /// Удаление элементов из спрвочника платежных систем
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void DeleteJuristic(DataTable dt)
        {
            try
            {
                new DictionaryGate().DeleteJuristic(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        
        /// <summary>
        /// Удаление элементов из спрвочника статусов
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void InsertStatus(DataTable dt) 
        {
            try
            {
                new DictionaryGate().InsertStatus(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Обновление данных в справочнике статусов
        /// </summary>
        /// <param name="dt">Обновляемые элементы</param>
        public static void UpdateStatus(DataTable dt) 
        {
            try
            {
                new DictionaryGate().UpdateStatus(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }    
        }
        /// <summary>
        /// Удаление элементов из спрвочника статусов
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void DeleteStatus(DataTable dt) 
        {
            try
            {
                new DictionaryGate().DeleteStatus(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }

        /// <summary>
        /// Удаление элементов из спрвочника типов операций
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void InsertOperation(DataTable dt) 
        {
            try
            {
                new DictionaryGate().InsertOperation(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Обновление данных в справочнике типов операций
        /// </summary>
        /// <param name="dt">Обновляемые элементы</param>
        public static void UpdateOperation(DataTable dt) 
        {
            try
            {
                new DictionaryGate().UpdateOperation(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Удаление элементов из спрвочника типов операций
        /// </summary>
        /// <param name="dt">Удаляемые элементы</param>
        public static void DeleteOperation(DataTable dt) 
        {
            try
            {
                new DictionaryGate().DeleteOperation(dt);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }

        private string _dicType;
        private DataTable _dt = new DataTable();
        private DictionaryGate _gate = new DictionaryGate();
        private void SetCaptionColumn(DataGridView dgv)
        {
            if (dgv.Columns.Contains("LINK"))
                dgv.Columns["LINK"].Visible = false;
            if (dgv.Columns.Contains("S_NAME"))
                dgv.Columns["S_NAME"].HeaderText = CommonText.ClmS_NAME;
            if (dgv.Columns.Contains("S_PREF"))
                dgv.Columns["S_PREF"].HeaderText = CommonText.ClmS_PREF;
            if (dgv.Columns.Contains("S_PASS"))
                dgv.Columns["S_PASS"].Visible = false;
            if (dgv.Columns.Contains("F_LOGIN"))
                dgv.Columns["F_LOGIN"].Visible = false;
            if (dgv.Columns.Contains("F_LOGIN"))
                dgv.Columns["LOGIN_NAME"].HeaderText = CommonText.ClmS_LOGIN;
        }
    }

    public class DicType
    {
        public const string DESIGN = "design";
        public const string PAYMENTS = "payments";
        public const string JURISTIC = "juristic";
        public const string STATUS = "status";
        public const string OPERATION = "oper_types";
        public const string USERS = "users";
        public const string LOGINS = "logins";
    }

    public class Design
    {
        
        #region Properties
        /// <summary>
        /// Уникальный идентификатор проекта
        /// </summary>
        public int LINK { get;  set; }
        /// <summary>
        /// Имя проекта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дизайн лицевой стороны
        /// </summary>
        public Image Facade { get; set; }
        /// <summary>
        /// Дизайн обратной стороны
        /// </summary>
        public Image Outside { get; set; }
        /// <summary>
        /// Формат файла лицевой стороны
        /// </summary>
        public ImageFormat FacedeFormat { get { return formatFromPath(PathToFacedeFile); } }
        /// <summary>
        /// Формат файла оборотной стороны
        /// </summary>
        public ImageFormat OutsideFormat { get { return formatFromPath(PathToOutsideFile); } }
        /// <summary>
        /// Путь к файлу оборотной стороны дизайна
        /// </summary>
        public string PathToFacedeFile { get; set; }
        /// <summary>
        /// Путь к файлу оборотной стороны дизайна
        /// </summary>
        public string PathToOutsideFile { get; set; }

        #endregion

        #region Method
        /// <summary>
        /// Сохранение проекта
        /// </summary>
        public int Save()
        {
            try
            {
                if (LINK == 0)
                    return new DictionaryGate().InsertDesign(this);
                else
                    return new DictionaryGate().UpdateDesign(this);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Удаление элементов справочника дизайнов
        /// </summary>
        /// <param name="dt">Список удаляемых элементов</param>
        public static void Delete(int[] idx)
        {
            try
            {
                new DictionaryGate().DeleteDesign(idx);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        public static Design LoadById (int id)
        {
            DataTable dt = new DictionaryGate().DesignLoad(id);
            Design ds = new Design();
            ds.LINK = Convert.ToInt32(dt.Rows[0]["LINK"], CultureInfo.InvariantCulture);
            ds.Name = dt.Rows[0]["S_NAME"].ToString();
            ds.PathToFacedeFile = "c:\\null."+dt.Rows[0]["S_F_EXT"].ToString();
            ds.PathToOutsideFile = "c:\\null." + dt.Rows[0]["S_O_EXT"].ToString();
            ds.Facade = Image.FromStream(new MemoryStream((byte[])dt.Rows[0]["F_FACADE"]));
            ds.Outside = Image.FromStream(new MemoryStream((byte[])dt.Rows[0]["F_OUTSIDE"]));

            return ds;
        }

        #endregion

        #region Helper

        public static byte[] ImageToStream(Image img, ImageFormat imgFormat, out int size)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] b= stream.ToArray();
            size = b.Length;
            return b;
        }
        public static ImageFormat formatFromPath(string path)
        {
            switch (Path.GetExtension(path).ToUpper())
            {
                case ".BMP":
                    return ImageFormat.Bmp;
                case ".EMF":
                    return ImageFormat.Emf;
                case ".EXIF":
                    return ImageFormat.Exif;
                case ".GIF":
                    return ImageFormat.Gif;
                case ".ICO":
                    return ImageFormat.Icon;
                case ".JPEG":
                case ".JPG":
                    return ImageFormat.Jpeg;
                case ".PNG":
                    return ImageFormat.Png;
                case ".TIFF":
                    return ImageFormat.Tiff;
                case ".WMF":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Bmp;
            }
        }

        #endregion
    }

    public class User
    {
        #region Свойства
    
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int LINK { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string S_NAME { get; set; }
        /// <summary>
        /// Префикс пользователя
        /// </summary>
        public string PREF { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string S_PASS { get; set; }
        /// <summary>
        /// Идентификатор имени входа
        /// </summary>
        public int F_LOGIN { get; set; }
        /// <summary>
        /// Справочник имен входа
        /// </summary>
        public Dictionary LOGINS { get { return _logins; } }
        #endregion

        #region Metods

        /// <summary>
        /// Сохранение проекта
        /// </summary>
        public int Save()
        {
            try
            {
                if (LINK == 0)
                    return new DictionaryGate().InsertUser(this);
                else
                    return new DictionaryGate().UpdateUser(this);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Удаление элементов справочника дизайнов
        /// </summary>
        /// <param name="dt">Список удаляемых элементов</param>
        public static void Delete(int[] idx)
        {
            try
            {
                new DictionaryGate().DeleteUser(idx);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }
        /// <summary>
        /// Загрузка данных пользователя из базы по переданному id
        /// </summary>
        /// <param name="id">Идентфикатор пользователя</param>
        /// <returns>Экземпляр класса пользователя</returns>
        public static User LoadById(int id)
        {
            DataTable dt = new DictionaryGate().UserLoad(id);
            User u = new User();
            u.LINK = Convert.ToInt32(dt.Rows[0]["LINK"], CultureInfo.InvariantCulture);
            u.S_NAME = dt.Rows[0]["S_NAME"].ToString();
            u.PREF = dt.Rows[0]["PREF"].ToString();
            u.F_LOGIN = Convert.ToInt32(dt.Rows[0]["F_LOGIN"], CultureInfo.InvariantCulture);
            u.S_PASS = "";

            return u;
        }

        #endregion

        private Dictionary _logins = new Dictionary(DicType.LOGINS);
    }
}
