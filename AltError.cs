using DbSQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Alternative
{
    /// <summary>
    /// Основной класс обработки ошибок, содержит статическую функцию вызываемую обработки ошибок в зависимости от 
    /// их типа. Является базовым классом для прочих типов ошибок. Релизует обработку нераспознаных ошибок
    /// </summary>
    public class EAlternate : Exception
    {
        #region Constructors

        /// <summary>
        /// Закрывается конструктор класса. 
        /// Экземпляр можно создать статическим методом CreateException
        /// </summary>
        public EAlternate() { }
        public EAlternate(string message)
        {
            _text = message;
        }

        /// <summary>
        /// Генрация нужного сообщения. При вызове функции из секции catch может понадобиться:
        /// <list type="bullet">
        /// <item><description>передать ошибку без изменения (вызов процедуры: throw CreateException(err))</description></item>
        /// <item><description>перегенерить ошибку в тип EAlternate, с сохранением существующих данных ошибки (вызов процедуры: throw CreateException(err, new EAltDb()))</description></item>
        /// <item><description>перегенерить ошибку в тип EAlternate, с сохранением существующих данных ошибки и добавлением своего сообщения (вызов процедуры: throw CreateException(err, new EAltDb("Error message")))</description></item>
        /// <item><description>перегенерить ошибку с замещением ее данных (вызов процедуры: throw CreateException(new EAltDb()))</description></item>
        /// </list>
        /// </summary>
        /// <param name="err">Сгенерированная кодом ошибка</param>
        /// <param name="eNewAlt">Обработанная пользователем ошибка</param>
        /// <param name="ommitIfAlt">Передать ошибку вверх по стеку без обработки, если эта ошибка типа EAlternate, по умолчанию true</param>
        public static Exception CreateException(Exception err, EAlternate eNewAlt = null, bool ommitIfAlt = true)
        {
            EAlternate e = err as EAlternate;
            // Если исключение типа EAlternate и установлен флаг пропустить исключение - возвращаем исключение без изменения
            if ((e != null) && ommitIfAlt)
                return err;

            if (eNewAlt != null)
            { 
                if (String.IsNullOrEmpty(eNewAlt.Text))
                    eNewAlt.Text = err.Message;
                else
                    eNewAlt.Text = eNewAlt.Text + Environment.NewLine + err.Message;
                eNewAlt.Stack = err.StackTrace;
                eNewAlt.HelpLink = err.HelpLink;
                foreach (DictionaryEntry element in err.Data)
                    eNewAlt.Data.Add(element.Key, element.Value);
                return eNewAlt;
            }

            return err;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Стек ошибки, шифруется при помощи public key
        /// </summary>
        public virtual string Stack { get { return EAlternate.EncriptRSA(_stack); } set { _stack = value; } }
        
        /// <summary>
        /// Текст ошибки
        /// </summary>
        public virtual string Text { get { return _text; } set { _text = value; } }

        #endregion

        /// <summary>
        /// Обработка неопознанной ошибки
        /// </summary>
        public virtual void Treat(Exception err)
        {
            SaveToErrorDb(err);
            MessageBox.Show(err.Message, CommonText.UserFormMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Шифрование с помощью внешнего ключа стека ошибки. 
        /// Файл внешнего ключа должен быть расположен в каталоге установки программы.
        /// </summary>
        /// <returns>Возвращается шифрованный текст стека</returns>
        public static string EncriptRSA(string stack)
        {
            string pubKey;
            try
            {
                pubKey = File.ReadAllText(Path.ChangeExtension(Application.ExecutablePath, ".pub"));
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(pubKey);
                return Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(stack), false));
            }
            catch
            {
                pubKey = null;
            }
            return stack;
        }

        /// <summary>
        /// Исключение сохраняется в базе данных ошибок
        /// </summary>
        protected void SaveToErrorDb(Exception err)
        {
            string eType = err.GetType().ToString();
            string eText;
            string eStack;

            EAlternate eAlt = err as EAlternate;
            if (eAlt != null)
            {
                eText = eAlt.Text;
                eStack = eAlt.Stack;
            }
            else
            {
                eText = err.Message;
                eStack = EAlternate.EncriptRSA(err.StackTrace);
            }

            ParametersCollection parameters = new ParametersCollection();
            parameters.Add("dt", DateTime.Now, DbType.Date);
            parameters.Add("etype", eType, DbType.String);
            parameters.Add("etext", eText, DbType.String);
            parameters.Add("estack", eStack, DbType.String);

            SQLite.Item.Insert("errors", parameters);
        }

        /// <summary>
        /// Обслуживание базы данных ошибок.
        /// Из базы удаляются старые ошибки. Кол-во дней раннее которых ошибки удаляются определяются 
        /// параметром ErrorDay в файле конфигурации
        /// </summary>
        public static void HandleDb()
        {
            SQLite.Item.Vacuum();
            SQLite.Item.ClearErrorDb(-Properties.Settings.Default.edt);
        }

        /// <summary>
        /// Статический метод обработки ошибок программы, вызывается в program.cs
        /// </summary>
        /// <param name="err">Экземляр исключения</param>
        public static void HandleError(Exception err)
        {
            EAlternate eAlt = err as EAlternate;
            if (eAlt != null)
                eAlt.Treat(err);
            else 
                new EAlternate().Treat(err);
        }

        protected bool ErrorNumberInRange(IDictionary de)
        {
            object obj = de["HelpLink.EvtID"];
            int objValue;
            if (obj != null && Int32.TryParse(obj.ToString(), out objValue))
                return (objValue >= USER_DB_ERROR_MIN && objValue <= USER_DB_ERROR_MAX) ? true : false;
            return false;
        }


        #region Fields

        private string _stack;
        private string _text;

        #endregion

        const int USER_DB_ERROR_MIN = 53200;
        const int USER_DB_ERROR_MAX = 53300;
    }

    /// <summary>
    /// Исключение сгенерированное на уровне интерфейса приложения
    /// </summary>
    public class EAltDesign : EAlternate
    {
        public EAltDesign() : base() { }
        public EAltDesign(string message) : base(message) { }
        public override void Treat(Exception err)
        {
            base.SaveToErrorDb((EAltDesign)err);
            MessageBox.Show(((EAltDesign)err).Text, CommonText.UserFormMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Исключение сгенерированное на уровне модели классов приложения
    /// </summary>
    public class EAltModel : EAlternate
    {
        public EAltModel() : base() { }
        public EAltModel(string message) : base(message) { }
        public override void Treat(Exception err)
        {
            base.SaveToErrorDb((EAltModel)err);
            MessageBox.Show(((EAltModel)err).Text, CommonText.UserFormMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Исключение сгенерированное на уровне обращения к базе данных
    /// </summary>
    public class EAltDb : EAlternate
    {
        public EAltDb() : base() { }
        public EAltDb(string message) : base(message) { }
        public override void Treat(Exception err)
        {
            if (!ErrorNumberInRange(err.Data))
                base.SaveToErrorDb((EAltDb)err);
            MessageBox.Show(((EAltDb)err).Text, CommonText.UserFormMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Обработанное исключение, которое нет необходимости заносить в лог. 
    /// Сообщает пользователю о неправильном поведении программы связанным с пользовательским вводом.
    /// </summary>
    public class EAltMessage : EAlternate
    {
        public EAltMessage(string message):base(message){}
        public override void Treat(Exception err)
        {
            EAltMessage e = err as EAltMessage;
            MessageBox.Show(e.Text, CommonText.UserFormMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}
