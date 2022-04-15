// <copyright file="SavingAccount.cs" company="Caden Weiner">
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
    /// Savings account.
    /// </summary>
    public class SavingAccount : Account
    {
        /// <summary>
        /// The interest gain rate on savings account.
        /// </summary>
        private float interestRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingAccount"/> class.
        /// This is a savings account contructor that sets the account with default values.
        /// </summary>
        /// <param name="nAccountNumber">AccountNumber.</param>
        /// <param name="nUsername">Username.</param>
        /// <param name="nAccountName">AccountName.</param>
        public SavingAccount(int nAccountNumber, string nUsername, string nAccountName)
            : base(nAccountNumber, nUsername, nAccountName)
        {
            this.AccountBalance = 15000F;
            this.interestRate = .005F; // 5%
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingAccount"/> class.
        /// This is the constructor for the savings account.
        /// </summary>
        /// <param name="nAccountNumber">AccountNumber.</param>
        /// <param name="nUsername">Username.</param>
        /// <param name="nAccountName">AccountName.</param>
        /// <param name="savingBalance">Saving balance float.</param>
        /// <param name="interestRate">interst float.</param>
        public SavingAccount(int nAccountNumber, string nUsername, string nAccountName, float savingBalance, float interestRate)
            : base(nAccountNumber, nUsername, nAccountName)
        {
            this.AccountBalance = savingBalance;
            this.interestRate = interestRate;
        }

        /// <summary>
        /// Shows the savings info.
        /// </summary>
        /// <returns>string.</returns>
        public override string ShowAccountInfo()
        {
            return "Saving Account: " + "(Account Name-> " + this.AccountName + ") (Saving Balance->" + this.AccountBalance.ToString() + ") (Interest Rate->" + this.interestRate.ToString() + ") (Account Number->" + this.AccountNumber.ToString() + ")";
        }

        /// <summary>
        /// Returns the interest rate.
        /// </summary>
        /// <returns>interest rate.</returns>
        public float GetInterestRate()
        {
            return this.interestRate;
        }
    }
}
