using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CreditReferencingConsoleApplication.Models;

namespace CreditReferencingConsoleApplication.Services
{
    public class CsvReaderService
    {
        public List<Transaction> ReadTransactions(string filePath)
        {
            var transactions = new List<Transaction>();

            try
            {
                using(var reader = new StreamReader(filePath))
                {
                    // Skip non-header lines until the line with "Date" header is found
                    bool foundHeader = false;
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        if(line.IndexOf("Date", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            foundHeader = true;
                            break;
                        }
                    }

                    if(!foundHeader)
                    {
                        throw new InvalidDataException("Could not find headers with 'Date' in the CSV file.");
                    }

                    // Read and process each line as a transaction
                    while((line = reader.ReadLine()) != null)
                    {
                        if(string.IsNullOrWhiteSpace(line))
                            continue;

                        var fields = line.Split(',');

                        if(fields.Length < 6)
                        {
                            Console.WriteLine($"Skipping line with incomplete data: {line}");
                            continue;
                        }

                        // Parse fields into Transaction object
                        var transaction = new Transaction
                        {
                            MoneyOut = ParseDecimal(fields[3].Trim()) ?? 0m,
                            MoneyIn = ParseDecimal(fields[4].Trim()) ?? 0m
                        };

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

        private decimal? ParseDecimal(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                return null;

            if(decimal.TryParse(value.Replace("£", "").Trim(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal result))
                return result;

            return null;
        }

        public List<Property> ReadProperties(string filePath)
        {
            var properties = new List<Property>();

            try
            {
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);

                // Ensure there's at least one line with data
                if(lines.Length == 0)
                {
                    throw new InvalidDataException("CSV file is empty.");
                }

                // Process each line in the CSV file
                properties = lines
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => SplitCsvLine(line))              
                    .Where(fields => fields.Length >= 3)             
                    .Select(fields => new Property
                    {
                        Id = int.TryParse(fields[0].Trim('"'), out int id) ? id : 0,
                        Address = fields[1].Trim('"'),                               
                        RentPerMonth = decimal.TryParse(fields[2].Trim('"'), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal rent) ? rent : 0m // Parse RentPerMonth
                    })
                    .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error reading properties CSV file: {ex.Message}");
            }

            return properties;
        }

        // Custom CSV line splitter to handle quoted fields
        private string[] SplitCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            // Using LINQ to iterate through characters in the line
            var splitFields = line.Select((c, index) =>
            {
                if(c == '"')
                {
                    inQuotes = !inQuotes;
                }

                // Check for comma outside quotes to split fields
                if(c == ',' && !inQuotes)
                {
                    var field = line.Substring(startIndex, index - startIndex).Trim('"');
                    startIndex = index + 1;
                    return field;
                }

                // Return null if not at end of line and in quotes or not at comma
                return index == line.Length - 1 ? line.Substring(startIndex).Trim('"') : null;
            })
            .Where(field => field != null)
            .ToList();
            return splitFields.ToArray();
        }
    }
}