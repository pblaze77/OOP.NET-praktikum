

using Dao;
using System.Drawing.Text;

namespace WinFormsApp
{
    public partial class StartSettings : Form
    {
        IFilesRepo repo;
        public StartSettings()
        {

            InitializeComponent();


            repo = RepoFactory.GetRepo();

            int[] sett = { repo.GetSettings()[0], repo.GetSettings()[1] };

            repo.SetSettings(sett);

            repo.InitializeSettings();

            SetCulture(repo.GetSettings()[1]);
            cbChampionship.SelectedIndex = repo.GetSettings()[0];
            cbLanguage.SelectedIndex = repo.GetSettings()[1];

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            int langChoice = cbLanguage.SelectedIndex;

            int chamChoice = cbChampionship.SelectedIndex;



            int[] choices = { chamChoice, langChoice };

            repo.SetSettings(choices);

            repo.InitializeSettings();

            SetCulture(langChoice);

        }

        private void SetCulture(int langChoice)
        {
            var cultureCode = repo.GetSettings()[1] switch
            {
                0 => "hr-HR",
                1 => "",
                _ => Thread.CurrentThread.CurrentCulture.Name
            };

            RefreshUI();
        }

        private void RefreshUI()
        {
            Controls.Clear();
            InitializeComponent();
            repo.InitializeSettings();
            cbChampionship.SelectedIndex = repo.GetSettings()[0];
            cbLanguage.SelectedIndex = repo.GetSettings()[1];

        }


        private void btnNext_Click(object sender, EventArgs e)
        {

            var tempchamchoice = repo.GetSettings()[0];
            var langChoice = cbLanguage.SelectedIndex;
            var chamChoice = cbChampionship.SelectedIndex;

            var choices = new[] { chamChoice, langChoice };

            repo.SetSettings(choices);
            SetCulture(langChoice);

            if (tempchamchoice != chamChoice)
            {
                repo.ClearFavouriteTeam();
                repo.ClearFavourites();
            }

            Close();

        }

        private void StartSettings_Load(object sender, EventArgs e)
        {
        }

        private void cbChampionship_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            if (keyData == Keys.Enter)
            {
                var tempchamchoice = repo.GetSettings()[0];
                var langChoice = cbLanguage.SelectedIndex;
                var chamChoice = cbChampionship.SelectedIndex;

                var choices = new[] { chamChoice, langChoice };

                repo.SetSettings(choices);
                SetCulture(langChoice);

                if (tempchamchoice != chamChoice)
                {
                    repo.ClearFavouriteTeam();
                    repo.ClearFavourites();
                }

                Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
