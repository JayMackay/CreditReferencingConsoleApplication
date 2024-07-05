using System;
using System.Collections.Generic;
using CreditReferencingConsoleApplication.Models;
using CreditReferencingConsoleApplication.Services;

namespace CreditReferencingConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Transaction and properties file path
            string transactionsFilePath = "transactions.csv";
            string propertiesFilePath = "properties.csv";

            // Read transactions and properties from CSV files
            var csvReaderService = new CsvReaderService();
            List<Transaction> transactions = csvReaderService.ReadTransactions(transactionsFilePath);
            List<Property> properties = csvReaderService.ReadProperties(propertiesFilePath);

            // Perform credit reference check
            var affordabilityService = new AffordabilityService();
            List<Property> affordableProperties = affordabilityService.CreditReferenceCheck(transactions, properties);

            // Output results
            Console.WriteLine("Affordable Properties:");
            foreach(var property in affordableProperties)
            {
                Console.WriteLine($"{property.Address} - Rent: {property.RentPerMonth:C}");
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}