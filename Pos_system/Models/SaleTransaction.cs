using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos_system.Models
{
    public class SaleTransaction
    {
        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
