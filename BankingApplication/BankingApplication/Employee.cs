// <copyright file="Employee.cs" company="Caden Weiner">
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
    /// Employee class open for further extension.
    /// </summary>
    public class Employee : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// Constructor for employee.
        /// </summary>
        /// <param name="nUsername">the user name.</param>
        /// <param name="nPassword">the user password.</param>
        public Employee(string nUsername, string nPassword)
            : base(nUsername, nPassword)
        {
        }

        /// <summary>
        /// Employees can create new accounts for users. Must be issued to an existing user.
        /// </summary>
        /// <param name="newAccount">new account.</param>
        /// <param name="bS">banking setup to alter.</param>
        /// <returns>whether or not new account is issued.</returns>
        public bool IssueNewAccount(Account newAccount, BankingSystem bS)
        {
            int count = bS.AccountsList.Where(x => x.AccountNumber == newAccount.AccountNumber).Count();

            // Console.WriteLine("Made it");
            if (count == 0)
            {
                // Console.WriteLine("Made it");
                // there are no accounts with the given number.
                if (bS.AccountsList.Where(x => x.Username == newAccount.Username && x.AccountName == newAccount.AccountName).Count() == 0)
                {
                    // create
                    // Console.WriteLine("Made it");
                    count = bS.BankUsers.Where(x => x.GetUsername() == newAccount.Username).Count(); // usernames are unique
                    if (count == 1)
                    {
                        // there is an account with this username
                        User userToGainAccount = bS.BankUsers.Find(x => x.GetUsername() == newAccount.Username); // issues
                        if ((userToGainAccount as Client) != null)
                        {
                            // Console.WriteLine("Made it");
                            (userToGainAccount as Client).AccountsList.Add(newAccount);
                            bS.AccountsList.Add(newAccount);
                            return true;
                        }
                    }
                }
            }

            // if it is not possible to make this account do nothing and return false
            return false;
        }
    }
}
