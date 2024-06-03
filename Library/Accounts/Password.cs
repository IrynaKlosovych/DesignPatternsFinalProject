using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Accounts
{
    public static class Password
    {
        private static readonly int _minimumLength;
        public static bool IsValidPass(string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return password.Length >= _minimumLength && HasLetterAndDigit(password);
        }

        private static bool HasLetterAndDigit(string password)
        {
            bool hasDigit = password.Any(char.IsDigit);
            bool hasLetter = password.Any(char.IsLetter);

            return hasDigit && hasLetter;
        }

        public static bool ComparePasswords(string? password1, string? password2)
        {
            if (password1 == null || password2 == null)
            {
                return false;
            }
            return password1.Equals(password2);
        }
    }
}
