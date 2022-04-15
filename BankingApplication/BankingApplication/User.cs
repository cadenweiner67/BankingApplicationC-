// <copyright file="User.cs" company="Caden Weiner">
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
    /// This is the base class for a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// This is the user name.
        /// </summary>
        private string username;

        /// <summary>
        /// This is the user password.
        /// </summary>
        private string password;

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// This is a constructor for user.
        /// </summary>
        /// <param name="nUsername">The username.</param>
        /// <param name="nPassword">The password.</param>
        public User(string nUsername, string nPassword)
        {
            this.username = nUsername;
            this.password = nPassword;
        }

        /// <summary>
        /// gets a username.
        /// </summary>
        /// <returns>string.</returns>
        public string GetUsername()
        {
            return this.username;
        }
    }
}
