using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using CreditReferencingConsoleApplication.Models;
using CreditReferencingConsoleApplication.Services;

namespace CreditReferencingConsoleApplication.Tests
{
    [TestFixture]
    public class CsvReaderServiceTests
    {
        private CsvReaderService _csvReaderService;

        [SetUp]
        public void Setup()
        {
            _csvReaderService = new CsvReaderService();
        }

        [Test]
        public void ReadTransactions_ValidData_ReturnsTransactions()
        {
            // Arrange
            string csvContent = 
                @"Date,Payment Type,Details,Money Out,Money In,Balance
                1st January 2020,Direct Debit,Gas & Electricity,95.06,,1200.04
                4th January 2020,Bank Credit,Awesome Job Ltd,,1254.23,1934.27
                1st February 2020,Direct Debit,Gas & Electricity,95.06,,1839.21
                4th February 2020,Bank Credit,Awesome Job Ltd,,1254.23,2543.44
                3rd February 2020,Standing Order,London Room,500.00,,1289.21";

            string filePath = CreateTempCsvFile(csvContent);

            // Act
            List<Transaction> transactions = _csvReaderService.ReadTransactions(filePath);

            // Assert
            Assert.NotNull(transactions);
            Assert.AreEqual(5, transactions.Count); // Adjust based on your mock data

            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void ReadProperties_ValidData_ReturnsProperties()
        {
            // Arrange
            string csvContent = 
                @"Id,Address,RentPerMonth
                1,1 Oxford Street,300
                2,12 St John Avenue,750
                3,Flat 43, Expensive Block,1200
                4,Flat 44, Expensive Block,1150";

            string filePath = CreateTempCsvFile(csvContent);

            // Act
            List<Property> properties = _csvReaderService.ReadProperties(filePath);

            // Assert
            Assert.NotNull(properties);
            Assert.AreEqual(4, properties.Count); // Adjust based on your mock data

            // Clean up
            File.Delete(filePath);
        }

        private string CreateTempCsvFile(string csvContent)
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, csvContent);
            return tempFilePath;
        }
    }
}