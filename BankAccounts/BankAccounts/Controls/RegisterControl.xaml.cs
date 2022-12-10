using BankAccounts.Database.Tables;
using BankAccounts.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankAccounts.Controls
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public LoginWindow? MyLoginWindow { get; set; }
        public DatabaseHandler? MyDatabase { get; set; }

        public RegisterControl()
        {
            InitializeComponent();

        }

        private void BirthDate_Loaded(object sender, RoutedEventArgs e)
        {
            var textbox1 = (TextBox)BirthDate.Template.FindName("PART_TextBox", BirthDate);
            textbox1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00ADB5"));
            textbox1.CaretBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEEEEE"));

        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            bool end = false;

            if (BoxFirstName.Text == "") { BoxFirstName.BorderBrush = new SolidColorBrush(Colors.Red); end = true; } else { BoxFirstName.BorderBrush = new SolidColorBrush(Colors.White); }
            if (BoxLastName.Text == "") { BoxLastName.BorderBrush = new SolidColorBrush(Colors.Red);end = true; } else { BoxLastName.BorderBrush = new SolidColorBrush(Colors.White); }
            if (BoxEmail.Text == "") { BoxEmail.BorderBrush = new SolidColorBrush(Colors.Red);end = true; } else { BoxEmail.BorderBrush = new SolidColorBrush(Colors.White); }

            if (BirthDate.SelectedDate == null) { BirthDate.BorderBrush = new SolidColorBrush(Colors.Red); end = true; } else { BirthDate.BorderBrush = new SolidColorBrush(Colors.Transparent); }
            if (BoxPassword.Password == null) { BoxPassword.BorderBrush = new SolidColorBrush(Colors.Red); end = true; } else { BoxPassword.BorderBrush = new SolidColorBrush(Colors.White); }
            if (BoxPassword.Password != BoxPasswordCheck.Password) { BoxPassword.BorderBrush = new SolidColorBrush(Colors.Red); BoxPasswordCheck.BorderBrush = new SolidColorBrush(Colors.Red); end = true; } else { BoxPassword.BorderBrush = new SolidColorBrush(Colors.White); BoxPasswordCheck.BorderBrush = new SolidColorBrush(Colors.White); }
            if (end) return;


            if (MyLoginWindow != null && MyDatabase != null)
            {
                Guid aa = Guid.NewGuid();
               

                Person person = new Person()
                {
                    FirstName = BoxFirstName.Text,
                    LastName = BoxLastName.Text,
                    Email = BoxEmail.Text,
                    Password = BoxPasswordCheck.Password,
                    BirthDate = BirthDate.SelectedDate,
                    Balance = 0,
                    IdAccount = aa.ToString()
                };

                 MyDatabase.InsertValue<Person>(person);

                 MyLoginWindow.MyRegister.Visibility = Visibility.Hidden;
                 MyLoginWindow.MyNotification.SetUserName();
                 MyLoginWindow.MyNotification.Visibility = Visibility.Visible;
            }
           

            


        }
    }
}
