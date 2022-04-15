// <copyright file="TestAccountFactory.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using BankingApplication;

    /// <summary>
    /// Tests the account factory.
    /// </summary>
    public class TestAccountFactory
    {
        /// <summary>
        /// This is a test for account factory savings with only certain parameters.
        /// </summary>
        public void TestAccountFactoryLoan()
        {
            Account createdAccount = AccountFactory.CreateAccount(1, "Caden Weiner", "My Loan", 1000, 500, .2F);
            Assert.AreEqual("Loan Account: (Account Name-> My Loan) (Account Number-> 1) (Current Amount-> 500) (Interest Rate-> 0.2) (Initial Balance-> 100)", createdAccount.ShowAccountInfo());
        }

        /// <summary>
        /// This tests the account with interest an given initial balance for saving.
        /// </summary>
        public void TestAccountFactorySavingsWithInterestandInitialBalance()
        {
            Account createdAccount = AccountFactory.CreateAccount(1, "Caden Weiner", "My Saving", 1000, .2F);
            Assert.AreEqual("Saving Account: (Account Name-> My Saving) (Saving Balance->1000) (Interest Rate->0.2) (Account Number->1)", createdAccount.ShowAccountInfo());
        }

        /// <summary>
        /// This tests the account factory for saving account.
        /// </summary>
        public void TestAccountFactorySavingAccountNoInterestorInitialBalance()
        {
            Account createdAccount = AccountFactory.CreateAccount(1, "Caden Weiner", "My Saving");

            Assert.AreEqual("Saving Account: (Account Name-> My Saving) (Saving Balance->15000) (Interest Rate->0.005) (Account Number->1)", createdAccount.ShowAccountInfo());
        }

        /// <summary>
        /// This tests the account factory for a checking.
        /// </summary>
        public void TestAccountFactoryCheckingAccount()
        {
            Account createdAccount = AccountFactory.CreateAccount(1, "Caden Weiner", "My Checking", 1000);
            Assert.AreEqual("Checking Account: (Account Name->My Checking) (Account Number->1) (Checking Balance->1000)", createdAccount.ShowAccountInfo());
        }
    }
}
