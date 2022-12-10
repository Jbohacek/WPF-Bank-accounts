using BankAccounts.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankAccounts.Controls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {


        public LoginWindow? MyLoginWindow { get; set; }
        public DatabaseHandler? MyDatabase { get; set; }


        public LoginControl()
        {
            InitializeComponent();
        }

        private void ButtonCreateAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MyLoginWindow != null)
            {
                MyLoginWindow.MyLogin.Visibility = Visibility.Hidden;
                MyLoginWindow.MyRegister.Visibility = Visibility.Visible;
            }
        }
    }
}
