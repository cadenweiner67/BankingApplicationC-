// <copyright file="Loan.cs" company="Caden weiner">
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
    /// This handles a clients outstanding loans.
    /// </summary>
    public class Loan : Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Loan"/> class.
        /// This is the constructor for a loan.
        /// </summary>
        /// <param name="initialAmount">float.</param>
        /// <param name="currentAmount">float(The current balance left on the loan).</param>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">string.</param>
        /// <param name="nAccountName">string name.</param>
        /// <param name="initialAmount">float amount.</param>
        /// <param name="currentAmount">float amount currently left on loan.</param>
        /// <param name="interestRate">interest amount.</param>
        public Loan(int nAccountNumber, string nUsername, string nAccountName, float initialAmount, float currentAmount, float interestRate)
            : base(nAccountNumber, nUsername, nAccountName)
        {
            this.InitialAmount = initialAmount;
            this.AccountBalance = currentAmount; // the loans current amount is the account balance in account.
            this.InterestRate = interestRate;
        }

        /// <summary>
        /// Gets or Sets checking balance.
        /// </summary>
        public float CurrentAmount { get; set; }

        /// <summary>
        /// Gets or Sets initial loan amount.
        /// </summary>
        public float InitialAmount { get; set; }

        /// <summary>
        /// Gets or Sets interest.
        /// </summary>
        public float InterestRate { get; set; }

        /// <summary>
        /// This makes a loan payment.
        /// </summary>
        /// <param name="amount">amount.</param>
        public void AddLoanPayment(float amount)
        {
            // calls add operation
        }

        /// <summary>
        /// Shows the account info.
        /// </summary>
        /// <returns>string.</returns>
        public override string ShowAccountInfo()
        {
            return "Loan Account: " + "(Account Name-> " + this.AccountName + ") (Account Number-> " + this.AccountNumber.ToString() + ") (Current Amount-> " + this.AccountBalance.ToString() + ") (Interest Rate-> " + this.InterestRate.ToString() + ") (Initial Balance-> " + this.InitialAmount.ToString() + ")";
        }
    }
}
