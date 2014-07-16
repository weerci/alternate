using Alternative.DB;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative.Model
{
    public class Project
    {
        #region Properties

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Int64 LINK { get; set; }

        /// <summary>
        /// Имя проекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер проекта
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Ссылка на платежную систему
        /// </summary>
        public Int64 F_Payment { get; set; }

        /// <summary>
        /// Ссылка на юридическое лицо
        /// </summary>
        public Int64 F_Jurictic { get; set; }

        /// <summary>
        /// Ссылка на справочник дизайнов
        /// </summary>
        public int F_Design { get; set; }

        /// <summary>
        /// Справочник дизайнов
        /// </summary>
        public Dictionary Design { get { return _design; } }
        
        /// <summary>
        /// Справочник платежек
        /// </summary>
        public Dictionary Payment { get {return _payment; } }
        
        /// <summary>
        /// Справочник юридическх лиц
        /// </summary>
        public Dictionary Juristic { get { return _juristic; } }

        /// <summary>
        /// Изовбражение лицевой стороны карточки
        /// </summary>
        public MemoryStream Facade { get; set; }

        /// <summary>
        /// Изображение обратной стороны карточки
        /// </summary>
        public MemoryStream Outside { get; set; }

        /// <summary>
        /// Список карт привязанных к проекту
        /// </summary>
        public List<Card> Cards { get { return _cards; } }

        /// <summary>
        /// Возвращает текущее состояние проекта, свойство только для чтения, 
        /// изменение состояния возможно только в хранимых процедурах
        /// </summary>
        public ProjectState State { get; private set; }

        public string StateName { get; private set; }

        #endregion

        #region Методы

        /// <summary>
        /// Сохраняет проект в базе данных
        /// </summary>
        /// <returns>Сохраненный проект</returns>
        public Project Save()
        {
            try
            {
                ProjectGate.Insert(F_Payment, Name, F_Jurictic, Number, F_Design);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err,  new EAltModel(ErrorMsg.EPrInsert));
            }
            return this;
        }
        
        /// <summary>
        /// Обновляет данные проекта в базе
        /// </summary>
        /// <returns>Обновленный проект</returns>
        public Project Update()
        {
            try
            {
                ProjectGate.Update(LINK, F_Payment, Name, F_Jurictic, Number, F_Design);
                return this;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ErrorMsg.EPrUpdate));
            }
        }

        /// <summary>
        /// Загружаются связанные с проектом карточки
        /// </summary>
        public DataTable LoadCards()
        {
            DataTable dt = Card.List(LINK);
            return dt;
        }

        /// <summary>
        /// Удаляет проект из базы данных
        /// </summary>
        /// <param name="projectId">Идентификатор удаляемого проекта</param>
        public static void Delete(DataGridView dgv)
        {
            try
            {
                if (dgv.SelectedRows.Count > 0)
                    ProjectGate.Delete(dgv.SelectedRows.Cast<DataGridViewRow>().Select(n => Convert.ToInt64(n.Cells["LINK"].Value)).ToArray());
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err);
            }
        }


        /// <summary>
        /// Список проектов
        /// </summary>
        /// <param name="dgv">DataGridView для выгрузки</param>
        public static void List(DataGridView dgv)
        {
            try
            {
                dgv.DataSource = ProjectGate.List();
                dgv.Columns["LINK"].Visible = false;
                dgv.Columns["S_NAME"].HeaderText = CommonText.DHName;
                dgv.Columns["S_NUMBER"].HeaderText = CommonText.DHNumber;
                dgv.Columns["PAYMENT"].HeaderText = CommonText.DHPayment;
                dgv.Columns["JURISTIC"].HeaderText = CommonText.DHJuristic;
                dgv.Columns["DESIGN"].HeaderText = CommonText.DHDesign;
                dgv.Columns["LABEL"].HeaderText = CommonText.DHStatus;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel());
            }
        }

        /// <summary>
        /// Из базы данных подгружается информация о проекте и возвращается проект
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <returns>Экземпляр проекта</returns>
        public static Project Load(Int64 id)
        {
            try 
	        {	        
                DataTable dt = ProjectGate.Load(id);
                Project pr = new Project();
                if (dt.Rows.Count > 0)
                {
                    pr.LINK = Convert.ToInt64(dt.Rows[0]["LINK"], CultureInfo.InvariantCulture);
                    pr.Name = dt.Rows[0]["S_NAME"].ToString();
                    pr.Number = dt.Rows[0]["S_NUMBER"].ToString();
                    pr.F_Design = Convert.ToInt32(dt.Rows[0]["F_DESIGN"], CultureInfo.InvariantCulture);
                    pr.F_Jurictic = Convert.ToInt64(dt.Rows[0]["F_JURISTIC"], CultureInfo.InvariantCulture);
                    pr.F_Payment = Convert.ToInt64(dt.Rows[0]["F_PAYMENTS"], CultureInfo.InvariantCulture);
                    pr.State = getState(dt.Rows[0]["LABEL"].ToString());
                    pr.StateName = dt.Rows[0]["LABEL_NAME"].ToString();
                }
                return pr;
	        }
	        catch (Exception err)
	        {
                throw EAlternate.CreateException(err, new EAltModel());
            }
        }

        /// <summary>
        /// Преобразует строку в экземпляр перечисления ProjectState
        /// </summary>
        /// <param name="state">Входная строка</param>
        /// <returns>Экземпляр перечисления ProjectState</returns>
        public static ProjectState getState(string state)
        {
            switch (state)
            {
                case "out_to_press":
                    return ProjectState.Pressed;
                default:
                    return ProjectState.New;
            }
        }

        #endregion

        #region Field

        private Dictionary _design = new Dictionary(DicType.DESIGN);
        private Dictionary _payment = new Dictionary(DicType.PAYMENTS);
        private Dictionary _juristic = new Dictionary(DicType.JURISTIC);
        private List<Card> _cards = new List<Card>();

        #endregion
    }

    public enum ProjectState {New, Pressed};
}
