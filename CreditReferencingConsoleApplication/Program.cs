using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CreditReferencingConsoleApplication.Models;
using CreditReferencingConsoleApplication.Services;

namespace CreditReferencingConsoleApplication
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Load appsettings.json
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var configJson = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<AppSettings>(configJson);

            // Read transactions and properties from CSV files
            var transactionsFilePath = Path.Combine(Directory.GetCurrentDirectory(), config.CsvFiles.TransactionsFilePath);
            var propertiesFilePath = Path.Combine(Directory.GetCurrentDirectory(), config.CsvFiles.PropertiesFilePath);

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

    public class AppSettings
    {
        public CsvFilesSettings CsvFiles { get; set; }
    }

    public class CsvFilesSettings
    {
        public string TransactionsFilePath { get; set; }
        public string PropertiesFilePath { get; set; }
    }
}