// <copyright file="InterestTests.cs" company="Caden Weiner">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace InterestEngineTests
{
    using InterestEngine;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Interest calculations.
    /// </summary>
    public class InterestTests
    {
        /// <summary>
        /// Test the interest due where both val are float.
        /// </summary>
        [Test]
        public void TestInterestPayment()
        {
            Assert.AreEqual(20F, InterestCalculator.CalculateInterest(0.2F, 100.00F));
        }

        /// <summary>
        /// Test the interest due where both val are float.
        /// </summary>
        [Test]
        public void TestInterestPaymentNoInterest()
        {
            Assert.AreEqual(0.00F, InterestCalculator.CalculateInterest(0.0F, 1.00F));
        }

        /// <summary>
        /// Test the interest due where both val are float with no payment.
        /// </summary>
        [Test]
        public void TestInterestNoPayment()
        {
            Assert.AreEqual(0F, InterestCalculator.CalculateInterest(0.1F, 0.0F));
        }

        /// <summary>
        /// Test the interest due where both val are float.
        /// </summary>
        [Test]
        public void TestInterestPaymentFloatInterestAndPayment()
        {
            Assert.AreEqual(.10F, InterestCalculator.CalculateInterest(0.1F, 1.00F));
        }

        /// <summary>
        /// Tests interest calculations.
        /// </summary>
        /// <param name="interest">The interest of the loan.</param>
        /// <param name="paymentAmount">the payment amount.</param>
        /// <returns>float interest due value.</returns>
        [TestCase(.1F, 100F, ExpectedResult = 10F)]
        [TestCase(.2F, 100F, ExpectedResult = 20F)]
        [TestCase(.1F, 100.50F, ExpectedResult = 10.05F)]
        [TestCase(.9F, 100F, ExpectedResult = 90F)]
        [TestCase(.8F, 100F, ExpectedResult = 80F)]
        [TestCase(.0875F, 100F, ExpectedResult = 8.75F)]
        [TestCase(.009F, 100F, ExpectedResult = .9F)]
        public float TestInterestPaymentFloatInterestAndPaymentManyTests(float interest, float paymentAmount)
        {
            return InterestCalculator.CalculateInterest(interest, paymentAmount);
        }

    }
}