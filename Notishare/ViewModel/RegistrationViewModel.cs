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


        private string login;

        public string LoginString
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string user;

        public string NameString
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }


        private string surname;

        public string SurnameString
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged();
            }
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
            if (CrossValidationHelper.CheckEmail(EmailString) && CrossValidationHelper.ValidatePasswordLenght(PasswordString) && CrossValidationHelper.ValidatePasswords(PasswordString, PasswordRepeatString))
            {
                return true;
            }
            return false;
        }


        private async Task<string> Register()
        {
            var registerObject = new RegistrationObject
            {
                Name = NameString,
                Surname = SurnameString,
                UserName = LoginString,
                Email = EmailString,
                PasswordHash = HashHelper.GetHashString(PasswordString)
            };
            return await HttpWorker.Instance.RegisterUser(registerObject);
        }
    }
}
