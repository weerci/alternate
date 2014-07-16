using Alternative.DB;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Alternative.Model
{
    /// <summary>
    /// Представляет сущность "Карточка"
    /// </summary>
    public class Card
    {
        #region Properties

        /// <summary>
        /// Уникальный идентификатор карточки
        /// </summary>
        public Int64 LINK { get; set; }

        /// <summary>
        /// Номер карточки
        /// </summary>
        public Int64 N_NUMBER { get; set; }

        /// <summary>
        /// Пин-код карточки
        /// </summary>
        public string S_PIN { get; set; }

        /// <summary>
        /// Ссыкла на проект
        /// </summary>
        public Int64 F_PROJECT { get; set; }

        /// <summary>
        /// Номинал карты
        /// </summary>
        public double M_FACE { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public double M_SALE { get; set; }

        /// <summary>
        /// Дата начала действия карточки
        /// </summary>
        public DateTime D_FROM { get; set; }

        /// <summary>
        /// Дата окончания действия карточки
        /// </summary>
        public DateTime D_END { get; set; }

        /// <summary>
        /// Дата активации карты
        /// </summary>
        public DateTime D_ACTIVATE { get; set; }

        /// <summary>
        /// Статус карты
        /// </summary>
        public int F_STATUS { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string T_COMMENT { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает журнал изменений карточки
        /// </summary>
        /// <param name="cardId">Идентификатор карточки</param>
        /// <param name="dgv">Сетка данных, куда складывается информация из журнала</param>
        public static void Journal(Int64 cardId, DataGridView dgv)
        {
            DataTable dt = CardGate.Journal(cardId);
            dgv.DataSource = dt;
            dgv.Columns["LINK"].Visible = false;
            dgv.Columns["CARD_NUM"].HeaderText = CommonText.DHNumber;
            dgv.Columns["F_OPER_TYPES"].Visible = false;
            dgv.Columns["OPER_NAME"].HeaderText = CommonText.DHOperType;
            dgv.Columns["D_DATE"].HeaderText = CommonText.DHOperDate;
            dgv.Columns["field_1"].HeaderText = CommonText.DHField_1;
            dgv.Columns["field_2"].HeaderText = CommonText.DHField_2;
        }

        /// <summary>
        /// Возвращается DataTable заполненный прикрепленными к проекту карточками
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <returns>DataTable с итоговым набором</returns>
        public static DataTable List(Int64 projectId)
        {
            return CardGate.List(projectId);
        }

        /// <summary>
        /// Поиск карточек значения полей которых совпадают со значениями переданными в параметрах функции
        /// </summary>
        /// <param name="s_project_number">Номер проекта</param>
        /// <param name="n_card_number">Номер карточки</param>
        /// <param name="m_face">Номинал карточки</param>
        /// <param name="s_pin">Пин карточки</param>
        /// <param name="dgv">Сетка с колонками, в которую выводятся найденные карточки</param>
        public static void Find(string s_project_number, int n_card_number, double m_face, string s_pin, DataGridView dgv)
        {
            DataTable dt = CardGate.Find(s_project_number, n_card_number, m_face, s_pin);
            dgv.DataSource = dt;
            dgv.Columns["LINK"].Visible = false;
            dgv.Columns["N_NUMBER"].HeaderText = CommonText.DHNumber;
            dgv.Columns["M_FACE"].HeaderText = CommonText.DHFace;
            dgv.Columns["M_SALE"].Visible = false;
            dgv.Columns["D_FROM"].HeaderText = CommonText.DHFrom;
            dgv.Columns["d_end"].HeaderText = CommonText.DHEnd;
            dgv.Columns["D_ACTIVATE"].HeaderText = CommonText.DHActivated;
        }

        /// <summary>
        /// Активация карточки
        /// </summary>
        /// <param name="cardId"></param>
        public static void Active(Int64 cardId, string pin, string amount, string phone)
        {
            try
            {
                #region Check

                // Поле Pin должно быть заполнено

                #endregion

                Card card = Card.Load(cardId);
                CardGate.Active(card, pin, amount, phone);
                Thread.Sleep(Properties.Settings.Default.IntervalGetState);
                DataTable dt = CardGate.GetState(card);
                for (int i = 0; i < Properties.Settings.Default.CountRepeatGetState; i++)
                {
                    if (Convert.ToInt32(dt.Rows[0]["state"]) != 0)
                        break;
                    Thread.Sleep(Properties.Settings.Default.IntervalGetState);
                    dt = CardGate.GetState(card);
                }

                MessageBox.Show(dt.Rows[0]["prs_state"].ToString() + "\n" + dt.Rows[0]["ext_descr"].ToString(), 
                    CommonText.MsgCardState, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel());
            }

        }

        /// <summary>
        /// Переданный DataGridView заполняется данными по карточкам привязанным к проекту
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        /// <param name="dg">DataGridView для заполнения данными по карточкам</param>
        public static void List(Int64 projectId, DataGridView dgv)
        {
            try
            {
                dgv.DataSource = CardGate.List(projectId);
                dgv.Columns["LINK"].Visible = false;
                dgv.Columns["S_PREF"].Visible = false;
                dgv.Columns["N_NUMBER"].HeaderText = CommonText.DHNumber;
                dgv.Columns["S_STATUS"].HeaderText = CommonText.DHStatus;
                dgv.Columns["M_FACE"].HeaderText = CommonText.DHFace;
                dgv.Columns["M_SALE"].HeaderText = CommonText.DHSale;
                dgv.Columns["D_FROM"].HeaderText = CommonText.DHFrom;
                dgv.Columns["D_END"].HeaderText = CommonText.DHEnd;
                dgv.Columns["D_ACTIVATE"].HeaderText = CommonText.DHActivated;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel());
            }
        }

        /// <summary>
        /// Создается карточка по переданному идентификатору
        /// </summary>
        /// <param name="f_card">Идентификатор карточки</param>
        /// <returns>Новая карточка</returns>
        public static Card Load(Int64 f_card)
        {
            try
            {
                DataTable dt = CardGate.Load(f_card);
                Card card = new Card();
                if (dt.Rows.Count > 0)
                {
                    card.LINK = Convert.ToInt64(dt.Rows[0]["LINK"], CultureInfo.InvariantCulture);
                    card.N_NUMBER = Convert.ToInt64(dt.Rows[0]["N_NUMBER"], CultureInfo.InvariantCulture); 
                    card.F_PROJECT = Convert.ToInt64(dt.Rows[0]["F_PROJECT"], CultureInfo.InvariantCulture);
                    card.M_FACE = Convert.ToDouble(dt.Rows[0]["M_FACE"], CultureInfo.InvariantCulture);
                    card.M_SALE = Convert.ToDouble(dt.Rows[0]["M_SALE"], CultureInfo.InvariantCulture); 
                    card.D_FROM = Convert.ToDateTime(dt.Rows[0]["D_FROM"], CultureInfo.InvariantCulture); 
                    card.D_END = Convert.ToDateTime(dt.Rows[0]["D_END"], CultureInfo.InvariantCulture);
                    if (dt.Rows[0]["D_ACTIVATE"] != DBNull.Value)
                        card.D_ACTIVATE = Convert.ToDateTime(dt.Rows[0]["D_ACTIVATE"], CultureInfo.InvariantCulture); 
                    card.F_STATUS = Convert.ToInt32(dt.Rows[0]["F_STATUS"], CultureInfo.InvariantCulture);
                    if (dt.Rows[0]["T_COMMENT"] != DBNull.Value)                    
                        card.T_COMMENT = dt.Rows[0]["T_COMMENT"].ToString(); 
                }
                return card;
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ECardLoad));
            }
        }

        /// <summary>
        /// Удаляются все карточки выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        public static void CardUnload(Int64 projectId)
        { 
            try 
	        {
                CardGate.CardUnload(projectId);
	        }
	        catch (Exception err)
	        {
                throw EAlternate.CreateException(err, new EAltModel(ECardUnloadToPress));
	        }
        }

        public static void CardUnloadToPress(Int64 projectId)
        {
            try
            {
                CardGate.CardUnloadToPress(projectId);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltModel(ECardUnloadToPress));
            }
        }
        
        #endregion

        #region Errors

        const string ECardUnloadToPress = "Не удалось выгрузить карточки проекта в типографию.";
        const string ECardUnload = "Не удалось удалить карточки проекта.";
        const string ECardLoad = "Не удалось загрузить карточки.";
        
        // Ошибки ввода
        const string EValidPhone = "Номер телефона должен состоять из 10 цифр, например: '9032920000'";
        const string EValidCard = "Номер карты должен состоять из 14 цифр";
        const string EValidPin = "Пин-код должен состоять из 12 цифр";
        const string EValidAmmount = "Сумма записывается в копейках и должна \nсостоять из последовательности от 3 до 14 цифр";

        // Функции проверки ввода данных
        public static string ValidPhone(string phone)
        {
            return new Regex(@"^\d{10}$").IsMatch(phone.Replace("-","")) ? String.Empty : EValidPhone; 
        }
        public static string ValidCard(string cardNumber)
        {
            return new Regex(@"^\d{14}$").IsMatch(cardNumber) ? String.Empty : EValidCard;
        }
        public static string ValidPin(string pin)
        {
            return new Regex(@"^\d{12}$").IsMatch(pin) ? String.Empty : EValidPin;
        }
        public static string ValidAmmount(string ammount)
        {
            return new Regex(@"^\d{3,14}$").IsMatch(ammount) &&  Convert.ToInt64(ammount) > 0 ? String.Empty : EValidAmmount;
        }
        

        #endregion
    }
}
