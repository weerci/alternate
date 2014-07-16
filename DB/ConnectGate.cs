using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CoreCommon;
using System.Data.SqlClient;
using System.Globalization;
using Alternative.AltServSpace;

namespace Alternative.DB
{
   
    public class ConnectGate
    {
        public static void Connect(string user, string password)
        {
            AlternateClient ac = new AlternateClient();
            string result = ac.Login(null, user, password);
            string s;
            if (Query.ParseResult(out s, ref result) == 0)
            {
                throw new EAltMessage(result);
            }
            else
                _cert = result;
        }
        public static string Certificate
        {
            get { return _cert; }
        }

        private static string _cert;

   }
}
