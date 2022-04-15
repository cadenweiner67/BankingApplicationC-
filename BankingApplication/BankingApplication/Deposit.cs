// <copyright file="Deposit.cs" company="Caden Weiner">
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
    /// This is a deposit. It is a part of the command design pattern.
    /// </summary>
    public class Deposit : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Deposit"/> class.
        /// Deposit constructor.
        /// </summary>
        /// <param name="account">account.</param>
        /// <param name="amount">amount.</param>
        public Deposit(Account account, float amount)
        {
            this.DepositAccount = account;
            this.DepositAmount = amount;
        }

        /// <summary>
        /// Gets or Sets the account to deposit to.
        /// </summary>
        public Account DepositAccount { get; set; }

        /// <summary>
        /// Gets or Sets the amount to deposit.
        /// </summary>
        public float DepositAmount { get; set; }

        /// <summary>
        /// This is an execute command.
        /// </summary>
        public void Execute()
        {
            // might be easier to have account hold balances, and have (current amount) for loans.
            this.DepositAccount.AccountBalance += this.DepositAmount;
        }

        /// <summary>
        /// This is the implementation of the execute command.
        /// </summary>
        public void UndoExecute()
        {
        }

        /// <summary>
        /// Shows operation.
        /// </summary>
        /// <returns>string.</returns>
        public string ShowOperation()
        {
            return "Deposit to Account Number:" + this.DepositAccount.AccountNumber.ToString() + " Amount:" + this.DepositAmount.ToString() + "\n";
        }
    }
}
