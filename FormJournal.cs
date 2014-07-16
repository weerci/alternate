using Alternative.DB;
using Alternative.Model;
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
    public partial class FormJournal : Form
    {
        public FormJournal(Card card)
        {
            InitializeComponent();
            this._card = card;
        }
        private void FormJournal_Load(object sender, EventArgs e)
        {
            tbLinkCard.Text = _card.LINK.ToString();
            tbNumCard.Text = _card.N_NUMBER.ToString();
            Card.Journal(_card.LINK, dgvJournal);

            //Период устанавливается равным прошлому месяцу
            int yr = DateTime.Today.Year;
            int mth = DateTime.Today.Month;
            dtpFrom.Value = new DateTime(yr, mth, 1).AddMonths(-1); 
            dtpTo.Value = new DateTime(yr, mth, 1).AddDays(-1);
        }
        private void btnUnload_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    DataTable dt = CardGate.JournalToExcel(_card.LINK, dtpFrom.Value, dtpTo.Value);
                    SaveToFile(dt);
                }
                catch (Exception err)
                {
                    throw EAlternate.CreateException(err, new EAltModel());
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnToday_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    DataTable dt = CardGate.JournalToExcel(_card.LINK, DateTime.Today, DateTime.Today.AddDays(1));
                    SaveToFile(dt);
                }
                catch (Exception err)
                {
                    throw EAlternate.CreateException(err, new EAltModel());
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnYestoday_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    DataTable dt = CardGate.JournalToExcel(_card.LINK, DateTime.Today, DateTime.Today.AddDays(-1));
                    SaveToFile(dt);
                }
                catch (Exception err)
                {
                    throw EAlternate.CreateException(err, new EAltModel());
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void LoadToFile(DataTable dt, string path)
        {
            List<string> ls = new List<string>();
            foreach (DataRow row in dt.Rows)
                ls.Add(String.Format("{0};{1};{2};{3};{4}", row[0], row[1], row[2], row[3], row[4]));

            File.WriteAllLines(path, ls.ToArray());
        }
        private void SaveToFile(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                throw new EAltMessage(EJournalEmpty);
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = String.Format("Card_{0}_{1}_{2}", _card.N_NUMBER, dtpFrom.Value.ToShortDateString(), dtpTo.Value.ToShortDateString());
                sfd.Filter = "Файл карточек (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                    LoadToFile(dt, sfd.FileName);
            }
            catch (Exception err)
            {
                throw EAlternate.CreateException(err, new EAltDesign(String.Format(EJournalToExel, _card.N_NUMBER)));
            }
        }

        #region Fields

        private Card _card;

        #endregion

        #region Обработка ошибок

        const string ECardUnloadByFilter = "Не удалось выгрузить журнал для карты № {0}";
        const string EJournalToExel = "Не удалось сохранить журнал карты № {0}";
        const string EJournalEmpty = "Нет данных за выбранный период";
        
        #endregion

    }
}
