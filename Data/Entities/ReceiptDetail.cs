using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class ReceiptDetail : BaseEntity
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public double DiscountUnitPrice { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }


        public Receipt Receipt { get; set; }
        public Product Product { get; set; }
    }
}
