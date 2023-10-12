using System;
using System.Collections.Generic;
using System.Text;

namespace Individuellt_projekt
{
    internal class BankAccount //Bankkonto-klass och dess properties
    {
        public string AccountName { get; private set; }
        public double AccountBalance { get; set; }

        public BankAccount(string accountName, double accountBalance) //Bankkonto konstruktor
        {
            AccountName = accountName;
            AccountBalance = accountBalance;
        }
    }
}
