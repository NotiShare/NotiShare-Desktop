using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Notishare.Commands;
using Notishare.Model.DataTypes;
using Notishare.Model.HttpWorker;
using NotiShare.Helper;

namespace Notishare.ViewModel
{
    public class RegistrationViewModel:BaseViewModel
    {

        public RegistrationViewModel()
        {
            CanRegister = false;
            IsWorking = false;
            RegistrationCommand = new AsyncCommand(async () =>
            {
                IsWorking = true;
                var result = await Register();
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
                CanRegister = IsCanEnableButton();
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
                CanRegister = IsCanEnableButton();
            }
        }


        private string passwordRepeatString;
        public string PasswordRepeatString
        {
            get { return passwordRepeatString; }
            set
            {
                passwordRepeatString = value;
                OnPropertyChanged();
                CanRegister = IsCanEnableButton();
            }
        }

        private bool canRegister;

        public bool CanRegister
        {
            get { return canRegister; }
            set
            {
                canRegister = value;
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

        public ICommand RegistrationCommand { get; set; }

        private bool IsCanEnableButton()
        {
            if (ValidationHelper.CheckEmail(EmailString) && ValidationHelper.ValidatePasswordLenght(PasswordString) && ValidationHelper.ValidatePasswords(PasswordString, PasswordRepeatString))
            {
                return true;
            }
            return false;
        }


        private async Task<string> Register()
        {
            var registerObject = new RegistrationObject
            {
                Email = EmailString,
                PasswordHash = HashHelper.GetHashString(PasswordString)
            };
            return await HttpWorker.Instance.RegisterUser(registerObject);
        }
    }
}
