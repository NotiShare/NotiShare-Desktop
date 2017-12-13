using System.Windows;
using System.Windows.Controls;
using NotiShareModel.DataTypes;
using Notishare.ViewModel;
using NotiShareModel.CrossHelper;
using NotiShareModel.HttpWorker;

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
            LoginResult result = null;
            if (context != null)
            {
                var loginObject = new LoginObject
                {
                    UserName = context.UserString,
                    PasswordHash = HashHelper.GetHashString(context.PasswordString)

                };
                result = await HttpWorker.Instance.Login(loginObject);
                if (result.Message.Equals("Welcome"))
                {
                    var mainWindow = new MainWindow(result.UserId);
                    mainWindow.Show();
                    this.Close();
                }
            }
            
        }
    }   
}
