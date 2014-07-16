using Alternative.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Alternative.DB
{
    /// <summary>
    /// Класс предоставляет доступ к базе данных проекта
    /// </summary>
    public class ProjectGate
    {
        public static void Insert(Int64 f_payment, string s_name, Int64 f_juristic, string s_number, int f_design)
        {
            string sql = "pla.pr_ins";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@f_payment", SqlDbType.BigInt, f_payment, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_name", SqlDbType.VarChar, s_name, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_juristic", SqlDbType.BigInt, f_juristic, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_number", SqlDbType.VarChar, s_number, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_design", SqlDbType.Int, f_design, sqlCmd, false);
    
                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EPrInsert));
            }
        }
        public static void Update(Int64 link, Int64 f_payment, string s_name, Int64 f_juristic, string s_number, int f_design)
        {
            string sql = "pla.pr_edit";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.BigInt, link, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_payment", SqlDbType.BigInt, f_payment, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_name", SqlDbType.VarChar, s_name, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_juristic", SqlDbType.BigInt, f_juristic, sqlCmd, false);
                WFSql.DB.AddInParameter("@s_number", SqlDbType.VarChar, s_number, sqlCmd, false);
                WFSql.DB.AddInParameter("@f_design", SqlDbType.Int, f_design, sqlCmd, false);

                sqlCmd.ExecuteNonQuery();

                WFSql.DB.Commit();
            }
            catch(Exception err)
            {
                WFSql.DB.Rollback();
                throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EPrUpdate));
            }
        
        }
        public static void Delete(Int64[] ids)
        {
            string sql = "pla.pr_del";

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@link", SqlDbType.Int);
                foreach (Int64 item in ids)
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
                    throw EAlternate.CreateException(err, new EAltDb(ErrorMsg.EPrDelete));

            }
        }
        public static DataTable Load(Int64 link)
        {
            string sql = "pla.pr_load";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                WFSql.DB.AddInParameter("@link", SqlDbType.BigInt, link, sqlCmd, false);


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
        public static DataTable List()
        {
            string sql = "pla.pr_project_list";
            DataTable dt = new DataTable();

            WFSql.DB.StartTransaction();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sql, WFSql.DB.SqlConnection, WFSql.DB.SqlTransaction);
                sqlCmd.CommandType = CommandType.StoredProcedure;

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

        #region Errors

        const string EUsedObject = "Проект не может быть удален, поскольку имеет связанные с ним данные.";

        #endregion
    }
}
