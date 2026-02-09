/*
 * 
 * This class represents a complete sale or transaction
 * 
 */

using System;
using System.Collections.Generic;

namespace Pos_system.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        //public decimal TotalAmount { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach(var item in Items)
                {
                    total += item.TotalPrice;
                }
                return total;
            }
        }

        
    }
}
