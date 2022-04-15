// <copyright file="ITransaction.cs" company="Caden Weiner">
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
    /// this is the interface used for all cameras.
    /// </summary>
    public interface ITransaction
    {
        /// <summary>
        /// This executes the transaction.
        /// </summary>
        void Execute();

        /// <summary>
        /// This executes the transaction.
        /// </summary>
        void UndoExecute();

        /// <summary>
        /// Shows operation.
        /// </summary>
        /// <returns>string.</returns>
        string ShowOperation();
    }
}
