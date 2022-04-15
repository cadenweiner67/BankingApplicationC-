// <copyright file="CheckingAccount.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Checking account class that inherits from account.
    /// </summary>
    public class CheckingAccount : Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">username.</param>
        /// <param name="nAccountName">account name.</param>
        /// <param name="balance">account balance.</param>
        public CheckingAccount(int nAccountNumber, string nUsername, string nAccountName, float balance)
            : base(nAccountNumber, nUsername, nAccountName)
        {
            this.AccountBalance = balance;
        }

        /// <summary>
        /// Shows the Checking accounts info.
        /// </summary>
        /// <returns>string.</returns>
        public override string ShowAccountInfo()
        {
            return "Checking Account: " + "(Account Name->" + this.AccountName + ") (Account Number->" + this.AccountNumber.ToString() + ") (Checking Balance->" + this.AccountBalance.ToString() + ")";
        }
    }
}
