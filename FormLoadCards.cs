using Alternative.Model;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative
{
    public partial class FormLoadCards : Form
    {
        public FormLoadCards(Project pr)
        {
            InitializeComponent();
            if (pr == null)
                pr = new Project();
            else
            {
                _pr = pr;
                tbProjectName.Text = _pr.Name;
                tbProjectNumber.Text = _pr.Number;
            }
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ofdLoadFile.InitialDirectory = "c:\\";
                ofdLoadFile.Filter = "Файл карточек (*.csv)|*.csv|All files (*.*)|*.*";
                ofdLoadFile.FilterIndex = 1;
                ofdLoadFile.RestoreDirectory = true;

                if (ofdLoadFile.ShowDialog() == DialogResult.OK)
                    tbPath.Text = ofdLoadFile.FileName;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this._cardsFile = new CardsFile(tbPath.Text, _pr);
                _cardsFile.Check();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tbPath_TextChanged(object sender, EventArgs e)
        {
            EnabledItem();
        }
        private void btnLoadToBase_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this._cardsFile = new CardsFile(tbPath.Text, _pr);
                if (_cardsFile.Check(false))
                    _cardsFile.LoadToDb();

                //На вызывающей форме будет обновлен список карточек проекта
                if (OnLoadCardEvent != null)
                    OnLoadCardEvent(null, null);

                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region Helper

        private void EnabledItem()
        { 
            bool b1 = String.IsNullOrEmpty(tbPath.Text.Trim());
            btnCheck.Enabled = !b1;
            btnLoadToBase.Enabled = !b1;
        }
        #endregion

        private Project _pr;
        private CardsFile _cardsFile;

        public event EventHandler OnLoadCardEvent;

    }

}
