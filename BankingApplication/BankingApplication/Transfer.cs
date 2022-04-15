// <copyright file="Transfer.cs" company="Caden Weiner">
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
    /// This is the class that handles transfer.
    /// </summary>
    public class Transfer : ITransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transfer"/> class.
        /// The class that handles transfers.
        /// </summary>
        /// <param name="withdrawalCommand">withdraw command.</param>
        /// <param name="depositCommand">deposit command.</param>
        /// <param name="amount">amount.</param>
        public Transfer(Withdraw withdrawalCommand, Deposit depositCommand, float amount)
        {
            this.TransferAmount = amount;
            this.WithdrawalCommand = withdrawalCommand;
            this.DepositCommand = depositCommand;
        }

        /// <summary>
        /// Gets or Sets the transfer amount.
        /// </summary>
        public float TransferAmount { get; set; }

        /// <summary>
        /// Gets or Sets the withdrawal commands.
        /// </summary>
        public Withdraw WithdrawalCommand { get; set; }

        /// <summary>
        /// Gets or Sets the deposit commands.
        /// </summary>
        public Deposit DepositCommand { get; set; }

        /// <summary>
        /// This Executes a transfer.
        /// </summary>
        public void Execute()
        {
            this.WithdrawalCommand.Execute();
            this.DepositCommand.Execute();
        }

        /// <summary>
        /// This Undoes an Execute a transfer.
        /// </summary>
        public void UndoExecute()
        {
            // Console.WriteLine("Undoing Our Transaction Execution");
            Console.WriteLine(this.DepositCommand.ShowOperation());
            Console.WriteLine(this.WithdrawalCommand.ShowOperation());

            Account w = this.WithdrawalCommand.WithdrawalAccount;
            Account d = this.DepositCommand.DepositAccount;
            ITransaction wC = new Withdraw(d, this.TransferAmount);
            ITransaction dC = new Deposit(w, this.TransferAmount);
            wC.Execute();
            dC.Execute();
        }

        /// <summary>
        /// Shows operation.
        /// </summary>
        /// <returns>string.</returns>
        public string ShowOperation()
        {
            return "Transfer to Account Number:" + this.DepositCommand.DepositAccount.AccountNumber.ToString() + " from Account Number:" + this.WithdrawalCommand.WithdrawalAccount.AccountNumber.ToString() + " Amount:" + this.TransferAmount.ToString() + "\n";
        }
    }
}
