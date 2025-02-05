using Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class CustomMessageBox : Form
    {
        IFilesRepo repo;
        public CustomMessageBox()
        {
            repo = RepoFactory.GetRepo();
            SetLanguage(repo);
            repo.InitializeSettings();
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

       

        private void SetLanguage(IFilesRepo repo)
        {
            var cultureCode = repo.GetSettings()[1] switch
            {
                0 => "hr-HR",
                1 => "",
                _ => Thread.CurrentThread.CurrentCulture.Name 
            };

            var cultureInfo = new System.Globalization.CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

        }

        

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
        }
    }
}
