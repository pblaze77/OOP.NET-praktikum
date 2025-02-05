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
    public partial class PlayerUC : UserControl
    {
        IFilesRepo repo;

        public PlayerUC()
        {
            repo = RepoFactory.GetRepo();

            SetLanguage(repo);

            InitializeComponent();
        }

        private void SetLanguage(IFilesRepo repo)
        {

            var cultureCode = repo.GetSettings()[1] switch
            {
                0 => "hr-HR",
                1 => "",
                _ => Thread.CurrentThread.CurrentCulture.Name
            };


        }


        private void pbPlayer_Click(object sender, EventArgs e)
        {

        }

        private void PlayerUC_Load(object sender, EventArgs e)
        {

        }

        private void lbName_Click(object sender, EventArgs e)
        {

        }
    }
}
