using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Common
{

    public static class ValidationHelper
    {
        public static bool FullNameHasNoDigits(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return false;
            }

            return !Regex.IsMatch(fullName, @"\d");
        }

  
        public static bool EmailHasValidAt(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            email = email.Trim();
            int at = email.IndexOf('@');
            if (at <= 0 || at >= email.Length - 1)
            {
                return false;
            }

            return true;
        }

        public static bool IsAgeInRange(int age, int min, int max)
        {
            return age >= min && age <= max;
        }
    }
}
