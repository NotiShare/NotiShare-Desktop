using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notishare.Commands;
using NotiShareModel.DataTypes;
using NotiShareModel.CrossHelper;
using NotiShareModel.HttpWorker;

namespace Notishare.ViewModel
{
    public class LoginViewModel :BaseViewModel
    {


        public LoginViewModel()
        {
            CanLogin = false;
            IsWorking = false;
            LoginCommand = new AsyncCommand(async () =>
            {
                IsWorking = true;
                var result = await Login();
                IsWorking = false;
            }); 
        }

        private string userString;

        public string UserString
        {
            get { return userString; }
            set
            {
                userString = value;
                OnPropertyChanged();
            }
        }

        private string passwordString;
        public string PasswordString
        {
            get { return passwordString; }
            set
            {           
                passwordString = value;
                CanLogin = IsCanEnableButton();
                OnPropertyChanged();
            }
        }


        private bool canLogin;

        public bool CanLogin
        {
            get { return canLogin; }
            set
            {
                canLogin = value;
                OnPropertyChanged();
            }
        }


        private bool isWorking;

        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                OnPropertyChanged();
            }

        }

        public ICommand LoginCommand { get; set; }


        private async Task<LoginResult> Login()
        {
            
            var loginObject = new LoginObject
            {
                UserName = UserString,
                PasswordHash = HashHelper.GetHashString(PasswordString)

            };
            var result = await HttpWorker.Instance.Login(loginObject);
            return result;
        }


        private bool IsCanEnableButton()
        {
            if  (CrossValidationHelper.ValidatePasswordLenght(PasswordString))
            {
                return true;
            }
            return false;
        }
    }
}
