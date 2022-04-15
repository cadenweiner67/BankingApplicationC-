// <copyright file="BankingSystem.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplication
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using LoginEngine;

    /// <summary>
    /// This is the class for the banking system.
    /// </summary>
    public class BankingSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankingSystem"/> class.
        /// Constructor for the banking system.
        /// </summary>
        /// <param name="fileName">filename.</param>
        /// <param name="accountFileName">account filename.</param>
        public BankingSystem(string fileName, string accountFileName)
        {
            this.FileName = fileName;
            this.AccountFileName = accountFileName;
            this.BankUsers = new List<User>();
            this.AccountsList = new List<Account>();
            if (this.AccountFileName != string.Empty)
            {
                this.LoadAccounts();
            }

            if (this.FileName != string.Empty)
            {
                this.BankLogin = new Login(this.FileName);
                this.LoadUsers();
            }
        }

        /// <summary>
        /// Gets or Sets the list of accounts. Used to verify uniqueness of accounts. Also used for account transfers.
        /// </summary>
        public List<Account> AccountsList { get; set; }

        /// <summary>
        /// Gets or Sets the filename where accounts are loaded from.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets the filename where accounts are loaded from.
        /// </summary>
        public string AccountFileName { get; set; }

        /// <summary>
        /// Gets or Sets The total funds available for a new account.
        /// </summary>
        public float BankFundsAvailable { get; set; }

        /// <summary>
        /// Gets or Sets Bank login.
        /// </summary>
        public Login BankLogin { get; set; }

        /// <summary>
        /// Gets or Sets the list of bank users.
        /// </summary>
        public List<User> BankUsers { get; set; }

        /// <summary>
        /// Loads the user with xml reader and creates the user objects.
        /// </summary>
        public void LoadUsers()
        {
            this.BankLogin.LoadAllUsers();
            this.CreateUsers(this.BankLogin.GetUsers());
        }

        /// <summary>
        /// Sets up user accounts for each user.
        /// </summary>
        public void SetupUserAccounts()
        {
            // updates all users to include the accounts associated with them.
            foreach (var acc in this.AccountsList)
            {
                User u = this.BankUsers.Find(x => x.GetUsername() == acc.Username);
                (u as Client).AccountsList.Add(acc);
            }
        }

        /// <summary>
        /// Loads the account with xml reader.
        /// </summary>
        public void LoadAccounts()
        {
            Debug.WriteLine(this.AccountsList.Count.ToString());
            using (Stream fs = File.Open(this.AccountFileName, FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            // Debug.WriteLine("Name of the Element is : " + reader.Name.ToString());
                            string name = reader.Name.ToString();

                            Debug.WriteLine("Name is : " + name.ToString());
                            switch (name)
                            {
                                case "Account":
                                    string aType = reader.GetAttribute("type").ToString();
                                    string accountType = reader.GetAttribute("accountType").ToString();
                                    string username = reader.GetAttribute("username").ToString();
                                    int accountNumber = 0;
                                    int.TryParse(reader.GetAttribute("accountNumber"), out accountNumber);
                                    string accountName = reader.GetAttribute("accountName").ToString();
                                    float balance = 0;
                                    float.TryParse(reader.GetAttribute("balance"), out balance);

                                    // only used by saving and loan
                                    float initialAmount = 0;
                                    float interestRate = 0;
                                    switch (aType)
                                    {
                                        case "loan":
                                            float.TryParse(reader.GetAttribute("initialAmount"), out initialAmount);
                                            float.TryParse(reader.GetAttribute("interest"), out interestRate);
                                            this.AccountsList.Add(new Loan(accountNumber, username, accountName, initialAmount, balance, interestRate));
                                            break;
                                        case "saving":
                                            float.TryParse(reader.GetAttribute("interest"), out interestRate);
                                            this.AccountsList.Add(new SavingAccount(accountNumber, username, accountName, balance, interestRate));
                                            break;
                                        case "checking":
                                            this.AccountsList.Add(new CheckingAccount(accountNumber, username, accountName, balance));
                                            break;
                                    }

                                    this.AccountsList.ElementAt(this.AccountsList.Count - 1);
                                    Debug.WriteLine("\n");

                                    // name = reader.Name.ToString();
                                    // Debug.WriteLine("Name is : " + name.ToString());
                                    break;
                                case "Operation":
                                    // we know operation is within account so add it to the last created account.
                                    string opType = reader.GetAttribute("opType").ToString();
                                    int dNumber = 0;
                                    float dAmount = 0;
                                    int wNumber = 0;
                                    float wAmount = 0;
                                    switch (opType)
                                    {
                                        case "Payment":
                                            float interestDue = 0;
                                            float capitalDue = 0;
                                            float.TryParse(reader.GetAttribute("paymentInterest"), out interestDue);
                                            float.TryParse(reader.GetAttribute("paymentCapital"), out capitalDue);
                                            Debug.WriteLine("Payment");

                                            // must be a loan
                                            this.AccountsList.ElementAt(this.AccountsList.Count - 1).LastOperations.Add(new LoanPayment(this.AccountsList.ElementAt(this.AccountsList.Count - 1) as Loan, interestDue + capitalDue)); // op belongs to this account

                                            // this.AccountsList.Add(new Loan(accountNumber, username, accountName, initialAmount, balance, interestRate));
                                            break;
                                        case "Deposit":
                                            int.TryParse(reader.GetAttribute("depositAccount"), out dNumber);
                                            float.TryParse(reader.GetAttribute("depositAmount"), out dAmount);
                                            this.AccountsList.ElementAt(this.AccountsList.Count - 1).LastOperations.Add(new Deposit(this.AccountsList.ElementAt(this.AccountsList.Count - 1), dAmount));
                                            Debug.WriteLine("Deposit");
                                            break;
                                        case "Withdraw":
                                            int.TryParse(reader.GetAttribute("withdrawalAccount"), out wNumber);
                                            float.TryParse(reader.GetAttribute("withdrawalAmount"), out wAmount);
                                            this.AccountsList.ElementAt(this.AccountsList.Count - 1).LastOperations.Add(new Withdraw(this.AccountsList.ElementAt(this.AccountsList.Count - 1), wAmount));
                                            Debug.WriteLine("Withdraw");

                                            // this.AccountsList.Add(new CheckingAccount(accountNumber, username, accountName, balance));
                                            break;
                                        case "Transfer":
                                            int.TryParse(reader.GetAttribute("withdrawalAccount"), out wNumber);
                                            float.TryParse(reader.GetAttribute("withdrawalAmount"), out wAmount);
                                            int.TryParse(reader.GetAttribute("depositAccount"), out dNumber);
                                            float.TryParse(reader.GetAttribute("depositAmount"), out dAmount);
                                            Debug.WriteLine("Transfer");

                                            // Account wAccount = this.AccountsList.Where(c => c.AccountNumber == wNumber).First();
                                            // Account dAccount = this.AccountsList.Where(c => c.AccountNumber == dNumber).First();
                                            this.AccountsList.ElementAt(this.AccountsList.Count - 1).LastOperations.Add(new Transfer(new Withdraw(new SavingAccount(wNumber, "Placeholder", "Withdraw Placeholder"), wAmount), new Deposit(new SavingAccount(wNumber, "Placeholder", "Deposit Placeholder"), dAmount), wAmount));
                                            break;
                                    }

                                    break;
                            }
                        }
                    }
                }
            }

            Debug.WriteLine(this.AccountsList.Count.ToString());
        }

        /// <summary>
        /// Saves the account with xml writer.
        /// </summary>
        public void SaveAccounts()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            // create writer and begin spreadsheet
            File.Delete(this.AccountFileName); // delete the file before creating it. Makes sure it can test creating each time.
            Debug.WriteLine("Deleting " + this.AccountFileName + " if it already exists.");
            FileStream fs;
            Debug.WriteLine(this.AccountsList.Count.ToString());
            using (fs = new FileStream(this.AccountFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
            {
                XmlWriter writer = XmlWriter.Create(fs, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Accounts");

                foreach (var k in this.AccountsList)
                {
                    writer.WriteStartElement("Account");
                    writer.WriteAttributeString("username", k.Username.ToString()); // write username
                    writer.WriteAttributeString("balance", k.AccountBalance.ToString());
                    writer.WriteAttributeString("accountNumber", k.AccountNumber.ToString());
                    writer.WriteAttributeString("accountName", k.AccountName.ToString());
                    writer.WriteAttributeString("accountType", "Client");

                    if (k as CheckingAccount != null)
                    {
                        writer.WriteAttributeString("type", "checking");
                    }
                    else if (k as SavingAccount != null)
                    {
                        writer.WriteAttributeString("type", "saving");
                        writer.WriteAttributeString("interest", (k as SavingAccount).GetInterestRate().ToString());
                    }
                    else if (k as Loan != null)
                    {
                        writer.WriteAttributeString("type", "loan");
                        writer.WriteAttributeString("interest", (k as Loan).InterestRate.ToString());
                        writer.WriteAttributeString("initialAmount", (k as Loan).InitialAmount.ToString());
                    }

                    writer.WriteStartElement("LastOperations");
                    Stack<ITransaction> helper = new Stack<ITransaction>();

                    // This is how we get the right amount of airlines and reverse order for storing.
                    for (int i = k.LastOperations.Count - 1; i > k.LastOperations.Count - 11 && i >= 0; i--)
                    {
                        ITransaction op = k.LastOperations.ElementAt(i);
                        helper.Push(op);
                    }

                    foreach (ITransaction op in helper)
                    {
                        writer.WriteStartElement("Operation");
                        if (op as Deposit != null)
                        {
                            writer.WriteAttributeString("opType", "Deposit");
                            writer.WriteAttributeString("depositAmount", (op as Deposit).DepositAmount.ToString());
                            writer.WriteAttributeString("depositAccount", (op as Deposit).DepositAccount.AccountNumber.ToString());
                        }
                        else if (op as Withdraw != null)
                        {
                            writer.WriteAttributeString("opType", "Withdraw");
                            writer.WriteAttributeString("withdrawalAmount", (op as Withdraw).WithdrawalAmount.ToString());
                            writer.WriteAttributeString("withdrawalAccount", (op as Withdraw).WithdrawalAccount.AccountNumber.ToString());
                        }
                        else if (op as LoanPayment != null)
                        {
                            writer.WriteAttributeString("opType", "Payment");
                            writer.WriteAttributeString("paymentCapital", (op as LoanPayment).Capital.ToString());
                            writer.WriteAttributeString("paymentInterest", (op as LoanPayment).Interest.ToString());
                        }
                        else if (op as Transfer != null)
                        {
                            writer.WriteAttributeString("opType", "Transfer");
                            writer.WriteAttributeString("depositAmount", (op as Transfer).DepositCommand.DepositAmount.ToString());
                            writer.WriteAttributeString("depositAccount", (op as Transfer).DepositCommand.DepositAccount.AccountNumber.ToString());
                            writer.WriteAttributeString("withdrawalAmount", (op as Transfer).WithdrawalCommand.WithdrawalAmount.ToString());
                            writer.WriteAttributeString("withdrawalAccount", (op as Transfer).WithdrawalCommand.WithdrawalAccount.AccountNumber.ToString());
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        /// <summary>
        /// Loads the account with xml reader.
        /// </summary>
        /// <param name="loginDict">login dictionary.</param>
        public void CreateUsers(Dictionary<string, (string, string)> loginDict)
        {
            this.BankUsers = new List<User>(); // should be empty when creating.
            foreach (var k in loginDict.Keys)
            {
                if (loginDict[k].Item2 == "Client")
                {
                    this.BankUsers.Add(new Client(k, loginDict[k].Item1, null));
                }
                else if (loginDict[k].Item2 == "Employee")
                {
                    this.BankUsers.Add(new Employee(k, loginDict[k].Item1));
                }

                // otherwise not an existing user type.
            }
        }
    }
}
