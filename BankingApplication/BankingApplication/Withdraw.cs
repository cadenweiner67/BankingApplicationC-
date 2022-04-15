// <copyright file="Withdraw.cs" company="Caden Weiner">
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
    /// This class implements the withdrawal functionality.
    /// </summary>
    public class Withdraw : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Withdraw"/> class.
        /// </summary>
        /// <param name="nWithdrawAccount">account to withdraw from.</param>
        /// <param name="nWithdrawalAmount">the amount to withdraw.</param>
        public Withdraw(Account nWithdrawAccount, float nWithdrawalAmount)
        {
            this.WithdrawalAccount = nWithdrawAccount;
            this.WithdrawalAmount = nWithdrawalAmount;
        }

        /// <summary>
        /// Gets or Sets withdrawal amount.
        /// </summary>
        public float WithdrawalAmount { get; set; }

        /// <summary>
        /// Gets or Sets withdrawal account.
        /// </summary>
        public Account WithdrawalAccount { get; set; }

        /// <summary>
        /// The execute command.
        /// </summary>
        public void Execute()
        {
            if (this.CheckIfWithdrawalAvailable())
            {
                this.WithdrawalAccount.AccountBalance = this.WithdrawalAccount.AccountBalance - this.WithdrawalAmount;
            }
        }

        /// <summary>
        /// The command to undo the execute command.
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
            return "Withdraw from Account Number:" + this.WithdrawalAccount.AccountNumber.ToString() + " Amount:" + this.WithdrawalAmount.ToString() + "\n";
        }

        /// <summary>
        /// This is checks if a withdrawal is possible.
        /// </summary>
        /// <returns>It returns whether or not the account has a balance that is large enough to be withdrawn from.</returns>
        public bool CheckIfWithdrawalAvailable()
        {
            if (this.WithdrawalAccount.AccountBalance - this.WithdrawalAmount >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
