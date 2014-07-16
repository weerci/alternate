using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alternative
{
    public static class AltCommon
    {
        public static string Find(string text)
        {
            string result = "";
            if (text.ToUpper().Contains("ЗАПРЕЩЕНО РАЗРЕШЕНИЕ"))
                result = ErrorMsg.EAdmRightError;

            return result;
        }

        public const int VARCHAR_SIZE_1 = 512;
    }
}
