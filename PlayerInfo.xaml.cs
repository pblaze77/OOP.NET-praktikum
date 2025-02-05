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
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {
        public PlayerInfo()
        {
            InitializeComponent();
        }

        public void SetLabelValues(string[] values)
        {
            if (values.Length != 6)
                throw new ArgumentException("Values array must contain exactly 9 elements.");

            lbNameOut.Content = values[0];
            lbNumberOut.Content = values[1];
            lbPositionOut.Content = values[2];
            lbCaptainOut.Content = values[3];
            lbScoredGoalsOut.Content = values[4];
            lbYellowCardsOut.Content = values[5];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            var scaleTransform = new ScaleTransform(0.5, 0.5);
            MainGrid.RenderTransform = scaleTransform;
            MainGrid.RenderTransformOrigin = new Point(0.5, 0.5);

           
            var scaleAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

         
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

          
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            this.BeginAnimation(Window.OpacityProperty, opacityAnimation);
        }
    }
 }
