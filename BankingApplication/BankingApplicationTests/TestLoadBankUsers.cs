// <copyright file="TestLoadBankUsers.cs" company="Caden Weiner">
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
    /// Class to test loading bank users.
    /// </summary>
    public class TestLoadBankUsers
    {
        /// <summary>
        /// Tests load clients.
        /// </summary>
        [Test]
        public void TestLoadClients()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestUsers\\TestLoadFileClients.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(filePath, string.Empty);
            bS.LoadUsers();
            bool areSameLists = true;

            // only verifying usernames and passwords.
            List<User> userTestList = new List<User>();
            userTestList.Add(new Client("Brock", "Rock", null));
            userTestList.Add(new Client("Cynthia", "Spirit", null));
            userTestList.Add(new Client("Ash", "Ketchum", null));
            userTestList.Add(new Client("Steven", "Stone", null));
            int i = 3;

            // verifying types are equal.
            if (bS.BankUsers.Count == userTestList.Count)
            {
                foreach (var user in bS.BankUsers)
                {
                    if (user.GetType() != userTestList.ElementAt(i).GetType())
                    {
                        areSameLists = false;
                        break;
                    }

                    i--;
                }
            }
            else
            {
                areSameLists = false;
            }

            Assert.AreEqual(true, areSameLists);
        }

        /// <summary>
        /// Tests load employees.
        /// </summary>
        [Test]
        public void TestLoadEmployees()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestUsers\\TestLoadFileEmployees.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(filePath, string.Empty);
            bS.LoadUsers();
            bool areSameLists = true;

            // only verifying usernames and passwords.
            List<User> userTestList = new List<User>();
            userTestList.Add(new Employee("Brock", "Rock"));
            userTestList.Add(new Employee("Cynthia", "Spirit"));
            userTestList.Add(new Employee("Ash", "Ketchum"));
            userTestList.Add(new Employee("Steven", "Stone"));
            int i = 3;

            // verifying types are equal.
            if (bS.BankUsers.Count == userTestList.Count)
            {
                foreach (var user in bS.BankUsers)
                {
                    if (user.GetType() != userTestList.ElementAt(i).GetType())
                    {
                        areSameLists = false;
                        break;
                    }

                    i--;
                }
            }
            else
            {
                areSameLists = false;
            }

            Assert.AreEqual(true, areSameLists);
        }

        /// <summary>
        /// Test load employees and clients.
        /// </summary>
        [Test]
        public void TestLoadEmployeesAndClients()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestUsers\\TestLoadFileBoth.xml";
            Console.WriteLine(filePath);
            BankingSystem bS = new BankingSystem(filePath, string.Empty);
            bS.LoadUsers();
            bool areSameLists = true;

            // only verifying usernames and passwords.
            List<User> userTestList = new List<User>();
            userTestList.Add(new Employee("Brock", "Rock"));
            userTestList.Add(new Client("Cynthia", "Spirit", null));
            userTestList.Add(new Client("Ash", "Ketchum", null));
            userTestList.Add(new Client("Steven", "Stone", null));
            userTestList.Add(new Client("Caden Weiner", "123", null));
            int i = 4;

            // verifying types are equal.
            if (bS.BankUsers.Count == userTestList.Count)
            {
                foreach (var user in bS.BankUsers)
                {
                    Console.WriteLine(user.GetType().ToString() + " " + userTestList.ElementAt(i).GetType().ToString());
                    if (user.GetType() != userTestList.ElementAt(i).GetType())
                    {
                        areSameLists = false;
                        break;
                    }

                    i--;
                }
            }
            else
            {
                areSameLists = false;
            }

            Console.WriteLine("Are not equal" + bS.BankUsers.Count.ToString() + userTestList.Count.ToString());
            Assert.AreEqual(true, areSameLists);
        }
    }
}
