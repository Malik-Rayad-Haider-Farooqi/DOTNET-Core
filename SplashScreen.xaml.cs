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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        private  async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = (Storyboard)this.Resources["FadeInStoryboard"];


            fadeIn.Begin(this);
            await System.Threading.Tasks.Task.Delay(3000);
            this.Close();
            
            

        }
    }
}
