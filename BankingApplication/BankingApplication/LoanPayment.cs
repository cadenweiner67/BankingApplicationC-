// <copyright file="LoanPayment.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using InterestEngine;

    /// <summary>
    /// This handles loan payments.
    /// </summary>
    public class LoanPayment : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanPayment"/> class.
        /// </summary>
        /// <param name="loanToPay">the loan account to pay.</param>
        /// <param name="amount">The amount to pay.</param>
        public LoanPayment(Loan loanToPay, float amount)
        {
            this.LoanToPay = loanToPay;
            this.Interest = InterestCalculator.CalculateInterest(this.LoanToPay.InterestRate, amount);
            this.Capital = amount - this.Interest;
            if (this.Capital < 0)
            {
                this.Capital = 0;
            }
        }

        /// <summary>
        /// Gets or Sets loan to pay.
        /// </summary>
        public Loan LoanToPay { get; set; }

        /// <summary>
        /// Gets or Sets interest.
        /// </summary>
        public float Interest { get; set; }

        /// <summary>
        /// Gets or Sets capital.
        /// </summary>
        public float Capital { get; set; }

        /// <summary>
        /// Gets or Sets a payment.
        /// </summary>
        public float Payment { get; set; }

        /// <summary>
        /// This is the execute command.
        /// </summary>
        public void Execute()
        {
            this.LoanToPay.AccountBalance = this.LoanToPay.AccountBalance - this.Interest - this.Capital;
        }

        /// <summary>
        /// This undoes the execute command.
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
            return "Loan Payment (Interest:" + this.Interest.ToString() + " Capital:" + this.Capital.ToString() + ")\n";
        }
    }
}
