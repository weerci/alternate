using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Alternative
{
    public static class AltExtensions
    {
        public static void AddLog(this List<string> ls, string s, int indexStr = -1)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            try 
	        {	        
		        Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                if (indexStr != -1)
                    ls.Add(String.Format("{0} {1} стр.{2} {3}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), indexStr, s));
                else
                    ls.Add(String.Format("{0} {1} {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), s));
	        }
	        finally
	        {
                Thread.CurrentThread.CurrentCulture = originalCulture;
	        }
        }
    }   
}
