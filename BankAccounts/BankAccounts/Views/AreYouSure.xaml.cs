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

namespace BankAccounts.Views
{
    /// <summary>
    /// Interaction logic for AreYouSure.xaml
    /// </summary>
    public partial class AreYouSure : Window
    {
        public bool Answer { get; set; }
        public AreYouSure(string title, string Question)
        {
            InitializeComponent();
            this.Height = 0;
            DoubleAnimation OpactytyThisWindow = new DoubleAnimation(150, new Duration(TimeSpan.FromSeconds(0.2)));
            this.BeginAnimation(HeightProperty,OpactytyThisWindow);
            Maintitle.Text = title;
            question.Text = Question;
        }
        private void Cross(object sender, RoutedEventArgs e)
        {
            Answer = false;
            Continues();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Answer= true;
            Continues();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Answer= false;
            Continues();
        }

        private void Continues()
        {
            DoubleAnimation OpactytyThisWindow = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.2)));
            OpactytyThisWindow.Completed += CloseThis;
            this.BeginAnimation(HeightProperty, OpactytyThisWindow);
            
        }

        private void CloseThis(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
