using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditReferencingConsoleApplication.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public decimal RentPerMonth { get; set; }
    }
}
