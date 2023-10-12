using System;
using System.Collections.Generic;
using System.Text;

namespace Individuellt_projekt
{
    internal class User //Användare-klass och dess properties
    {
        public string UserName { get; private set; }
        public int UserPinCode { get; private set; }
        public List<BankAccount> Accounts { get; private set; }

        public User(string userName, int userPinCode, List<BankAccount> accounts) //Användare konstruktor
        {
            UserName = userName.ToUpper();
            UserPinCode = userPinCode;
            Accounts = accounts;

        }
    }
}
