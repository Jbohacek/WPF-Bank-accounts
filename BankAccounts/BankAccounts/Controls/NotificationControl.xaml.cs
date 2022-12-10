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
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public LoginWindow? MyLoginWindow { get; set; }
        public RegisterControl? MyRegisterControl { get; set; }
        public NotificationControl()
        {
            InitializeComponent();
            
        }
        public void SetUserName()
        {
            if (MyRegisterControl != null)
            {
                BoxShowUserName.Text = MyRegisterControl.BoxLastName.Text.ToLower() + MyRegisterControl.BoxFirstName.Text.ToLower();
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if(MyLoginWindow != null)
            {
                MyLoginWindow.MyNotification.Visibility = Visibility.Hidden;
                MyLoginWindow.MyLogin.Visibility = Visibility.Visible;
            }
            

        }
    }
}
