using Alternative.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Alternative.DB
{
    public class DictionaryGate
    {
        public void Open(string name, DataTable dt)
        {
            string sql = "pla.dc_dictionary_load";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, name, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();
            }
            catch
            {
                WFSql.DB.Rollback();
                throw;
            }
        }
       
        public int InsertDesign(Design dt)
        {
            string sql = "pla.dc_design_ins";

            WFSql.DB.StartTransaction();
            try
            {
                int size = 0;
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, dt.Name, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_f_ext", SqlDbType.VarChar, dt.FacedeFormat.ToString(), sqlCmd, false);
                WFSql.DB.AddInParameter("@s_o_ext", SqlDbType.VarChar, dt.OutsideFormat.ToString(), sqlCmd, false);
                SqlParameter prmFacede = sqlCmd.Parameters.Add("@facede", SqlDbType.VarBinary);
                prmFacede.Value = Design.ImageToStream(dt.Facade, dt.FacedeFormat, out size);
                prmFacede.Size = size;
                SqlParameter prmOutside = sqlCmd.Parameters.Add("@outside", SqlDbType.VarBinary);
                prmOutside.Value = Design.ImageToStream(dt.Outside, dt.FacedeFormat, out size);
                prmOutside.Size = size;

                SqlParameter res = WFSql.DB.AddOutParameter("@res", SqlDbType.Int, sqlCmd);

                sqlCmd.ExecuteNonQuery();
                WFSql.DB.Commit();
                return Convert.ToInt32(res.Value, CultureInfo.InvariantCulture);
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601")
                    throw new EAltMessage(ErrorMsg.EDcUniqueDesign);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcSaveDesign));
            }
        }
        public int UpdateDesign(Design dt)
        {
            string sql = "pla.dc_design_edit";

            WFSql.DB.StartTransaction();
            try
            {
                int size = 0;
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, dt.Name, sqlCmd, false);
                WFSql.DB.AddInParameter("@link", SqlDbType.VarChar, dt.LINK, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_f_ext", SqlDbType.VarChar, dt.FacedeFormat.ToString(), sqlCmd, false);
                WFSql.DB.AddInParameter("@s_o_ext", SqlDbType.VarChar, dt.OutsideFormat.ToString(), sqlCmd, false);
                SqlParameter prmFacede = sqlCmd.Parameters.Add("@facade", SqlDbType.VarBinary);
                prmFacede.Value = Design.ImageToStream(dt.Facade, dt.FacedeFormat, out size);
                prmFacede.Size = size;
                SqlParameter prmOutside = sqlCmd.Parameters.Add("@outside", SqlDbType.VarBinary);
                prmOutside.Value = Design.ImageToStream(dt.Outside, dt.FacedeFormat, out size);
                prmOutside.Size = size;

                sqlCmd.ExecuteNonQuery();
                WFSql.DB.Commit();
                return dt.LINK;
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601")
                    throw new EAltMessage(ErrorMsg.EDcUniqueDesign);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcSaveDesign));
            }
        }
        public void DeleteDesign(int[] idx)
        {
            string sql = "pla.dc_design_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                foreach (int item in idx)
                {
                    sqlCmd.Parameters["@link"].Value = item;
                    sqlCmd.ExecuteNonQuery();
                }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcDeleteDictionary)); 
                ;
            }
        }
        public DataTable DesignLoad(int id)
        {
            string sql = "pla.dc_design_load";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.Int, id, sqlCmd, false);

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

        public void InsertPayment(DataTable dt)
        {
            string sql = "pla.dc_payment_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@pref", SqlDbType.VarChar);
                DataTable addTable = dt.GetChanges(DataRowState.Added);
                if (addTable != null && addTable.Rows.Count > 0)
                    foreach (DataRow item in addTable.Rows)
                    {
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.Parameters["@pref"].Value = item["S_PREF"];
                        sqlCmd.ExecuteNonQuery();
                    }
                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601")
                    throw new EAltMessage(ErrorMsg.EDcUniquePayment);
                else
                    throw EAlternate.CreateException(err);
            }
        }
        public void UpdatePayment(DataTable dt)
        {
            string sql = "pla.dc_payment_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@pref", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);

                DataTable editTable = dt.GetChanges(DataRowState.Modified);
                if (editTable != null && editTable.Rows.Count > 0)
                    foreach (DataRow item in editTable.Rows)
                    {
                        sqlCmd.Parameters["@link"].Value = item["LINK"];
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.Parameters["@pref"].Value = item["S_PREF"];
                        sqlCmd.ExecuteNonQuery();
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601")
                    throw new EAltMessage(ErrorMsg.EDcUniquePayment);
                else
                    throw EAlternate.CreateException(err);
            }
        }
        public void DeletePayment(DataTable dt)
        {
            string sql = "pla.dc_payment_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                DataTable delTable = dt.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                    foreach (DataRow item in delTable.Rows)
                    {
                        var res = item["LINK", DataRowVersion.Original];
                        if (res != DBNull.Value)
                        {
                            sqlCmd.Parameters["@link"].Value = Convert.ToInt32(res, CultureInfo.InvariantCulture);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcDeleteDictionary));

            }
        }

        public void InsertJuristic(DataTable dt)
        {
            string sql = "pla.dc_juristic_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                DataTable addTable = dt.GetChanges(DataRowState.Added);
                if (addTable != null && addTable.Rows.Count > 0)
                    foreach (DataRow item in addTable.Rows)
                    {
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }
                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueJuristic);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcInsertDictionary));
            }
        }
        public void UpdateJuristic(DataTable dt)
        {
            string sql = "pla.dc_juristic_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);

                DataTable editTable = dt.GetChanges(DataRowState.Modified);
                if (editTable != null && editTable.Rows.Count > 0)
                    foreach (DataRow item in editTable.Rows)
                    {
                        sqlCmd.Parameters["@link"].Value = item["LINK"];
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();

                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueJuristic);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcUpdateDictionary));
            }
        }
        public void DeleteJuristic(DataTable dt)
        {
            string sql = "pla.dc_juristic_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                DataTable delTable = dt.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                    foreach (DataRow item in delTable.Rows)
                    {
                        var res = item["LINK", DataRowVersion.Original];
                        if (res != DBNull.Value)
                        {
                            sqlCmd.Parameters["@link"].Value = Convert.ToInt32(res, CultureInfo.InvariantCulture);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcDeleteDictionary));
            }
        }

        public void InsertStatus(DataTable dt)
        {
            string sql = "pla.dc_status_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                DataTable addTable = dt.GetChanges(DataRowState.Added);
                if (addTable != null && addTable.Rows.Count > 0)
                    foreach (DataRow item in addTable.Rows)
                    {
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }
                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueStatusName);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcInsertDictionary));

            }
        }
        public void UpdateStatus(DataTable dt)
        {
            string sql = "pla.dc_status_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);

                DataTable editTable = dt.GetChanges(DataRowState.Modified);
                if (editTable != null && editTable.Rows.Count > 0)
                    foreach (DataRow item in editTable.Rows)
                    {
                        sqlCmd.Parameters["@link"].Value = item["LINK"];
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueStatusName);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcUpdateDictionary));

            }
        }
        public void DeleteStatus(DataTable dt)
        {
            string sql = "pla.dc_status_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                DataTable delTable = dt.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                    foreach (DataRow item in delTable.Rows)
                    {
                        var res = item["LINK", DataRowVersion.Original];
                        if (res != DBNull.Value)
	                    {
                            sqlCmd.Parameters["@link"].Value = Convert.ToInt32(res, CultureInfo.InvariantCulture);
                            sqlCmd.ExecuteNonQuery();
	                    }
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcDeleteDictionary));

            }
        }

        public void InsertOperation(DataTable dt)
        {
            string sql = "pla.dc_operation_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                DataTable addTable = dt.GetChanges(DataRowState.Added);
                if (addTable != null && addTable.Rows.Count > 0)
                    foreach (DataRow item in addTable.Rows)
                    {
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }
                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();

                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueOperationName);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcInsertDictionary));
            }
        }
        public void UpdateOperation(DataTable dt)
        {
            string sql = "pla.dc_operation_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@name", SqlDbType.VarChar);
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);

                DataTable editTable = dt.GetChanges(DataRowState.Modified);
                if (editTable != null && editTable.Rows.Count > 0)
                    foreach (DataRow item in editTable.Rows)
                    {
                        sqlCmd.Parameters["@link"].Value = item["LINK"];
                        sqlCmd.Parameters["@name"].Value = item["S_NAME"];
                        sqlCmd.ExecuteNonQuery();
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();

                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2601" && err.Message.Contains("s_name_idx"))
                    throw new EAltMessage(ErrorMsg.EDcUniqueOperationName);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcUpdateDictionary));
            }
        }
        public void DeleteOperation(DataTable dt)
        {
            string sql = "pla.dc_operation_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                DataTable delTable = dt.GetChanges(DataRowState.Deleted);
                if (delTable != null && delTable.Rows.Count > 0)
                    foreach (DataRow item in delTable.Rows)
                    {
                        var res = item["LINK", DataRowVersion.Original];
                        if (res != DBNull.Value)
                        {
                            sqlCmd.Parameters["@link"].Value = Convert.ToInt32(res, CultureInfo.InvariantCulture);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcDeleteDictionary));
            }
        }

        public int InsertUser(User u)
        {
            string sql = "pla.dc_user_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, u.S_NAME, sqlCmd, false);
                WFSql.DB.AddInParameter("@pass", SqlDbType.VarChar, u.S_PASS, sqlCmd, false);
                WFSql.DB.AddInParameter("@pref", SqlDbType.VarChar, u.PREF, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_login", SqlDbType.VarChar, u.F_LOGIN, sqlCmd, false);
                SqlParameter res = WFSql.DB.AddOutParameter("@res", SqlDbType.Int, sqlCmd);

                sqlCmd.ExecuteNonQuery();
                 
                WFSql.DB.Commit();

                return Convert.ToInt32(res.Value, CultureInfo.InvariantCulture);
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2627")
                    throw new EAltMessage(ErrorMsg.EDcUniqueUserName);
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53200")
                    throw new EAltMessage(err.Message);
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53201")
                    throw new EAltMessage(err.Message);
                else
                    throw EAlternate.CreateException(err, new EAltDb(EInsUser));
            }
        }
        public int UpdateUser(User u)
        {
            string sql = "pla.dc_user_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.Int, u.LINK, sqlCmd, false);
                WFSql.DB.AddInParameter("@name", SqlDbType.VarChar, u.S_NAME, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_login", SqlDbType.VarChar, u.F_LOGIN, sqlCmd, false);
                if (!String.IsNullOrEmpty(u.S_PASS))
                    WFSql.DB.AddInParameter("@pass", SqlDbType.VarChar, u.S_PASS, sqlCmd, false);
                WFSql.DB.AddInParameter("@pref", SqlDbType.VarChar, u.PREF, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
                return u.LINK;
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "2627")
                    throw new EAltMessage(ErrorMsg.EDcUniqueUserName);
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53200")
                    throw new EAltMessage(err.Message);
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "53201")
                    throw new EAltMessage(err.Message);
                else
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EDcSaveUser));
            }
        }
        public void DeleteUser(int[] idx)
        {
            string sql = "pla.dc_user_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                foreach (int item in idx)
                {
                    sqlCmd.Parameters["@link"].Value = item;
                    sqlCmd.ExecuteNonQuery();
                }
                
                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                if (err.Data != null && err.Data["HelpLink.EvtID"].ToString() == "547")
                    throw new EAltMessage(EUsedObject);
                else
                    throw EAlternate.CreateException(err, new EAltDb(EUserLoad)); 
            }
        }
        public DataTable UserLoad(int id)
        {
            string sql = "pla.dc_user_load";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.Int, id, sqlCmd, false);

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                dt.Clear();
                da.Fill(dt);
                WFSql.DB.Commit();

                return dt;
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err, new EAltDb(EUserLoad));
            }

        }

        #region Errors

        const string EUserLoad = "Ошибка загрузки данных пользователя из базы.";
        const string EUserDelete = "Ошибка удаления пользователя.";
        const string EUsedObject = "Значение не может быть удалено, поскольку имеет связанные с ним данные.";
        const string EInsUser = "Ошибка при сохранении пользователя в базе данных.";

        #endregion
    }
}
