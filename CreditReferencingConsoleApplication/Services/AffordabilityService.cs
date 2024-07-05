using System;
using System.Collections.Generic;
using System.Linq;
using CreditReferencingConsoleApplication.Interfaces;
using CreditReferencingConsoleApplication.Models;

namespace CreditReferencingConsoleApplication.Services
{
    public class AffordabilityService : IAffordabilityService
    {
        public List<Property> CreditReferenceCheck(List<Transaction> transactions, List<Property> properties)
        {
            // Total monthly income and expenditure
            decimal totalIncome = transactions
                .Where(t => t.MoneyIn > 0)
                .Sum(t => t.MoneyIn);

            decimal totalExpenditure = transactions
                .Where(t => t.MoneyOut > 0)
                .Sum(t => t.MoneyOut);

            // Monthly disposable income
            decimal monthlyDisposableIncome = totalIncome - totalExpenditure;

            // Calculate affordability threshold only if properties list is not empty
            decimal affordabilityThreshold = 0;
            if(properties.Any())
            {
                affordabilityThreshold = properties.Max(p => p.RentPerMonth) * 1.25m;
            }

            // Filter properties that are affordable based on disposable income
            List<Property> affordableProperties = properties
                .Where(p => p.RentPerMonth <= monthlyDisposableIncome - affordabilityThreshold)
                .ToList();

            return affordableProperties;
        }
    }
}