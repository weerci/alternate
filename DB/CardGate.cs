using Alternative.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative.DB
{
    public class CardGate
    {
        /// <summary>
        /// Возвращает список карточек проекта
        /// </summary>
        /// <param name="f_project">Идентификатор проекта</param>
        /// <returns>DataTable с карточками</returns>
        public static DataTable List(Int64 f_project)
        {
            string sql = "pla.pr_card_list";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@f_project", SqlDbType.BigInt, f_project, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }
        
        /// <summary>
        /// Вставляет список карточек в проект
        /// </summary>
        /// <param name="cards">Список карточек</param>
        public static void Insert(List<Card> cards)
        {
            string sql = "pla.pr_card_ins";


            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@n_number", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@s_pin", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@f_project", SqlDbType.BigInt);
                sqlCmd.Parameters.Add("@m_face", SqlDbType.Money);
                sqlCmd.Parameters.Add("@m_sale", SqlDbType.Money);
                sqlCmd.Parameters.Add("@d_from", SqlDbType.SmallDateTime);
                sqlCmd.Parameters.Add("@d_end", SqlDbType.SmallDateTime);
                sqlCmd.Parameters.Add("@d_activate", SqlDbType.SmallDateTime);
                sqlCmd.Parameters.Add("@f_status", SqlDbType.TinyInt);
                sqlCmd.Parameters.Add("@t_comment", SqlDbType.NVarChar);

                foreach (Card item in cards)
                {
                    sqlCmd.Parameters["@n_number"].Value = item.N_NUMBER;
                    sqlCmd.Parameters["@s_pin"].Value = item.S_PIN;
                    sqlCmd.Parameters["@f_project"].Value = item.F_PROJECT;
                    sqlCmd.Parameters["@m_face"].Value = item.M_FACE;
                    sqlCmd.Parameters["@m_sale"].Value = item.M_SALE;
                    sqlCmd.Parameters["@d_from"].Value = item.D_FROM;
                    sqlCmd.Parameters["@d_end"].Value = item.D_END;
                    if (item.D_ACTIVATE.Ticks == 0)
                        sqlCmd.Parameters["@d_activate"].IsNullable = true;
                    else
                        sqlCmd.Parameters["@d_activate"].Value = item.D_ACTIVATE;
                    sqlCmd.Parameters["@f_status"].Value = item.F_STATUS;
                    sqlCmd.Parameters["@t_comment"].Value = item.T_COMMENT;

                    sqlCmd.ExecuteNonQuery();

                }


                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }

        }

        /// <summary>
        /// Возвращает журнал карточки
        /// </summary>
        /// <param name="f_card">Идентификатор карточки</param>
        /// <returns>DataTable с данными по карточке</returns>
        public static DataTable Journal(Int64 f_card)
        {
            string sql = "pla.pr_journal_list";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@f_card", SqlDbType.BigInt, f_card, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }
        /// <summary>
        /// Выгрузка журнала в Excel
        /// </summary>
        /// <param name="cardId">Идентификатор карточки</param>
        /// <param name="dateFrom">Дата с</param>
        /// <param name="dateTo">Дата по</param>
        /// <returns>Результирующий набор данных</returns>
        public static DataTable JournalToExcel(Int64 cardId, DateTime dateFrom, DateTime dateTo)
        { 
            string sql = "pl.pr_journal_to_excel";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@card_id", SqlDbType.BigInt, cardId, sqlCmd, false);
                WFSql.DB.AddInParameter("@df", SqlDbType.DateTime, dateFrom, sqlCmd, false);
                WFSql.DB.AddInParameter("@dt", SqlDbType.DateTime, dateTo, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        
        }

        /// <summary>
        /// Возвращает поля карточки по ее идентификатору
        /// </summary>
        /// <param name="f_card">Идентификатор карточки</param>
        /// <returns>Набор данных связанных с карточкой</returns>
        public static DataTable Load(Int64 f_card)
        {
            string sql = "pla.pr_card_load";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.BigInt, f_card, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err);
            }             
        }

        /// <summary>
        /// Удаляются карточки выбранного проекта
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        public static void CardUnload(Int64 projectId)
        {
            string sql = "pla.pr_card_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@f_project", SqlDbType.BigInt, projectId, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53206")
                    throw new EAltMessage(err.Message);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ECardDelete));
            }
        }

        /// <summary>
        /// Карточки проекта выгружаются в типографию
        /// </summary>
        /// <param name="projectId">Идентификатор проекта</param>
        public static void CardUnloadToPress(Int64 projectId)
        {
            string sql = "pla.pr_card_to_press";
            Project project = Project.Load(projectId);

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@f_project", SqlDbType.BigInt, projectId, sqlCmd, false);

                SqlDataReader reader = sqlCmd.ExecuteReader();

                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Файл карточек (*.csv)|*.csv|All files (*.*)|*.*";
                sf.FileName = String.Format("{0}_{1}", project.Name, DateTime.Now.ToShortDateString());
                sf.FilterIndex = 1;
                sf.RestoreDirectory = true;

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sf.FileName;
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        while (reader.Read())
                            sw.WriteLine(String.Format("{0};{1};{2};{3};{4}", reader[0], reader[1], reader[2], reader[3], reader[4]));
                    }
                    reader.Close();
                    WFSql.DB.Commit();

                    if (MessageBox.Show(CommonText.MsgCardUnloadedToPress, CommonText.TitleCardUnloadToPress, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", "/select, \"" + sf.FileName + "\""); 
                    }
                }
                else
                    WFSql.DB.Rollback();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();

                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53206")
                    throw new EAltMessage(err.Message);
                else
                    throw EAlternate.CreateException(err, new EAltDb(String.Format(ECardUnloadToPress, project.Number)));
            }
        }

        /// <summary>
        /// Производится поиск карточек по заданным параметрам
        /// </summary>
        /// <param name="s_project_number">Номер проекта</param>
        /// <param name="n_card_number">Номер карты</param>
        /// <param name="m_face">Номинал</param>
        /// <param name="s_pin">Пин-код</param>
        /// <returns>Список найденных карт</returns>
        public static DataTable Find(string s_project_number, int n_card_number, double m_face, string s_pin)
        {
            //try
            //{
            //    AlternateClient ac = new AlternateClient();
            //    List<string> ls = new List<string>();
            //    if (!String.IsNullOrEmpty(s_project_number.Trim()))
            //        ls.Add("@s_project_number=" + s_project_number);
            //    if (n_card_number != -1)
            //        ls.Add("@n_card_number=" + n_card_number);
            //    if (m_face != -1)
            //        ls.Add("@m_face=" + m_face);
            //    if (!String.IsNullOrEmpty(s_pin.Trim()))
            //        ls.Add("@s_pin=" + s_pin);

            //    Query q = new Query(ls, "pl", "pr_card_find");
            //    string result = ac.Query(ConnectGate.Certificate, q.AsXML());

            //    DataTable dt = new DataTable();
            //    if (Query.ParseResult(dt, ref result) == 0)
            //    {
            //        throw new Exception(result);
            //    }
            //    else
            //        return dt;
            //}
            //catch (Exception err)
            //{
            //    throw EAlternate.CreateException(err, new EAltDb(String.Format(EFindCards, n_card_number)));
            //}
            return null;
        }

        public static void Active(Card card, string pin, string amount, string phone)
        {

            string sql = "pl.cd_activ_rapid";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                WFSql.DB.AddInParameter("@s_card_id", SqlDbType.BigInt, card.LINK, sqlCmd, true);
                WFSql.DB.AddInParameter("@s_pin", SqlDbType.VarChar, pin, sqlCmd, true);
                WFSql.DB.AddInParameter("@s_amount", SqlDbType.VarChar, amount, sqlCmd, true);
                WFSql.DB.AddInParameter("@s_phone", SqlDbType.VarChar, phone, sqlCmd, true);

                sqlCmd.ExecuteNonQuery();
                WFSql.DB.Commit();
            }
            catch (Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err, new EAltDb(String.Format(EActiveCards, card.N_NUMBER)));
            }
        }

        public static DataTable GetState(Card card)
        {

            string sql = "pl.cd_activ_state";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                WFSql.DB.AddInParameter("@s_card", SqlDbType.VarChar, card.N_NUMBER.ToString(), sqlCmd, true);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;


            }
            catch (Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err, new EAltDb(String.Format(EActiveCards, card.N_NUMBER)));
            }
        }

        #region Errors

        const string EFindCards = "Не удалось найти карточку № {0}.";
        const string EActiveCards = "Не удалось активировать карточку № {0}";
        const string ECardUnloadToPress = "Не удалось выгрузить в типографию карты проекта № {0}.";
        const string ECardDelete = "Не удалось удалить карточки проекта.";

        #endregion
    }
}
