// <copyright file="Program.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingApplication
{
    using System;
    using System.Activities.Statements;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;
    using LoginEngine;

    /// <summary>
    /// This runs the main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// lock object.
        /// </summary>
        private static object myLock = new object();

        /// <summary>
        /// This is the main function.
        /// </summary>
        /// <param name="args">the arguements for main.</param>
        public static void Main(string[] args)
        {
            BankingSystem bS;
            Login log;
            LoadBankingSystem(out bS, out log);
            string username = string.Empty;
            string aUsername = string.Empty;
            string password = string.Empty;
            string loginType = string.Empty;
            string input = string.Empty;
            string accountName = string.Empty;
            int accountNumber = 0;
            float accountBalance = 0;
            float interestRate = 0;
            log.LoadAllUsers();
            log.VerifyLogin(username, password);
            while (true)
            {
                LoginUser(log, out username, out password, out loginType);
                User currentUser = bS.BankUsers.Find(x => x.GetUsername() == username); // only one with this username
                string userChoice = "0";
                bool userLoggedIn = true; // should be true initially.
                if (currentUser as Employee != null)
                {
                    EmployeeActions(bS, username, aUsername, accountName, accountNumber, accountBalance, interestRate, currentUser, userChoice, userLoggedIn);
                }
                else if (currentUser as Client != null)
                {
                    ClientActions(bS, username, accountNumber, currentUser, userChoice, userLoggedIn);
                }
                else
                {
                    Console.WriteLine("Unsupported User Type"); // this should never be reached as all accounts are client or employee currently
                }

                // save any changes to xml.
                bS.SaveAccounts();
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="log">login object.</param>
        /// <param name="username">string.</param>
        /// <param name="password">password.</param>
        /// <param name="loginType">type being logged in.</param>
        private static void LoginUser(Login log, out string username, out string password, out string loginType)
        {
            do
            {
                // attempt login
                Console.WriteLine("Enter Username:");
                username = Console.ReadLine();
                Console.WriteLine("Enter Password:");
                password = Console.ReadLine();
                loginType = log.VerifyLogin(username, password);
            }
            while (loginType != "Client" && loginType != "Employee");
        }

        /// <summary>
        /// Loads the  banking system.
        /// </summary>
        /// <param name="bS">banking system.</param>
        /// <param name="log">login.</param>
        private static void LoadBankingSystem(out BankingSystem bS, out Login log)
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            filePath = Directory.GetParent(filePath).FullName;
            filePath = Directory.GetParent(Directory.GetParent(filePath).FullName).FullName;
            filePath += "\\BankingApplication\\LoadFiles\\XAccounts.xml";
            string userFilePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            userFilePath = Directory.GetParent(userFilePath).FullName;
            userFilePath = Directory.GetParent(Directory.GetParent(userFilePath).FullName).FullName;
            userFilePath += "\\BankingApplication\\LoadFiles\\XUsers.xml";
            Console.WriteLine(userFilePath);
            bS = new BankingSystem(userFilePath, filePath);
            bS.SetupUserAccounts();
            log = new Login(userFilePath);
        }

        /// <summary>
        /// EMployee actions.
        /// </summary>
        /// <param name="bS">banking system.</param>
        /// <param name="username">username.</param>
        /// <param name="aUsername">account user.</param>
        /// <param name="accountName">account name.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="accountBalance">account balance.</param>
        /// <param name="interestRate">interest rate.</param>
        /// <param name="currentUser">current user.</param>
        /// <param name="userChoice">user choice.</param>
        /// <param name="userLoggedIn">whether or not the user is logged in.</param>
        private static void EmployeeActions(BankingSystem bS, string username, string aUsername, string accountName, int accountNumber, float accountBalance, float interestRate, User currentUser, string userChoice, bool userLoggedIn)
        {
            Console.WriteLine("Hello Employee: " + username);
            while (userLoggedIn)
            {
                // do while logged in.
                EmployeeMenu();
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        EmployeeCreateChecking(currentUser, accountNumber, aUsername, accountName, accountBalance, bS); // create checkign account
                        break;
                    case "2":
                        EmployeeIssueLoan(currentUser, accountNumber, aUsername, accountName, accountBalance, interestRate, bS); // create loan account
                        break;
                    case "3":
                        EmployeeCreateGenericSavings(currentUser, accountNumber, aUsername, accountName, bS); // create a generic savings account.
                        break;
                    case "4":
                        EmployeeCreateSavings(currentUser, accountNumber, aUsername, accountName, accountBalance, interestRate, bS); // create a special savings account.
                        break;
                    case "5":
                        // log out user.
                        userLoggedIn = false;
                        Console.WriteLine("Goodbye : " + username);
                        break;
                }
            }
        }

        /// <summary>
        /// This handles user interaction for a client.
        /// </summary>
        /// <param name="bS">banking system.</param>
        /// <param name="username">username for account.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="currentUser">the  current user.</param>
        /// <param name="userChoice">what choice the user makes.</param>
        /// <param name="userLoggedIn">if a user is logged in or not.</param>
        private static void ClientActions(BankingSystem bS, string username, int accountNumber, User currentUser, string userChoice, bool userLoggedIn)
        {
            Console.WriteLine("Hello Client: " + username);
            while (userLoggedIn)
            {
                ClientMenu();
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        ClientTransfer(currentUser, bS);
                        break;
                    case "2":
                        ClientDeposit(currentUser);
                        break;
                    case "3":
                        ClientWithdraw(currentUser);
                        break;
                    case "4":
                        ClientLoanPayment(currentUser);
                        break;
                    case "5":
                        ShowClientAccountInfo(currentUser);
                        break;
                    case "6":
                        ShowClientAccountInfoAndOperations(currentUser);
                        break;
                    case "7":
                        // undo last transaction.
                        ClientUndoTransfer(currentUser);

                        break;
                    case "8":
                        userLoggedIn = false;
                        Console.WriteLine("Goodbye : " + username);
                        break;
                }
            }
        }

        /// <summary>
        /// Employee menu options.
        /// </summary>
        private static void EmployeeMenu()
        {
            Console.WriteLine("Enter Option:");
            Console.WriteLine("1). Create a New Checking Account");
            Console.WriteLine("2). Issue a New Loan");
            Console.WriteLine("3). Create a New Generic Savings Account");
            Console.WriteLine("4). Create a New Savings Account");
            Console.WriteLine("5). Log Out");
        }

        /// <summary>
        /// Create a checking account for a user.
        /// </summary>
        /// <param name="currentUser">Current Employee User.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="aUsername">username for account.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="accountBalance">balance.</param>
        /// <param name="bS">banking system.</param>
        private static void EmployeeCreateChecking(User currentUser, int accountNumber, string aUsername, string accountName, float accountBalance, BankingSystem bS)
        {
            Console.WriteLine("Enter Username:");
            aUsername = Console.ReadLine();
            Console.WriteLine("Enter Account Name:");
            accountName = Console.ReadLine();
            Console.WriteLine("Enter Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("Account Number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter Starting Checking Account Balance:");
            if (!float.TryParse(Console.ReadLine(), out accountBalance))
            {
                // unsuccessful parse
                Console.WriteLine("Account Balance must be a float");
                return; // exit switch early.
            }

            if ((currentUser as Employee).IssueNewAccount(new CheckingAccount(accountNumber, aUsername, accountName, accountBalance), bS))
            {
                Console.WriteLine("Created Account Number: " + accountNumber);
            }
            else
            {
                // failed to create
                Console.WriteLine("Cannot create an account with properties that should be unique");
            }

            return;
        }

        /// <summary>
        /// Issue a loan after getting user inputs.
        /// </summary>
        /// <param name="currentUser">Current Employee User.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="aUsername">username for account.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="accountBalance">balance.</param>
        /// <param name="interestRate">interest.</param>
        /// <param name="bS">banking system.</param>
        private static void EmployeeIssueLoan(User currentUser, int accountNumber, string aUsername, string accountName, float accountBalance, float interestRate, BankingSystem bS)
        {
            Console.WriteLine("Enter Username:");
            aUsername = Console.ReadLine();
            Console.WriteLine("Enter Account Name:");
            accountName = Console.ReadLine();
            Console.WriteLine("Enter Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("Account Number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Initial Loan Balance:");
            if (!float.TryParse(Console.ReadLine(), out accountBalance))
            {
                // unsuccessful parse
                Console.WriteLine("Account Balance Must Be a Float");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Loan's Interest Rate:");
            if (!float.TryParse(Console.ReadLine(), out interestRate))
            {
                // unsuccessful parse
                Console.WriteLine("The Loan's Interest Must be a Float");
                return; // exit switch early.
            }

            if ((currentUser as Employee).IssueNewAccount(new Loan(accountNumber, aUsername, accountName, accountBalance, accountBalance, interestRate), bS))
            {
                Console.WriteLine("Loan Successfully Created");
            }
            else
            {
                // failed to create
                Console.WriteLine("Cannot Create an Account with Properties that Should be Unique");
            }

            return;
        }

        /// <summary>
        /// Create a generic savings account.
        /// </summary>
        /// <param name="currentUser">Current Employee User.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="aUsername">username for account.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="bS">banking system.</param>
        private static void EmployeeCreateGenericSavings(User currentUser, int accountNumber, string aUsername, string accountName, BankingSystem bS)
        {
            Console.WriteLine("Enter Username:");
            aUsername = Console.ReadLine();
            Console.WriteLine("Enter Account Name:");
            accountName = Console.ReadLine();
            Console.WriteLine("Enter Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("Account Number must be an integer");
                return; // exit switch early.
            }

            if ((currentUser as Employee).IssueNewAccount(new SavingAccount(accountNumber, aUsername, accountName), bS))
            {
                Console.WriteLine("Created Account Number: " + accountNumber);
            }
            else
            {
                // failed to create
                Console.WriteLine("Cannot create an account with properties that should be unique");
            }

            return;
        }

        /// <summary>
        /// Create a savings account with specific balance and interest rate.
        /// </summary>
        /// <param name="currentUser">Current Employee User.</param>
        /// <param name="accountNumber">account number.</param>
        /// <param name="aUsername">username for account.</param>
        /// <param name="accountName">Account name.</param>
        /// <param name="accountBalance">balance.</param>
        /// <param name="interestRate">interest.</param>
        /// <param name="bS">banking system.</param>
        private static void EmployeeCreateSavings(User currentUser, int accountNumber, string aUsername, string accountName, float accountBalance, float interestRate, BankingSystem bS)
        {
            Console.WriteLine("Enter Username:");
            aUsername = Console.ReadLine();
            Console.WriteLine("Enter Account Name:");
            accountName = Console.ReadLine();
            Console.WriteLine("Enter Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("Account Number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Initial Saving Balance:");
            if (!float.TryParse(Console.ReadLine(), out accountBalance))
            {
                // unsuccessful parse
                Console.WriteLine("Account Balance Must Be a Float");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Saving Account Interest Rate:");
            if (!float.TryParse(Console.ReadLine(), out interestRate))
            {
                // unsuccessful parse
                Console.WriteLine("The Saving's Interest Rate Must be a Float");
                return; // exit switch early.
            }

            if ((currentUser as Employee).IssueNewAccount(new SavingAccount(accountNumber, aUsername, accountName, accountBalance, interestRate), bS))
            {
                Console.WriteLine("Savings Account Created");
            }
            else
            {
                // failed to create
                Console.WriteLine("Cannot create an account with properties that should be unique");
            }

            return;
        }

        /// <summary>
        /// Show the menu for client commands.
        /// </summary>
        private static void ClientMenu()
        {
            Console.WriteLine("\nEnter Option:");
            Console.WriteLine("1). Make a Transfer");
            Console.WriteLine("2). Make a Deposit");
            Console.WriteLine("3). Make a Withdrawal");
            Console.WriteLine("4). Make a Loan Payment");
            Console.WriteLine("5). View All Accounts Information");
            Console.WriteLine("6). View Accounts and Their Operation History");
            Console.WriteLine("7). Undo Transaction");
            Console.WriteLine("8). Log Out");
        }

        /// <summary>
        /// Execute a clients transfer after getting their inputs.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <param name="bS">banking system.</param>
        private static void ClientTransfer(User currentUser, BankingSystem bS)
        {
            int accountNumber = 0;
            float transAmount = 0;
            Console.WriteLine("Enter the Amount to Transfer:");
            if (!float.TryParse(Console.ReadLine(), out transAmount))
            {
                // unsuccessful parse
                Console.WriteLine("The Amount to Transfer Must be a Float");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Withdrawal Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("The Account Number Must be an Integer");
                return; // exit switch early.
            }

            // make sure that transfers can happen between multiple users in a bank. (Can only withdraw from users own account though)
            Account wTransAcc = (currentUser as Client).AccountsList.Find(x => x.AccountNumber == accountNumber); // can only withdraw from own account
            if (wTransAcc == null)
            {
                Console.WriteLine("Account to Withdraw from to is Not Valid");
                return; // can't find the account
            }

            ITransaction withdrawalTransCommand = new Withdraw(wTransAcc, transAmount);
            if (wTransAcc.AccountBalance - transAmount < 0)
            {
                Console.WriteLine("Cannot Overdraft Account");
                return; // can't withdraw to neg on transfer, some other banks may allow but not this one.
            }

            Console.WriteLine("Enter the Deposit Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("The Account Number Must be an Integer");
                return; // exit switch early.
            }

            // the amount is valid. Transfer to any account in the bank even ones that belong to someone else
            Account dTransAcc = bS.AccountsList.Find(x => x.AccountNumber == accountNumber); // can deposit to only own accounts number.
            if (dTransAcc == null)
            {
                Console.WriteLine("Account to Transfer to is Not Valid");
                return; // account to transfer to is not valid
            }

            ITransaction depositTransCommand = new Deposit(dTransAcc, transAmount);
            ITransaction transferCommand = new Transfer(withdrawalTransCommand as Withdraw, depositTransCommand as Deposit, transAmount);
            if (dTransAcc as Loan != null)
            {
                Console.WriteLine("Deposits cannot be made into a loan account, please make a loan payment instead.");
                return;
            }
            else if (wTransAcc as Loan != null)
            {
                Console.WriteLine("Cannot withdraw from a loan account, please make a loan payment instead.");
                return;
            }

            Console.WriteLine("Deposit Account " + dTransAcc.AccountNumber);
            Console.WriteLine("Withdraw Account " + wTransAcc.AccountNumber);

            (transferCommand as Transfer).Execute();
            dTransAcc.AddOperation(transferCommand);
            wTransAcc.AddOperation(transferCommand); // transfer should be recorded on both ends.

            (currentUser as Client).CurrentTransfer = transferCommand as Transfer;
            (currentUser as Client).StartTransferPeriod(myLock);

            Console.WriteLine("Transaction Receipt");
            Console.WriteLine(transferCommand.ShowOperation());
        }

        /// <summary>
        /// Make a deposit into own account.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        private static void ClientDeposit(User currentUser)
        {
            float depositAmount = 0;
            int accountNumber = 0;
            Console.WriteLine("Enter the Deposit Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("The account number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Deposit Amount:");
            if (!float.TryParse(Console.ReadLine(), out depositAmount))
            {
                // unsuccessful parse
                Console.WriteLine("The Deposit Amount Must be a Float");
                return; // exit switch early.
            }

            // the amount is valid
            Account dAcc = (currentUser as Client).AccountsList.Find(x => x.AccountNumber == accountNumber); // can deposit to only own accounts number.
            if (dAcc as Loan != null)
            {
                Console.WriteLine("Deposits cannot be made into a loan account, please make a loan payment instead.");
                return;
            }

            ITransaction depositCommand = new Deposit(dAcc, depositAmount);
            (depositCommand as Deposit).Execute();
            dAcc.AddOperation(depositCommand);
            Console.WriteLine("Transaction Receipt");
            Console.WriteLine(depositCommand.ShowOperation());
            return;
        }

        /// <summary>
        /// Make a withdrawal from own account.
        /// </summary>
        /// <param name="currentUser">the logged in user.</param>
        private static void ClientWithdraw(User currentUser)
        {
            int accountNumber = 0;
            float withdrawAmount = 0;
            Console.WriteLine("Enter the Withdrawal Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("The account number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Amount to Withdraw:");
            if (!float.TryParse(Console.ReadLine(), out withdrawAmount))
            {
                // unsuccessful parse
                Console.WriteLine("The Amount to Withdraw Must be a Float");
                return; // exit switch early.
            }

            Account wAcc = (currentUser as Client).AccountsList.Find(x => x.AccountNumber == accountNumber); // can only withdraw from own account
            if (wAcc as Loan != null)
            {
                Console.WriteLine("Withdrawals cannot be made from a loan account, please make a loan payment instead.");
                return;
            }

            ITransaction withdrawalCommand = new Withdraw(wAcc, withdrawAmount);
            (withdrawalCommand as Withdraw).Execute();
            wAcc.AddOperation(withdrawalCommand);
            Console.WriteLine("Transaction Receipt");
            Console.WriteLine(withdrawalCommand.ShowOperation());
            return;
        }

        /// <summary>
        /// This makes a loan payment for the current user.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        private static void ClientLoanPayment(User currentUser)
        {
            int accountNumber = 0;
            float paymentAmount = 0;
            Console.WriteLine("Enter the Loan Account Number:");
            if (!int.TryParse(Console.ReadLine(), out accountNumber))
            {
                // unsuccessful parse
                Console.WriteLine("The account number must be an integer");
                return; // exit switch early.
            }

            Console.WriteLine("Enter the Amount to Pay:");
            if (!float.TryParse(Console.ReadLine(), out paymentAmount))
            {
                // unsuccessful parse
                Console.WriteLine("The Amount to Pay Must be a Float");
                return; // exit switch early.
            }

            Account pAcc = (currentUser as Client).AccountsList.Find(x => x.AccountNumber == accountNumber); // can only withdraw from own account
            if (pAcc as Loan != null)
            {
                // it is a loan make payment.
                ITransaction paymentCommand = new LoanPayment(pAcc as Loan, paymentAmount);
                (paymentCommand as LoanPayment).Execute();
                pAcc.AddOperation(paymentCommand);
                Console.WriteLine("Transaction Receipt");
                Console.WriteLine(paymentCommand.ShowOperation());
            }

            return;
        }

        /// <summary>
        /// Shows all of the accounts of the current user.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        private static void ShowClientAccountInfo(User currentUser)
        {
            foreach (var account in (currentUser as Client).AccountsList)
            {
                Console.WriteLine(account.ShowAccountInfo());
            }
        }

        /// <summary>
        /// Shows all account info including previous operations for current user.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        private static void ShowClientAccountInfoAndOperations(User currentUser)
        {
            foreach (var account in (currentUser as Client).AccountsList)
            {
                Console.WriteLine(account.ShowAccountInfo());
                Console.WriteLine(account.ShowLastTenOperations());
            }
        }

        /// <summary>
        /// Undo the current transfer.
        /// </summary>
        /// <param name="currentUser">the current user.</param>
        private static void ClientUndoTransfer(User currentUser)
        {
            if ((currentUser as Client).CurrentTransfer != null)
            {
                // should be locked otherwise
                Console.WriteLine("Undoing Last Transfer");
                (currentUser as Client).CurrentTransfer.UndoExecute();

                // void the  previous transfer in the log. Not currently working.
                (currentUser as Client).CurrentTransfer.WithdrawalCommand = new Withdraw((currentUser as Client).CurrentTransfer.WithdrawalCommand.WithdrawalAccount, 0);
                (currentUser as Client).CurrentTransfer.DepositCommand = new Deposit((currentUser as Client).CurrentTransfer.DepositCommand.DepositAccount, 0);

                (currentUser as Client).CurrentTransfer = null; // set to null once done. // make sure to prevent reexecution.
            }
            else
            {
                Console.WriteLine("Cannot Undo. (No Undo, Already Undone or Undo Time Expired)");
            }
        }
    }
}
