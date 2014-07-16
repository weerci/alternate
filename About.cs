using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CoreCommon;
using System.Text;

namespace Alternative
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.Text = String.Format("{0} ", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            string[] arrayVersion = AssemblyVersion.Split('.');
            this.labelVersion.Text = String.Format("Версия {0} ", String.Format("{0}.{1}.{2}.{3}", arrayVersion[0], arrayVersion[1], arrayVersion[2], arrayVersion[3]));
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            string filePath = Path.ChangeExtension(Application.ExecutablePath, ".ver");
            try
            {
                Process.Start("notepad.exe", filePath);
            }
            catch(Exception err)
            {
                throw EAlternate.CreateException(err, new EAltDesign(EOpenVersionFile));
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://googledrive.com/host/0B49CBXu70uZAZnUta1htT19lNkE/");
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length != 0)
                {
                    StringBuilder s = new StringBuilder(((AssemblyDescriptionAttribute)attributes[0]).Description + "Список ресурсов:"+Environment.NewLine);

                    foreach (var refAsmName in Assembly.GetEntryAssembly().GetReferencedAssemblies())
                    {
                        s.Append(Assembly.Load(refAsmName).FullName + Environment.NewLine);
                    }
                    return s.ToString();
                }
                else
                    return "";
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        #region Errors

        const string EOpenVersionFile = "Ошибка открытия файла изменения версий.";

        #endregion
    }
}
