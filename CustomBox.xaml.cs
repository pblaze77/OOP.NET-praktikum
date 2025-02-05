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
    /// Interaction logic for CustomBox.xaml
    /// </summary>
    public partial class CustomBox : Window
    {
        IFilesRepo repo;
        public CustomBox()
        {
            repo = RepoFactory.GetRepo();
            SetLanguge();
            repo.InitializeSettingsforWpf();
            InitializeComponent();
        }

        private void SetLanguge()
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

        public EventHandler<bool> OnConfirmation;
        public bool UserDecision { get; set; }


        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            UserDecision = false;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            UserDecision = true;
            Close();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    UserDecision = false;
                    Close();
                    break;

                case Key.Enter:
                    UserDecision = true;
                    Close();
                    break;

                default:
                    break;
            }
        }
    }
}
