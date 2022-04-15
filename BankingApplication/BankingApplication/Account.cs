// <copyright file="Account.cs" company="PlaceholderCompany">
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
    /// This is the abstract class.
    /// </summary>
    public abstract class Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="nAccountNumber">account number.</param>
        /// <param name="nUsername">username.</param>
        /// <param name="nAccountName">account name.</param>
        public Account(int nAccountNumber, string nUsername, string nAccountName)
        {
            this.AccountNumber = nAccountNumber;
            this.Username = nUsername;
            this.AccountName = nAccountName;
            this.LastOperations = new List<ITransaction>();
            this.AccountBalance = 0;
        }

        /// <summary>
        /// Gets or Sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets the accountnumber.
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or Sets the accountname.
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or Sets the accountbalance.
        /// </summary>
        public float AccountBalance { get; set; }

        /// <summary>
        /// Gets or Sets the latest transactions on an account.
        /// </summary>
        public List<ITransaction> LastOperations { get; set; }

        /// <summary>
        /// Displays the last ten operations on an account.
        /// </summary>
        /// <returns>string.</returns>
        public string ShowLastTenOperations()
        {
            string display = string.Empty;

            for (int i = this.LastOperations.Count - 1; i > this.LastOperations.Count - 11 && i >= 0; i--)
            {
                display += this.LastOperations.ElementAt(i).ShowOperation();
            }

            return display;
        }

        /// <summary>
        /// Shows account info, meant to be overriden.
        /// </summary>
        /// <returns>string.</returns>
        public virtual string ShowAccountInfo()
        {
            return string.Empty;
        }

        /// <summary>
        /// Adds an operation.
        /// </summary>
        /// <param name="operation">operation to add.</param>
        public void AddOperation(ITransaction operation)
        {
            if (this.LastOperations.Count > 9)
            {
                this.LastOperations.RemoveAt(0); // Remove the first and oldest element to make room for newest.
                this.LastOperations.Add(operation);
            }
            else
            {
                // Can Just add operation
                this.LastOperations.Add(operation);
            }
        }
    }
}
