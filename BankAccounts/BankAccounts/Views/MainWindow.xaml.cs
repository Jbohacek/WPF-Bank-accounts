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
using BankAccounts.Views;
using BankAccounts.Pages;
using BankAccounts.Controls;
using System.Diagnostics;

namespace BankAccounts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        private static Person? theChosen;
        public static Person Chosen { get { if (theChosen == null) return new Person(); else return theChosen; } set { theChosen = value; } }

        public string DispleyTextShow { set { BoxInfo.Text = value; } get { return BoxInfo.Text; } }

        private double WidhtMainPageViewer { get; set; }

        public MainWindow(Person ChosenOne)
        {
            InitializeComponent();

            this.Opacity = 0;
            DoubleAnimation ShowThisWindow = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.2)));
            this.BeginAnimation(OpacityProperty, ShowThisWindow);


            Chosen = ChosenOne;
            //BoxFullName.Text = Chosen.FullName;
            //DispleyTextShow = "Welcome!";

            this.DataContext = Chosen;

            ShowPage();
            MainPageViewer.Content = new Overview();

            Reload();

        }
        public void Reload()
        {
            BoxFullName.Text = Chosen.FullName;
            BoxInfo.Text = Chosen.GetBalance;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
         
            if (e.LeftButton == MouseButtonState.Pressed)
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
            ShowLogout();
        }

        private void Log_Out_Click(object sender, RoutedEventArgs e)
        {
            ShowLogout();
        }

        private void ShowLogout()
        {
            AreYouSure check = new AreYouSure("Log out", "Do you want to log out?");
            check.ShowDialog();

            if (check.Answer == true)
            {
                DoubleAnimation ShowThisWindow = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.6)));
                this.BeginAnimation(WidthProperty, ShowThisWindow);
                DoubleAnimation OpactytyThisWindow = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2)));
                OpactytyThisWindow.BeginTime = TimeSpan.FromSeconds(0.4);
                OpactytyThisWindow.Completed += OpenLoginAgin;
                this.BeginAnimation(OpacityProperty, OpactytyThisWindow);
            }
            else
            {

            }
        }


        private void OpenLoginAgin(object? sender, EventArgs e)
        {
            LoginWindow NewWindow = new LoginWindow();
            NewWindow.Show();
            this.Close();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            ShowPage();
            MainPageViewer.Content = new Options();
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Payments_Click(object sender, RoutedEventArgs e)
        {
            ShowPage();
            MainPageViewer.Content = new Payments(this);
        }

        private void Cards_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            ShowPage();
            MainPageViewer.Content = new Overview();
        }

        private void ShowPage()
        {

            DoubleAnimation WidthPageView = new DoubleAnimation(1000, new Duration(TimeSpan.FromSeconds(0.1)))
            { From = 0 };

            DoubleAnimation OpacityPageView = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3)))
            { From = 0 };

            MainPageViewer.BeginAnimation(WidthProperty, WidthPageView);
            MainPageViewer.BeginAnimation(OpacityProperty, OpacityPageView);


            //MainPageViewer.Content = new T();
        }

        

        int HowFarWillGo = 20;
        bool CanShowSave = true;
        public void ShowSave()
        {
            if (!CanShowSave) return;
            CanShowSave= false;
            SaveCheckShow.Visibility = Visibility.Visible;
            ThicknessAnimation MarginSaveUp = new ThicknessAnimation(new Thickness(0, 0, 0, HowFarWillGo), new Duration(TimeSpan.FromSeconds(0.2)))
            {
                From = new Thickness(0, 0, 0, -60),

            };
            MarginSaveUp.Completed += MarginSaveUp_Completed;


            SaveCheckShow.BeginAnimation(MarginProperty, MarginSaveUp);

        }
        private void MarginSaveUp_Completed(object? sender, EventArgs e)
        {
            ThicknessAnimation MarginSaveDown = new ThicknessAnimation(new Thickness(0, 0, 0, -60), new Duration(TimeSpan.FromSeconds(0.2)))
            {
                From = new Thickness(0, 0, 0, HowFarWillGo),
                BeginTime = TimeSpan.FromSeconds(1)
            };
            MarginSaveDown.Completed += MarginSaveDown_Completed;

            SaveCheckShow.BeginAnimation(MarginProperty, MarginSaveDown);
        }
        private void MarginSaveDown_Completed(object? sender, EventArgs e)
        {
            SaveCheckShow.Visibility = Visibility.Hidden;
            CanShowSave = true;
        }
        

        private void Personal_Click(object sender, RoutedEventArgs e)
        {
            ShowPage();
            MainPageViewer.Content = new Personal(this);
        }
    }
}
