using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class ValidateData
    {
        public bool ValidationEmailFormat(String email)
        {
            if (Regex.IsMatch(email, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$"))
            {
                return true;
            }
            return false;
        }

        public bool ValidationUsernameFormat(string username)
        {
            if ((Regex.IsMatch(username, @"^[^ ][a-zA-Z 0-9]+[^ ]$")))
            {
                return true;
            }
            return false;
        }


    }
}
