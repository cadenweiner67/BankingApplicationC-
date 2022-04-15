// <copyright file="TestAccount.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BankingApplication;
    using NUnit.Framework;

    /// <summary>
    /// Test account.
    /// </summary>
    public class TestAccount
    {
        /// <summary>
        /// Show las 10 op on saving account.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsSavingAccountFull()
        {
            Account acc = new SavingAccount(1, "Caden", "First Savings Account");
            acc.AccountBalance = 3000;
            Account accT = new CheckingAccount(2, "Caden", "First Checkings Account", 3000);
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 50));
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 50));
            acc.AddOperation(new Transfer(new Withdraw(acc, 100), new Deposit(accT, 100), 100));
            acc.AddOperation(new Transfer(new Withdraw(acc, 50), new Deposit(accT, 50), 50));
            Assert.AreEqual("Transfer to Account Number:2 from Account Number:1 Amount:50\nTransfer to Account Number:2 from Account Number:1 Amount:100\nDeposit to Account Number:1 Amount:50\nDeposit to Account Number:1 Amount:100\nDeposit to Account Number:1 Amount:100\nDeposit to Account Number:1 Amount:100\nWithdraw from Account Number:1 Amount:100\nWithdraw from Account Number:1 Amount:50\nWithdraw from Account Number:1 Amount:100\nWithdraw from Account Number:1 Amount:100\n", acc.ShowLastTenOperations());

            // load in accounts
        }

        /// <summary>
        /// Show last 10 on checking account full.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsCheckingAccountFull()
        {
            Account acc = new CheckingAccount(2, "Caden", "First Checkings Account", 3000);
            Account accT = new SavingAccount(1, "Caden", "First Savings Account");
            accT.AccountBalance = 3000;
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 50));
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 50));
            acc.AddOperation(new Transfer(new Withdraw(acc, 100), new Deposit(accT, 100), 100));
            acc.AddOperation(new Transfer(new Withdraw(acc, 50), new Deposit(accT, 50), 50));
            Assert.AreEqual("Transfer to Account Number:1 from Account Number:2 Amount:50\nTransfer to Account Number:1 from Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:50\nDeposit to Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:50\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\n", acc.ShowLastTenOperations());
        }

       /// <summary>
       /// SHow last 10 on loan account.
       /// </summary>
        [Test]
        public void TestShowLast10OperationsLoanAccountFull()
        {
            Account acc = new Loan(3, "Caden", "First Loan Account", 10000F, 5000F, .05F);
            Account accT = new SavingAccount(1, "Caden", "First Savings Account");
            acc.AccountBalance = 3000;
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 50));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 200));
            acc.AddOperation(new LoanPayment(acc as Loan, 300));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 50));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            Assert.AreEqual("Loan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:2.5 Capital:47.5)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:15 Capital:285)\nLoan Payment (Interest:10 Capital:190)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:2.5 Capital:47.5)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:5 Capital:95)\n", acc.ShowLastTenOperations());
        }

        /// <summary>
        /// SHow last 10 opeations with no op.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsSavingAccountEmpty()
        {
            Account acc = new CheckingAccount(2, "Caden", "First Checkings Account", 0);
            Account accT = new SavingAccount(1, "Caden", "First Savings Account");
            Assert.AreEqual(string.Empty, acc.ShowLastTenOperations());
        }

        /// <summary>
        /// show half full 10 op.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsCheckingAccountHalfFull()
        {
            Account acc = new CheckingAccount(2, "Caden", "First Checkings Account", 0);
            Account accT = new SavingAccount(1, "Caden", "First Savings Account");
            acc.AccountBalance = 3000;
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 50));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Transfer(new Withdraw(acc, 50), new Deposit(accT, 50), 50));
            Assert.AreEqual("Transfer to Account Number:1 from Account Number:2 Amount:50\nDeposit to Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:50\nWithdraw from Account Number:2 Amount:100\n", acc.ShowLastTenOperations());
        }

        /// <summary>
        /// show half full 10 op on saving.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsSavingAccountHalfFull()
        {
            Account acc = new SavingAccount(1, "Caden", "First Savings Account");
            acc.AccountBalance = 3000;
            Account accT = new CheckingAccount(2, "Caden", "First Checkings Account", 0);
            acc.AddOperation(new Withdraw(acc, 100));
            acc.AddOperation(new Withdraw(acc, 500));
            acc.AddOperation(new Deposit(acc, 100));
            acc.AddOperation(new Deposit(acc, 50));
            acc.AddOperation(new Transfer(new Withdraw(acc, 50), new Deposit(accT, 50), 50));
            Assert.AreEqual("Transfer to Account Number:2 from Account Number:1 Amount:50\nDeposit to Account Number:1 Amount:50\nDeposit to Account Number:1 Amount:100\nWithdraw from Account Number:1 Amount:500\nWithdraw from Account Number:1 Amount:100\n", acc.ShowLastTenOperations());
        }

        /// <summary>
        /// Show half full loan acc.
        /// </summary>
        [Test]
        public void TestShowLast10OperationsLoanAccountHalfFull()
        {
            Account acc = new Loan(3, "Caden", "First Loan Account", 10000F, 5000F, .05F);
            Account accT = new SavingAccount(1, "Caden", "First Savings Account");
            acc.AccountBalance = 3000;
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 300));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 100));
            acc.AddOperation(new LoanPayment(acc as Loan, 50));
            Assert.AreEqual("Loan Payment (Interest:2.5 Capital:47.5)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:5 Capital:95)\nLoan Payment (Interest:15 Capital:285)\nLoan Payment (Interest:5 Capital:95)\n", acc.ShowLastTenOperations());
        }

        /// <summary>
        /// show checking account.
        /// </summary>
        [Test]
        public void TestShowCheckingAccount()
        {
            Account acc = new CheckingAccount(2, "Caden", "First Checkings Account", 3000);
            Assert.AreEqual("Checking Account: (Account Name->First Checkings Account) (Account Number->2) (Checking Balance->3000)", acc.ShowAccountInfo());
        }

        /// <summary>
        /// show saving account test.
        /// </summary>
        [Test]
        public void TestShowSavingAccount()
        {
            Account acc = new SavingAccount(1, "Caden", "First Savings Account", 3000, .09F);
            Assert.AreEqual("Saving Account: (Account Name-> First Savings Account) (Saving Balance->3000) (Interest Rate->0.09) (Account Number->1)", acc.ShowAccountInfo());
        }

        /// <summary>
        /// show loan account.
        /// </summary>
        [Test]
        public void TestShowLoanAccount()
        {
            Account acc = new Loan(3, "Caden", "First Loan Account", 10000F, 5000F, .05F);
            Assert.AreEqual("Loan Account: (Account Name-> First Loan Account) (Account Number-> 3) (Current Amount-> 5000) (Interest Rate-> 0.05) (Initial Balance-> 10000)", acc.ShowAccountInfo());
        }
    }
}
