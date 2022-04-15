// <copyright file="Client.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Client class for the banking application.
    /// </summary>
    public class Client : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// The constructor for the client class.
        /// </summary>
        /// <param name="nUsername">the user name on the account.</param>
        /// <param name="nPassword">the password of the user.</param>
        /// <param name="accountsList">acountslist.</param>
        public Client(string nUsername, string nPassword, List<Account> accountsList)
            : base(nUsername, nPassword)
        {
            this.ClientType = "Personal";
            this.CurrentTransfer = null; // starts at null initialized once user makes a transaction.
            if (accountsList == null)
            {
                this.AccountsList = new List<Account>();
            }
            else
            {
                this.AccountsList = accountsList;
            }
        }

        /// <summary>
        /// Gets or Sets the list of accounts. Used to verify uniqueness of accounts. Also used for account transfers.
        /// </summary>
        public List<Account> AccountsList { get; set; }

        /// <summary>
        /// Gets or Sets the type of client.
        /// </summary>
        public string ClientType { get; set; }

        /// <summary>
        /// Gets or Sets the current transfer for an account.
        /// </summary>
        public Transfer CurrentTransfer { get; set; }

        /// <summary>
        /// allows us to undo the transfer.
        /// </summary>
        /// <param name="myLock">lock.</param>
        public void AllowUndoTransfer(object myLock)
        {
            // now lock current transfer until 10 seconds pass.
            // lock current transfer for 10 seconds
            lock (myLock)
            {
                if (this.CurrentTransfer != null)
                {
                    // wait 10 seconds
                    Thread.Sleep(10000);
                }
            }
        }

        /// <summary>
        /// Start the transfer period.
        /// </summary>
        /// <param name="myLock">the lock.</param>
        public void StartTransferPeriod(object myLock)
        {
            // now lock current transfer until 10 seconds pass.
            Thread t = new Thread(this.AllowUndoTransfer);
            t.Start(myLock);
        }
    }
}
