using BankAccounts.Database.Tables;
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

namespace BankAccounts.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DatabaseHandler data { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            MyLogin.MyLoginWindow = this;
            MyRegister.MyLoginWindow = this;
            MyNotification.MyLoginWindow = this;
            MyNotification.MyRegisterControl = MyRegister;

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

            data = new DatabaseHandler("BankAccounts");

            data.InsertValue<Person>(person);

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
    }
}
