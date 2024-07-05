using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CreditReferencingConsoleApplication.Interfaces;
using CreditReferencingConsoleApplication.Models;

namespace CreditReferencingConsoleApplication.Services
{
    public class CsvReaderService : ICsvReaderService
    {
        public List<Transaction> ReadTransactions(string filePath)
        {
            var transactions = new List<Transaction>();

            try
            {
                using(var reader = new StreamReader(filePath))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while(csv.Read())
                    {
                        var transaction = csv.GetRecord<Transaction>();
                        transactions.Add(transaction);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading transactions CSV file: {ex.Message}");
            }

            return transactions;
        }

        public List<Property> ReadProperties(string filePath)
        {
            var properties = new List<Property>();

            try
            {
                using(var reader = new StreamReader(filePath))
                using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while(csv.Read())
                    {
                        var property = csv.GetRecord<Property>();
                        properties.Add(property);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading properties CSV file: {ex.Message}");
            }

            return properties;
        }
    }
}