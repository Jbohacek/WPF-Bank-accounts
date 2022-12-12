using BankAccounts.Database.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

namespace BankAccounts.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DatabaseHandler data { get; set; }
        public Person FoundPerson = new Person();
        public LoginWindow()
        {
            InitializeComponent();
            MyLogin.MyLoginWindow = this;
            MyRegister.MyLoginWindow = this;
            MyNotification.MyLoginWindow = this;
            MyNotification.MyRegisterControl = MyRegister;

            

            data = new DatabaseHandler("BankAccounts");

            var _AddADmin = data.Load<Person>();

            if (_AddADmin.FirstOrDefault(x => x.IdAccount == "A1") == null)
            {
                Person person = new Person()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "Admin@Admin.cz",
                    Password = "123456Ab",
                    BirthDate = DateTime.Now,
                    Balance = 9999999,
                    IdAccount = "A1"
                };
                data.InsertValue<Person>(person);
            }


            MyLogin.MyDatabase = data;
            MyRegister.MyDatabase = data;

           
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void Login(string username, string password)
        {
            
            var x = data.Load<Person>();
            
            var nasel = x.FirstOrDefault(x => (x.UserName == username) && (x.Password == password));
            if (nasel != null)
            {
                Debug.WriteLine("Nasel");
                DoubleAnimation heightani = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3)));

                DoubleAnimation opactizthis = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2)));
                opactizthis.BeginTime = TimeSpan.FromSeconds(0.3);
                opactizthis.Completed += RemoveLogin;

                this.BeginAnimation(OpacityProperty, opactizthis);
                MyLogin.BeginAnimation(HeightProperty, heightani);

                FoundPerson = nasel;

            }
            else
            {
                MyLogin.BoxUsername.BorderBrush = new SolidColorBrush(Colors.Red);
                MyLogin.BoxPassword.BorderBrush = new SolidColorBrush(Colors.Red);
            }

        }

        private void RemoveLogin(object? sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow(FoundPerson);
            mainWindow.Show();
            this.Close();
        }

        private void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login(MyLogin.BoxUsername.Text, MyLogin.BoxPassword.Password);
            }
        }

        private void Minimaze(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }

        private void Cross(object sender, RoutedEventArgs e)
        {
           Environment.Exit(0);
        }
    }
}
