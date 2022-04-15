// <copyright file="Deposit.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TransactionEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This is a deposit. It is a part of the command design pattern.
    /// </summary>
    public class Deposit : ITransaction
    {
        private Account depositAccount;

        private float depositAmmount
        /// <summary>
        /// Initializes a new instance of the <see cref="Deposit"/> class.
        /// Deposit constructor.
        /// </summary>
        public Deposit()
        {

        }

        public void Execute()
        {

        }

        public void UndoExecute()
        {

        }
    }
}
