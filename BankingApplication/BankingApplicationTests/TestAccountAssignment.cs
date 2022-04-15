// <copyright file="TestAccountAssignment.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BankingApplication;
    using NUnit.Framework;

    /// <summary>
    /// This will tests how accounts are assigned to different users after being loaded in or added.
    /// </summary>
    public class TestAccountAssignment
    {
        /// <summary>
        /// testing account assignment.
        /// </summary>
        [Test]
        public void TestAccountAssignmentWithOperationsChecking()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);
            Employee ep = new Employee("C", "W");
            Assert.AreEqual(false, ep.IssueNewAccount(new CheckingAccount(1, "Caden Weiner", "My Checking", 2000), bS));
        }

        /// <summary>
        /// testing account assignment.
        /// </summary>
        [Test]
        public void TestAccountAssignmentWithOperationsCheckingValid()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);
            Employee ep = new Employee("C", "W");
            Assert.AreEqual(true, ep.IssueNewAccount(new CheckingAccount(10, "Caden Weiner", "My new Checking", 5000), bS));
        }

        /// <summary>
        /// testing account assignment with operations.
        /// </summary>
        [Test]
        public void TestAccountAssignmentWithOperationsSaving()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);
            Employee ep = new Employee("C", "W");
            Assert.AreEqual(false, ep.IssueNewAccount(new SavingAccount(1, "Caden Weiner", "My Saving"), bS));
        }

        /// <summary>
        /// test account assignment with operation.
        /// </summary>
        [Test]
        public void TestAccountAssignmentWithOperationsLoan()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);
            Employee ep = new Employee("C", "W");
            Assert.AreEqual(false, ep.IssueNewAccount(new Loan(1, "Caden Weiner", "My Loan", 10, 5, .3F), bS));
        }

        /// <summary>
        /// Test failing to add an account.
        /// </summary>
        [Test]
        public void TestAccountAssignmentAndFailingToAdd()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);
            Employee ep = new Employee("C", "W");

            // employee shouldn't overrite existing account
            Assert.AreEqual(false, ep.IssueNewAccount(new SavingAccount(1, "Caden Weiner", "My Saving"), bS)); // should not be issue
        }

        /// <summary>
        /// tests creating a new account.
        /// </summary>
        [Test]
        public void TestCreateNewAccount()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);

            // Account myAcc = bS.AccountsList.Where(x => x.AccountName == "My Checking").ElementAt(0); // there only is one
            Employee ep = new Employee("C", "W");

            // employee shouldn't overrite existing account
            Assert.AreEqual(true, ep.IssueNewAccount(new SavingAccount(10, "Caden Weiner", "My New Loan"), bS)); // should not be issue
        }

        /// <summary>
        /// Test creating a duplicate account.
        /// </summary>
        [Test]
        public void TestCreateNewAccountDuplicate()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);

            User me = bS.BankUsers.Where(x => x.GetUsername() == "Caden Weiner").ElementAt(0); // there only is one

            // Account myAcc = bS.AccountsList.Where(x => x.AccountName == "My Checking").ElementAt(0); // there only is one
            Employee ep = new Employee("C", "W");

            // employee shouldn't overrite existing account
            Assert.AreEqual(false, ep.IssueNewAccount(new Loan(1, "Caden Weiner", "My Loan", 2500, 544.5F, .1F), bS)); // should not be issue
        }

        /// <summary>
        /// Test create new account from empty.
        /// </summary>
        [Test]
        public void TestCreateNewAccountNothingLoaded()
        {
            string filePath = string.Empty;

            string userPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userPath = Directory.GetParent(userPath).FullName;
            userPath = Directory.GetParent(Directory.GetParent(userPath).FullName).FullName;
            userPath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(userPath);
            BankingSystem bS = new BankingSystem(userPath, filePath);

            User me = bS.BankUsers.Where(x => x.GetUsername() == "Caden Weiner").ElementAt(0); // there only is one

            // Account myAcc = bS.AccountsList.Where(x => x.AccountName == "My Checking").ElementAt(0); // there only is one
            Employee ep = new Employee("C", "W");

            // employee shouldn't overrite existing account since nothing is loaded so it can make
            Assert.AreEqual(true, ep.IssueNewAccount(new Loan(1, "Caden Weiner", "My Loan", 2500, 544.5F, .1F), bS)); // should not be issue
        }
    }
}
