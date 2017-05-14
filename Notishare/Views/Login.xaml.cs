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
using Notishare.Model.DataTypes;
using Notishare.Model.HttpWorker;
using Notishare.ViewModel;
using NotiShare.Helper;

namespace Notishare.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = DataContext as LoginViewModel;
            var password = sender as PasswordBox;
            if (context != null)
            {
                context.PasswordString = password?.Password;
            }
        }

        private void ButtonBaseRegister_OnClick(object sender, RoutedEventArgs e)
        {
            var registerWindows = new RegistrationWindow();
            registerWindows.ShowDialog();
        }

        private async void ButtonBaseLogin_OnClick(object sender, RoutedEventArgs e)
        {
            var context = DataContext as LoginViewModel;
            string result = string.Empty;
            if (context != null)
            {
                var loginObject = new LoginObject
                {
                    Email = context.EmailString,
                    PasswordHash = HashHelper.GetHashString(context.PasswordString)

                };
                result = await HttpWorker.Instance.Login(loginObject);
            }
            if (result.Equals("Welcome"))
            {
                var mainWindow = new MainWindow(context?.EmailString);
                mainWindow.Show();
                this.Close();
            }
        }
    }   
}
