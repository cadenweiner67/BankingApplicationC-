// <copyright file="LoginTests.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LoginEngineTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LoginEngine;
    using NUnit.Framework;

    /// <summary>
    /// Login tests.
    /// </summary>
    public class LoginTests
    {
        /// <summary>
        /// Setup anything for test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test for adding user credentials.
        /// </summary>
        [Test]
        public void AddUserCredentialClientTests()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            Login log = new Login(filePath);
            log.AddUserCredentials("CadenWeiner", "xyz123", "Client");
            Assert.AreEqual(log.GetUsers()["CadenWeiner"], ("xyz123", "Client"));
        }

        /// <summary>
        /// Adds user credential for employee regularly.
        /// </summary>
        [Test]
        public void AddUserCredentialEmployeeTests()
        {
            Login log = new Login(string.Empty);
            log.AddUserCredentials("CadenWeiner", "xyz123", "Employee");
            Assert.AreEqual(log.GetUsers()["CadenWeiner"], ("xyz123", "Employee"));
        }

        /// <summary>
        /// adds user credentials but shouldn't if the string is empty.
        /// </summary>
        [Test]
        public void AddUserCredentialEmployeeFailTests()
        {
            Login log = new Login(string.Empty);
            log.AddUserCredentials("CadenWeiner", string.Empty, "Employee"); // should not add a key without a password.
            Assert.AreNotEqual(log.GetUsers().ContainsKey("CadenWeiner"), true);
        }

        /// <summary>
        /// Makes sure passwords can't be overwritten.
        /// </summary>
        [Test]
        public void AddUserCredentialDuplicateKeyTests()
        {
            Login log = new Login(string.Empty);
            log.AddUserCredentials("CadenWeiner", "xyz123", "Employee");
            log.AddUserCredentials("CadenWeiner", "newpassword", "Employee"); // should not overrite the new password.
            Assert.AreEqual(log.GetUsers()["CadenWeiner"], ("xyz123", "Employee"));
        }

        /// <summary>
        /// Loads all users from a file and tests it.
        /// </summary>
        [Test]
        public void LoadUsersFromFileTests()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            Login log = new Login(filePath);
            log.LoadAllUsers();
            Console.WriteLine("Load Successful");
            Console.WriteLine("x" + log.GetUsers().Count.ToString());
            Dictionary<string, (string, string)> testDict = new Dictionary<string, (string, string)>();
            testDict["Cynthia"] = ("Spirit", "Client");
            testDict["Ash"] = ("Ketchum", "Client");
            testDict["Brock"] = ("Rock", "Employee");
            bool isEqual = true;
            if (testDict.Count == log.GetUsers().Count)
            {
                foreach (var dict in log.GetUsers())
                {
                    if (testDict.ContainsKey(dict.Key) && testDict[dict.Key].Item1 == dict.Value.Item1 && testDict[dict.Key].Item2 == dict.Value.Item2)
                    {
                        continue;
                    }
                    else
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            else
            {
                isEqual = false;
            }

            Assert.AreEqual(true, isEqual);
        }

        /// <summary>
        /// loads users from file and adds additional credentials.
        /// </summary>
        [Test]
        public void LoadUsersFromFileAndAddAdditionalCredentialsTests()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestLoadFile.xml";
            Console.WriteLine(filePath);
            Login log = new Login(filePath);
            log.LoadAllUsers();
            log.AddUserCredentials("Steven", "Stone", "Client");
            Dictionary<string, (string, string)> testDict = new Dictionary<string, (string, string)>();
            testDict["Steven"] = ("Stone", "Client");
            testDict["Cynthia"] = ("Spirit", "Client");
            testDict["Ash"] = ("Ketchum", "Client");
            testDict["Brock"] = ("Rock", "Employee");
            bool isEqual = true;
            if (testDict.Count == log.GetUsers().Count)
            {
                foreach (var dict in log.GetUsers())
                {
                    if (testDict.ContainsKey(dict.Key) && testDict[dict.Key].Item1 == dict.Value.Item1 && testDict[dict.Key].Item2 == dict.Value.Item2)
                    {
                        continue;
                    }
                    else
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            else
            {
                isEqual = false;
            }

            Assert.AreEqual(true, isEqual);
        }

        /// <summary>
        /// Stores all users in a file.
        /// </summary>
        [Test]
        public void StoreUsersTest()
        {
            // add some users, store and then load back in.
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\TestExamples\\TestStoreFile.xml";
            Login log = new Login(filePath);
            log.AddUserCredentials("Steven", "Stone", "Client");
            log.AddUserCredentials("Cynthia", "Spirit", "Client");
            log.AddUserCredentials("Ash", "Ketchum", "Client");
            log.AddUserCredentials("Brock", "Rock", "Employee");
            Console.WriteLine("Storing All users");
            log.StoreAllUsers();

            // reload the newly stored data
            Login newLog = new Login(filePath);

            bool isEqual = true;
            newLog.LoadAllUsers();
            if (newLog.GetUsers().Count == log.GetUsers().Count)
            {
                foreach (var dict in log.GetUsers())
                {
                    if (newLog.GetUsers().ContainsKey(dict.Key) && newLog.GetUsers()[dict.Key].Item1 == dict.Value.Item1 && newLog.GetUsers()[dict.Key].Item2 == dict.Value.Item2)
                    {
                        continue;
                    }
                    else
                    {
                        isEqual = false;
                        break;
                    }
                }
            }
            else
            {
                isEqual = false;
            }

            Assert.AreEqual(true, isEqual);
        }
    }
}