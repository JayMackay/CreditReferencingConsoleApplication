using System.Collections.Generic;
using CreditReferencingConsoleApplication.Models;

namespace CreditReferencingConsoleApplication.Interfaces
{
    public interface ICsvReaderService
    {
        List<Transaction> ReadTransactions(string filePath);
        List<Property> ReadProperties(string filePath);
    }
}