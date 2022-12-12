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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankAccounts.Database.Tables;

namespace BankAccounts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
  

    public partial class MainWindow : Window
    {
        public Person Chosen { get; set; }
        
        public string DispleyTextShow { set { BoxInfo.Text = value; } get { return BoxInfo.Text; } }

        public MainWindow(Person ChosenOne)
        {
            InitializeComponent();
            this.Opacity = 0;
            DoubleAnimation ShowThisWindow = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2)));
            this.BeginAnimation(OpacityProperty, ShowThisWindow);
            Chosen = ChosenOne;
            BoxFullName.Text = Chosen.FullName;
            DispleyTextShow = "Welcome!";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Minimaze(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cross(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation ShowThisWindow = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.6)));
            this.BeginAnimation(WidthProperty, ShowThisWindow);
        }
    }
}
