using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NotiShare.Helper
{
    internal static class ValidationHelper
    {

        internal static bool CheckEmail(string inputEmail)
        {
            if (string.IsNullOrEmpty(inputEmail))
            {
                return false;
            }
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(inputEmail);
            return match.Success;
        }


        internal static bool ValidatePasswordLenght(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            return password.Length > 5 && password.Length < 17;
        }


        internal static bool ValidatePasswords(string originalPassword, string repeatedPassword)
        {
            if (string.IsNullOrEmpty(originalPassword) || string.IsNullOrEmpty(repeatedPassword))
            {
                return false;
            }
            return originalPassword.Equals(repeatedPassword);
        }
    }
}