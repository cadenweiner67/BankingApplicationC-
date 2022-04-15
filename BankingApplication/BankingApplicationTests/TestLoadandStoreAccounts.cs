// <copyright file="TestLoadandStoreAccounts.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BankingApplication;
    using NUnit.Framework;

    /// <summary>
    /// Tests for loading accounts.
    /// </summary>
    public class TestLoadandStoreAccounts
    {
        // stores the record of operations, not the  operations themselves. (The account in the ITransaction object will just be the account number) For the list of transactions we use and remove the first(oldest element) when a new item is added beyond 10.

        /// <summary>
        /// Test that the account is loadded.
        /// </summary>
        [Test]
        public void TestLoadedCheckingAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            CheckingAccount account = new CheckingAccount(3, "Caden Weiner", "My Checking", 1000.50F);
            bool sameAccounts = false;
            foreach (var acc in bS.AccountsList.Where(c => c.ShowAccountInfo() == account.ShowAccountInfo()))
            {
                sameAccounts = true;
            }

            Assert.AreEqual(true, sameAccounts);
        }

        /// <summary>
        /// Test that the account is loaded.
        /// </summary>
        [Test]
        public void TestLoadedSavingAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            SavingAccount account = new SavingAccount(2, "Caden Weiner", "My Saving", 2500F, .02F);
            bool sameAccounts = false;
            Console.WriteLine("Not Here");
            foreach (var acc in bS.AccountsList.Where(c => c.ShowAccountInfo() == account.ShowAccountInfo()))
            {
                sameAccounts = true;
            }

            Assert.AreEqual(true, sameAccounts);
        }

        /// <summary>
        /// Test that the account is loadded.
        /// </summary>
        [Test]
        public void TestLoadedLoanAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // load in accounts.
            Loan account = new Loan(1, "Caden Weiner", "My Loan", 2500F, 544.5F, .1F);
            bool sameAccounts = false;
            foreach (var acc in bS.AccountsList.Where(c => c.ShowAccountInfo() == account.ShowAccountInfo()))
            {
                sameAccounts = true;
            }

            Assert.AreEqual(true, sameAccounts);
        }

        /// <summary>
        /// operations can be transfers, deposit or withdrawals.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsSavingAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // load in accounts
            Assert.AreEqual("Deposit to Account Number:2 Amount:100\nTransfer to Account Number:3 from Account Number:3 Amount:100\nTransfer to Account Number:3 from Account Number:3 Amount:100\nWithdraw from Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:50\nDeposit to Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\n", bS.AccountsList.ElementAt(1).ShowLastTenOperations());

            // orders should all be the same. Just need to check that the account numbers are the same and that the type of command and amount for transaction are the same.
        }

        /// <summary>
        /// operations can be transfers, deposit or withdrawals.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsCheckingAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // loads in the accounts.

            Assert.AreEqual("Deposit to Account Number:3 Amount:100\nTransfer to Account Number:2 from Account Number:2 Amount:100\nTransfer to Account Number:2 from Account Number:2 Amount:100\nWithdraw from Account Number:3 Amount:100\nDeposit to Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:50\nDeposit to Account Number:3 Amount:100\nDeposit to Account Number:3 Amount:100\n", bS.AccountsList.ElementAt(2).ShowLastTenOperations());
        }

        /// <summary>
        /// all operations should be made up of loan payments.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsLoanAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            Console.WriteLine(filePath);
            Assert.AreEqual("Loan Payment (Interest:0.55 Capital:4.95)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:50 Capital:450)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:10 Capital:90)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:10 Capital:90)\nLoan Payment (Interest:100 Capital:900)\nLoan Payment (Interest:5 Capital:45)\n", bS.AccountsList.ElementAt(0).ShowLastTenOperations());
        }

        /// <summary>
        /// all operations should be made up of loan payments.
        /// </summary>
        [Test]
        public void TestStoreAccountsWithOperations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // automatically loads accounts

            filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestStoreFile.xml";
            Console.WriteLine(filePath);
            bS.AccountFileName = filePath;
            bS.SaveAccounts(); // storing in the new file.

            BankingSystem newBankSystem = new BankingSystem(string.Empty, filePath); // load in the newly stored accounts.
            bool sameAccounts = false;
            for (int i = 0; i < bS.AccountsList.Count && bS.AccountsList.Count == newBankSystem.AccountsList.Count; i++)
            {
                if (bS.AccountsList.ElementAt(i).ShowAccountInfo() == newBankSystem.AccountsList.ElementAt(i).ShowAccountInfo() && bS.AccountsList.ElementAt(i).ShowLastTenOperations() == newBankSystem.AccountsList.ElementAt(i).ShowLastTenOperations())
                {
                    // the two accounts are the same.
                    sameAccounts = true;
                }
                else
                {
                    sameAccounts = false;
                    break;
                }
            }

            Assert.AreEqual(true, sameAccounts);

            // load it in then create and save it in a different file then compair.
        }

        /// <summary>
        /// all operations should be made up of loan payments.
        /// </summary>
        [Test]
        public void TestStoreWithNoOperations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFileNoOperations.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // acutomatically loads accounts

            filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestStoreFileNoOperations.xml";
            Console.WriteLine(filePath);
            bS.AccountFileName = filePath;
            bS.SaveAccounts(); // storing in the new file.
            bool sameAccounts = false;
            BankingSystem newBankSystem = new BankingSystem(string.Empty, filePath); // load in the newly stored accounts.

            for (int i = 0; i < bS.AccountsList.Count && bS.AccountsList.Count == newBankSystem.AccountsList.Count; i++)
            {
                if (bS.AccountsList.ElementAt(i).ShowAccountInfo() == newBankSystem.AccountsList.ElementAt(i).ShowAccountInfo() && bS.AccountsList.ElementAt(i).ShowLastTenOperations() == newBankSystem.AccountsList.ElementAt(i).ShowLastTenOperations())
                {
                    // the two accounts are the same.
                    sameAccounts = true;
                }
                else
                {
                    sameAccounts = false;
                    break;
                }
            }

            Assert.AreEqual(true, sameAccounts);
        }

        /// <summary>
        /// all operations should be made up of loan payments.
        /// </summary>
        [Test]
        public void TestLoadWithNoOperations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFileNoOperations.xml";
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            Console.WriteLine(filePath);
            bool sameAccounts = false;
            BankingSystem newBankSystem = new BankingSystem(string.Empty, string.Empty);
            newBankSystem.AccountsList.Add(new Loan(1, "Caden Weiner", "My Loan", 2500F, 544.5F, .1F));
            newBankSystem.AccountsList.Add(new SavingAccount(2, "Caden Weiner", "My Saving", 2500F, .02F));
            newBankSystem.AccountsList.Add(new CheckingAccount(3, "Caden Weiner", "My Checking", 1000.5F));

            for (int i = 0; i < bS.AccountsList.Count && bS.AccountsList.Count == newBankSystem.AccountsList.Count; i++)
            {
                if (bS.AccountsList.ElementAt(i).ShowAccountInfo() == newBankSystem.AccountsList.ElementAt(i).ShowAccountInfo() && bS.AccountsList.ElementAt(i).ShowLastTenOperations() == newBankSystem.AccountsList.ElementAt(i).ShowLastTenOperations())
                {
                    Console.WriteLine(bS.AccountsList.ElementAt(i).ShowAccountInfo());
                    Console.WriteLine(newBankSystem.AccountsList.ElementAt(i).ShowAccountInfo());

                    // the two accounts are the same.
                    sameAccounts = true;
                }
                else
                {
                    Console.WriteLine("Statements not equal");
                    sameAccounts = false;
                    break;
                }
            }

            Assert.AreEqual(true, sameAccounts);
        }

        /// <summary>
        /// operations can be transfers, deposit or withdrawals.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsSavingAccount8Operations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile8Op.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            Assert.AreEqual("Deposit to Account Number:2 Amount:100\nTransfer to Account Number:3 from Account Number:3 Amount:100\nTransfer to Account Number:3 from Account Number:3 Amount:100\nWithdraw from Account Number:2 Amount:100\nDeposit to Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:100\nWithdraw from Account Number:2 Amount:50\n", bS.AccountsList.ElementAt(1).ShowLastTenOperations());

            // orders should all be the same. Just need to check that the account numbers are the same and that the type of command and amount for transaction are the same.
        }

        /// <summary>
        /// operations can be transfers, deposit or withdrawals.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsCheckingAccount8Operations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile8Op.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath); // loads in the accounts.

            Assert.AreEqual("Deposit to Account Number:3 Amount:100\nTransfer to Account Number:2 from Account Number:2 Amount:100\nTransfer to Account Number:2 from Account Number:2 Amount:100\nWithdraw from Account Number:3 Amount:100\nDeposit to Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:100\nWithdraw from Account Number:3 Amount:50\n", bS.AccountsList.ElementAt(2).ShowLastTenOperations());
        }

        /// <summary>
        /// all operations should be made up of loan payments.
        /// </summary>
        [Test]
        public void TestLoadedPastOperationsLoanAccount8Operations()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile8Op.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(string.Empty, filePath);
            Assert.AreEqual("Loan Payment (Interest:0.55 Capital:4.95)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:50 Capital:450)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:10 Capital:90)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:5 Capital:45)\nLoan Payment (Interest:10 Capital:90)\n", bS.AccountsList.ElementAt(0).ShowLastTenOperations());
        }
    }
}