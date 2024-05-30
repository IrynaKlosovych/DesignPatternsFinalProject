using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Accounts
{
    public static class Password
    {
        public static bool IsValidPass(string? password)
        {
            if(password == null) return false;
            if (password.Length < 7)
            {
                return false;
            }
            bool hasDigit = false;
            bool hasLetter = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
            }
            return hasDigit && hasLetter;
        }

        public static bool ComparePasswords(string? password1, string? password2)
        {
            if(password1==null || password2==null) return false;
            return password1.Equals(password2);
        }
    }
}
