// <copyright file="InterestCalculator.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InterestEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class that will handle interest for loans.
    /// </summary>
    public class InterestCalculator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterestCalculator"/> class.
        /// Interest Construction.
        /// </summary>
        public InterestCalculator()
        {
        }

        /// <summary>
        /// Calculates the interest for a given loan.
        /// </summary>
        /// <param name="interestRate">the interest rate of the loan.</param>
        /// <param name="currentAmount">current amount remaining.</param>
        /// <returns>float.</returns>
        public static float CalculateInterest(float interestRate, float currentAmount)
        {
            return interestRate * currentAmount; // in this project, capital  = loan amount minus interest due.
        }
    }
}
