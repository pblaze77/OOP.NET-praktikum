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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TeamInfo.xaml
    /// </summary>
    public partial class TeamInfo : Window
    {
        public TeamInfo()
        {
            InitializeComponent();
        }

        public void SetLabelValues(string[] values)
        {
            if (values.Length != 9)
                throw new ArgumentException("Values array must contain exactly 9 elements.");

            ValueLabel1.Content = values[0];
            ValueLabel2.Content = values[1];
            ValueLabel3.Content = values[2];
            ValueLabel4.Content = values[3];
            ValueLabel5.Content = values[4];
            ValueLabel6.Content = values[5];
            ValueLabel7.Content = values[6];
            ValueLabel8.Content = values[7];
            ValueLabel9.Content = values[8];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };

            BeginAnimation(OpacityProperty, fadeInAnimation);
        }
    }
}
