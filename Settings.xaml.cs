using Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        IFilesRepo repo;
        public Settings()
        {
            InitializeComponent();

            repo = RepoFactory.GetRepo();

            int[] sett = { repo.GetSettingsforWpf()[0], repo.GetSettingsforWpf()[1], repo.GetSettingsforWpf()[2] };

            repo.SetSettingsforWpf(sett);

            repo.InitializeSettingsforWpf();

            SetCulture(repo.GetSettingsforWpf()[1]);
            cbChamp.SelectedIndex = repo.GetSettingsforWpf()[0];
            cbLang.SelectedIndex = repo.GetSettingsforWpf()[1];
            cbMainSize.SelectedIndex = repo.GetSettingsforWpf()[2];
        }

        private void SetCulture(int langChoice)
        {
            var cultureCode = langChoice switch
            {
                0 => "hr-HR",
                1 => "",
                _ => Thread.CurrentThread.CurrentCulture.Name 
            };

            var cultureInfo = new System.Globalization.CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            RefreshUI();
        }

        private void RefreshUI()
        {
            InitializeComponent();
            repo.InitializeSettingsforWpf();
            cbChamp.SelectedIndex = repo.GetSettingsforWpf()[0];
            cbLang.SelectedIndex = repo.GetSettingsforWpf()[1];
            cbMainSize.SelectedIndex = repo.GetSettingsforWpf()[2];
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var tempchamchoice = repo.GetSettingsforWpf()[0];
            var langChoice = cbLang.SelectedIndex;
            var chamChoice = cbChamp.SelectedIndex;
            var sizeChoice = cbMainSize.SelectedIndex;

            var choices = new[] { chamChoice, langChoice, sizeChoice };

            repo.SetSettingsforWpf(choices);
            SetCulture(langChoice);

            if (tempchamchoice != chamChoice)
            {
                repo.ClearFavouriteTeam();
                repo.ClearFavourites();
            }

            Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            var langChoice = cbLang.SelectedIndex;
            var chamChoice = cbChamp.SelectedIndex;
            var sizeChoice = cbMainSize.SelectedIndex;

            var choices = new[] { chamChoice, langChoice, sizeChoice };

            repo.SetSettingsforWpf(choices);
            repo.InitializeSettingsforWpf();
            SetCulture(langChoice);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;

                case Key.Enter:
                    HandleEnterKey();
                    break;

                default:
                   
                    break;
            }
        }

        private void HandleEnterKey()
        {
            var previousChampChoice = repo.GetSettingsforWpf()[0];
            var langChoice = cbLang.SelectedIndex;
            var chamChoice = cbChamp.SelectedIndex;
            var sizeChoice = cbMainSize.SelectedIndex;

            var choices = new[] { chamChoice, langChoice, sizeChoice };

            repo.SetSettingsforWpf(choices);
            SetCulture(langChoice);

            if (previousChampChoice != chamChoice)
            {
                repo.ClearFavouriteTeam();
                repo.ClearFavourites();
            }

            Close();

        }
    }
}
