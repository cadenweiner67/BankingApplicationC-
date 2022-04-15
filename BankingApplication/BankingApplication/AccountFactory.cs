// <copyright file="AccountFactory.cs" company="Caden Weiner">
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
    /// Public class for account factory.
    /// </summary>
    public class AccountFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountFactory"/> class.
        /// Account factory.
        /// </summary>
        public AccountFactory()
        {
        }

        /// <summary>
        /// Loan account generator.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">user name.</param>
        /// <param name="nAccountName">account name.</param>
        /// <param name="initialAmount">starting loan amount.</param>
        /// <param name="currentAmount">current account balance.</param>
        /// <param name="interestRate">interest rate.</param>
        /// <returns>loan account.</returns>
        public static Account CreateAccount(int nAccountNumber, string nUsername, string nAccountName, float initialAmount, float currentAmount, float interestRate)
        {
            return new Loan(nAccountNumber, nUsername, nAccountName, initialAmount, currentAmount, interestRate);
        }

        /// <summary>
        /// Savings account generator.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">account username.</param>
        /// <param name="nAccountName">account name.</param>
        /// <returns>saving accounts.</returns>
        public static Account CreateAccount(int nAccountNumber, string nUsername, string nAccountName)
        {
            return new SavingAccount(nAccountNumber, nUsername, nAccountName);
        }

        /// <summary>
        /// Savings account different parameters.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">account username.</param>
        /// <param name="nAccountName">account name.</param>
        /// <param name="savingBalance">savings balance.</param>
        /// <param name="interestRate">interest rate.</param>
        /// <returns>savings account.</returns>
        public static Account CreateAccount(int nAccountNumber, string nUsername, string nAccountName, float savingBalance, float interestRate)
        {
            return new SavingAccount(nAccountNumber, nUsername, nAccountName, savingBalance, interestRate);
        }

        /// <summary>
        /// Checking account.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">account username.</param>
        /// <param name="nAccountName">account name.</param>
        /// <param name="balance">balance.</param>
        /// <returns>checking account.</returns>
        public static Account CreateAccount(int nAccountNumber, string nUsername, string nAccountName, float balance)
        {
            return new CheckingAccount(nAccountNumber, nUsername, nAccountName, balance);
        }
    }
}
