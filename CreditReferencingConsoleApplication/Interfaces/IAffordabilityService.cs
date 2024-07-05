using System.Collections.Generic;
using CreditReferencingConsoleApplication.Models;

namespace CreditReferencingConsoleApplication.Interfaces
{
    public interface IAffordabilityService
    {
        List<Property> CreditReferenceCheck(List<Transaction> transactions, List<Property> properties);
    }
}