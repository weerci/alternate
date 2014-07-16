using CoreCommon;
using DbSQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative
{
    class SQLite : DbFacadeSQLite
    {
        
        #region Construcotors
        public static SQLite Item
        {
            get
            {
                if (_db == null)
                {
                    lock (_syncRoot)
                    {
                        if (_db == null)
                            _db = new SQLite(Path.ChangeExtension(Application.ExecutablePath, ".db3"));
                    }
                }
                return _db;
            }
        }
        private SQLite(string fileName) : base(fileName)
        {
            if (!File.Exists(fileName) || new FileInfo(fileName).Length == 0)
            {
                base.ExecuteNonQuery(@"CREATE TABLE [errors] (
                    [id] integer PRIMARY KEY AUTOINCREMENT NOT NULL,
                    [dt] DateTime NOT NULL,
                    [etype] TEXT NOT NULL,
                    [etext] TEXT NOT NULL,
                    [estack] TEXT NOT NULL
                    )");
            }
        }
        
        #endregion
     
        public void ClearErrorDb(int dt)
        {
            ParametersCollection parameters = new ParametersCollection();
            parameters.Add("dt", DateTime.Today.AddDays(dt), DbType.Date);
            SQLite.Item.Delete("errors", "dt < @dt", parameters);
        }
   
        #region Fields

        private static SQLite _db;
        private static object _syncRoot = new Object();

        #endregion
    }
}
