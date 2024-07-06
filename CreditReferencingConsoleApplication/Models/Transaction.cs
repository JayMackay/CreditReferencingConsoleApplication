using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditReferencingConsoleApplication.Models
{
    public class Transaction
    {
        public string Date { get; set; }
        public string PaymentType { get; set; }
        public string Details { get; set; }
        public decimal MoneyOut { get; set; }
        public decimal MoneyIn { get; set; }
        public decimal Balance { get; set; }
    }
}
