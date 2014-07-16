using Alternative.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Alternative
{
    public partial class FormDesign : Form
    {
        public FormDesign(int id)
        {
            InitializeComponent();

            foreach (var item in typeof(PictureBoxSizeMode).GetFields(BindingFlags.Static | BindingFlags.Public))
                cbFormatPic.Items.Add(item.Name);
            cbFormatPic.SelectedIndex = 0;
            pbFacede.SizeMode = SetStretchImage();
            pbOutside.SizeMode = SetStretchImage();

            if (id == 0)
            {
                this.Text = CommonText.DesignFormHeaderCreate;
                btnCreate.Text = CommonText.PrBtnSave;
            }
            else
            {
                _design = Design.LoadById(id);
                tbDesignName.Text = _design.Name;
                pbFacede.Image = _design.Facade;
                pbOutside.Image = _design.Outside;

                this.Text = CommonText.DesignFormHeaderEdit;
                btnCreate.Text = CommonText.PrBtnUpdate;
            }
        }

        private void FormDesign_Resize(object sender, EventArgs e)
        {
            pbFacede.Width = this.Width / 2 - pbFacede.Left - 11;
            pbOutside.Left = pbFacede.Width + 24;
            pbOutside.Width = pbFacede.Width;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    switch (btn.Name)
                    {
                        case "btnLoadFacede":
                            ShowImage(pbFacede);
                            break;
                        case "btnLoadOutside":
                            ShowImage(pbOutside);
                            break;
                        default:
                            break;
                    }
                    ControlShow();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnDel_Image(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null &&
                MessageBox.Show(CommonText.MsgImageDel, CommonText.ObjectDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes
                )
            {
                switch (btn.Name)
                {
                    case "btnDelFacede":
                            PictureBoxClear(pbFacede);
                        break;
                    case "btnDelOfside":
                        PictureBoxClear(pbOutside);
                        break;
                    default:
                        break;
                }
                ControlShow();
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _design.Name = tbDesignName.Text; 
                _design.Facade = pbFacede.Image;
                _design.Outside = pbOutside.Image;
                if (pbFacede.Tag != null)
                    _design.PathToFacedeFile = pbFacede.Tag.ToString();
                if (pbOutside.Tag != null)
                    _design.PathToOutsideFile = pbOutside.Tag.ToString();

                int res = _design.Save();

                if (res != _design.LINK) // Форма была открыта в режиме создания
                {
                    tbDesignName.Text = "";
                    PictureBoxClear(pbFacede);
                    PictureBoxClear(pbOutside);
                    _design = new Design() { LINK = 0 };
                }
                else
                    this.Close();
                
                // Обновляется справочник дизайнов
                if (OnCreateDesignEvent != null)
                    OnCreateDesignEvent(null, new CreateDesignEventArgs(res));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tbDesignName_TextChanged(object sender, EventArgs e)
        {
            ControlShow();
        }
        private void cbFormatPic_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbFacede.SizeMode = SetStretchImage();
            pbOutside.SizeMode = SetStretchImage();
        }
        #region Helper

        private void ControlShow()
        {
            bool b1 = !String.IsNullOrEmpty(tbDesignName.Text.Trim());
            bool b2 = pbFacede.Image != null;
            bool b3 = pbOutside.Image != null;
            btnCreate.Enabled = b1 && b2 && b3;

        }
        private void PictureBoxClear(PictureBox pb)
        {
            if (pb.Image != null) pb.Image.Dispose();
            pb.Image = null;
            pb.Invalidate();
        }
        private void ShowImage(PictureBox pb)
        {
            ofdImage.InitialDirectory = "c:\\";
            ofdImage.Filter = "Файл изображения (*.bmp, *.jpg, *.jpeg, *.png, *.gif)|*.bmp; *.jpg; *.jpeg; *.png; *.gif|All files (*.*)|*.*";
            ofdImage.FilterIndex = 1;
            ofdImage.RestoreDirectory = true;

            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                if (pb.Image != null) pb.Image.Dispose();

                pb.SizeMode = SetStretchImage();
                Bitmap MyImage = new Bitmap(ofdImage.FileName);
                pb.Image = (Image)MyImage;
                pb.Tag = ofdImage.FileName;
            }
        }
        private PictureBoxSizeMode SetStretchImage()
        {
            //if (cbFormatPic.Items.Count > 0)
            //{
            //    switch (cbFormatPic.SelectedItem.ToString())
            //    {
            //        case "AutoSize":
            //            return PictureBoxSizeMode.AutoSize;
            //        case "CenterImage":
            //            return PictureBoxSizeMode.CenterImage;
            //        case "Normal":
            //            return PictureBoxSizeMode.Normal;
            //        case "Zoom":
            //            return PictureBoxSizeMode.Zoom;
            //        default:
            //            return PictureBoxSizeMode.StretchImage;
            //    }  
            //}
            //return PictureBoxSizeMode.Normal;
            return PictureBoxSizeMode.StretchImage;
        }

        #endregion

        #region Fields

        private Design _design = new Design();
        public event CreateDesignEventHandler OnCreateDesignEvent;
        public delegate void CreateDesignEventHandler(object sender, CreateDesignEventArgs e);

        #endregion


    }
    public class CreateDesignEventArgs
    {
        public CreateDesignEventArgs(int insIndex) { Index = insIndex; }
        public int Index { get; private set; } // readonly
    }

}
