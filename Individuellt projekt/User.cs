using System;
using System.Collections.Generic;
using System.Text;

namespace Individuellt_projekt
{
    internal class User
    {
        public string UserName { get; set; }
        public int UserPinCode { get; set; }
        public List<BankAccount> Accounts { get; set; }

        public User(string userName, int userPinCode, List<BankAccount> accounts) 
        {
            UserName = userName.ToUpper();
            UserPinCode = userPinCode;
            Accounts = accounts;

        }
    }
}
