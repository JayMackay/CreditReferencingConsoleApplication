using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using CreditReferencingConsoleApplication.Models;
using CreditReferencingConsoleApplication.Services;
using CreditReferencingConsoleApplication.Interfaces;

namespace CreditReferencingConsoleApplication.Tests
{
    [TestFixture]
    public class AffordabilityServiceTests
    {
        private Mock<IAffordabilityService> _mockAffordabilityService;

        [SetUp]
        public void SetUp()
        {
            _mockAffordabilityService = new Mock<IAffordabilityService>();
        }

        [Test]
        public void CreditReferenceCheck_AffordableProperties_ReturnsCorrectResult()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { Date = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), PaymentType = "Direct Debit", Details = "Gas & Electricity", MoneyOut = 95.06m, Balance = 1200.04m },
                new Transaction { Date = new DateTime(2020, 1, 4, 0, 0, 0, DateTimeKind.Unspecified), PaymentType = "Bank Credit", Details = "Awesome Job Ltd", MoneyIn = 1254.23m, Balance = 1934.27m },
                new Transaction { Date = new DateTime(2020, 2, 1, 0, 0, 0, DateTimeKind.Unspecified), PaymentType = "Direct Debit", Details = "Gas & Electricity", MoneyOut = 95.06m, Balance = 1839.21m },
                new Transaction { Date = new DateTime(2020, 2, 4, 0, 0, 0, DateTimeKind.Unspecified), PaymentType = "Bank Credit", Details = "Awesome Job Ltd", MoneyIn = 1254.23m, Balance = 2543.44m },
                new Transaction { Date = new DateTime(2020, 2, 3, 0, 0, 0, DateTimeKind.Unspecified), PaymentType = "Standing Order", Details = "London Room", MoneyOut = 500.00m, Balance = 1289.21m }
            };

            var properties = new List<Property>
            {
                new Property { Id = 1, Address = "1, Oxford Street", RentPerMonth = 300m },
                new Property { Id = 2, Address = "12, St John Avenue", RentPerMonth = 750m },
                new Property { Id = 3, Address = "Flat 43, Expensive Block", RentPerMonth = 1200m },
                new Property { Id = 4, Address = "Flat 44, Expensive Block", RentPerMonth = 1150m }
            };

            var service = new AffordabilityService();

            // Act
            var affordableProperties = service.CreditReferenceCheck(transactions, properties);

            // Assert
            Assert.AreEqual(1, affordableProperties.Count);
            Assert.AreEqual("1, Oxford Street", affordableProperties[0].Address);
        }
    }
}