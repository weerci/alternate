using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alternative.DB
{
    public class Query
    {
        #region Constructor
        public Query(List<string> param, string ns, string method)
        {
            this._param = param;
            this._ns = ns;
            this._method = method;
        }

        #endregion

        #region Methods
        public string AsXML()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("class");
                    writer.WriteAttributeString("name",_ns);
                    writer.WriteStartElement("method");
                        writer.WriteAttributeString("name", _method);

                        if (_param != null)
                            foreach (string method in _param)
                            {
                    
                                writer.WriteStartElement("param");
                                writer.WriteAttributeString("name", method.Split('=')[0]);
                                writer.WriteAttributeString("value",  method.Split('=')[1]);
                                writer.WriteEndElement();
                            }
                    writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return sb.ToString();
        }
        
        public static int ParseResult(DataTable dt, ref string result)
        {

            if (result.StartsWith("!!!!Err;"))
            {
                result = result.Substring(8);
                return 0;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.ReadXml(XmlReader.Create(new StringReader(result)));
                ds.Tables[0].TableName = "result";
                dt = ds.Tables[0];
                return 1;
            }
        }
        public static int ParseResult(out string s, ref string result)
        {

            if (result.StartsWith("!!!!Err;"))
            {
                result = result.Substring(8);
                s = String.Empty;
                return 0;
            }
            else
            {
                s = result;
                return 1;
            }
        }

        #endregion

        #region Fields

        private List<string> _param;
        private string _ns;
        private string _method;

        #endregion
    }
}
