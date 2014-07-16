using Alternative.DB;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alternative.Model
{
    /// <summary>
    /// Предоставляет функционал работы с криптографией 
    /// </summary>
    public class AdminCripto
    {
        /// <summary>
        /// Устанавливает значение асинхронного ключа для типографии
        /// </summary>
        /// <param name="name">Имя файла ключа</param>
        /// <param name="path">Путь к файлу ключа</param>
        /// <returns>Значение ключа</returns>
        public string SetPressPublicKey(string name, string path)
        {
            string res;
            try
            {
                _gate.SetPressAsyncKey(name, path, out res);
                return res;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeyPressAsync));
            }
        }
        /// <summary>
        /// Возвращает значение публичного ключа для типографии
        /// </summary>
        /// <param name="name">Имя файла ключа</param>
        /// <param name="path">Путь к ключу</param>
        /// <returns>Значение ключа</returns>
        public string GetPressPublicKey(out string name, out string path)
        {
            try
            {
                return _gate.GetPressPublicKey(out name, out path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeyPressGetAsync));
            }
        }
        /// <summary>
        /// Редактируется путь или имя файла публичного ключа
        /// </summary>
        /// <param name="name">Имя публичного ключа</param>
        /// <param name="path">Путь к файлу публичного ключа</param>
        public void EditPressKey(string name, string path)
        {
            try
            {
                _gate.EditPressKey(name, path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeyPressEditAsync));
            }
        }

        /// <summary>
        /// Устанавливает значение асинхронного ключа для типографии
        /// </summary>
        /// <param name="name">Имя файла ключа</param>
        /// <param name="path">Путь к файлу ключа</param>
        /// <returns>Значение ключа</returns>
        public string SetSalePublicKey(string name, string path)
        {
            string res;
            try
            {
                _gate.SetSaleAsyncKey(name, path, out res);
                return res;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeySaleAsync));
            }
        }
        /// <summary>
        /// Возвращает значение публичного ключа платежной системы
        /// </summary>
        /// <param name="name">Имя файла ключа</param>
        /// <param name="path">Путь к ключу</param>
        /// <returns>Значение ключа</returns>
        public string GetSalePublicKey(out string name, out string path)
        {
            try
            {
                return _gate.GetSalePublicKey(out name, out path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeySaleGetAsync));
            }
        }
        /// <summary>
        /// Редактируется путь или имя файла публичного ключа
        /// </summary>
        /// <param name="name">Имя публичного ключа</param>
        /// <param name="path">Путь к файлу публичного ключа</param>
        public void EditSaleKey(string name, string path)
        {
            try
            {
                _gate.EditSaleKey(name, path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmKeySaleEditAsync));
            }
        }

        /// <summary>
        /// Устанавливает значение для ключевого слова
        /// </summary>
        /// <param name="name">Имя файла ключевого слова</param>
        /// <param name="path">Путь к файлу ключевого слова</param>
        /// <returns>Значение ключевого слова</returns>
        public string SetHashWord(string name, string path)
        {
            string res;
            try
            {
                _gate.SetHashWord(name, path, out res);
                return res;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmSetHashWord));
            }
        }
        /// <summary>
        /// Возвращает значение ключевого слова
        /// </summary>
        /// <param name="name">Имя файла ключевого слова</param>
        /// <param name="path">Путь к ключевому слову</param>
        /// <returns>Значение ключевого слова</returns>
        public string GetHashWord(out string name, out string path)
        {
            try
            {
                return _gate.GetHashWord(out name, out path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmGetHash));
            }
        }
        /// <summary>
        /// Редактируется путь или имя файла ключевого слова
        /// </summary>
        /// <param name="name">Имя файла ключевого слова</param>
        /// <param name="path">Путь к файлу ключевого слова</param>
        public void EditHashWord(string name, string path)
        {
            try
            {
                _gate.EditHashWord(name, path);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EAdmEditHash));
            }
        }

        private AdminCriptoGate _gate = new AdminCriptoGate();
    }
}
