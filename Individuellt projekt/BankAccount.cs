﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Individuellt_projekt
{
    internal class BankAccount
    {
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }

        public BankAccount(string accountName, double accountBalance) 
        {
            AccountName = accountName;
            AccountBalance = accountBalance;
        }
    }
}
