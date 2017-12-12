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

        private string emailString;

        public string EmailString
        {
            get { return emailString; }
            set
            {
                emailString = value;
                OnPropertyChanged();
                CanLogin = IsCanEnableButton();
            }
        }

        private string passwordString;
        public string PasswordString
        {
            get { return passwordString; }
            set
            {           
                passwordString = value;
                OnPropertyChanged();
                CanLogin = IsCanEnableButton();
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


        private async Task<string> Login()
        {
            
            var loginObject = new LoginObject
            {
                Email = EmailString,
                PasswordHash = HashHelper.GetHashString(PasswordString)

            };
            var result = await HttpWorker.Instance.Login(loginObject);
            return result;
        }


        private bool IsCanEnableButton()
        {
            if (CrossValidationHelper.CheckEmail(EmailString) && CrossValidationHelper.ValidatePasswordLenght(PasswordString))
            {
                return true;
            }
            return false;
        }
    }
}
