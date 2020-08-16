using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCRUD.Models
{
    public class InvoiceModel
    {
        public int InvoiceID { get; set; }
        [DisplayName("Items")]
        public string Items { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Tax")]
        public decimal Tax { get; set; }
        [DisplayName("Amount")]
        public decimal Amount { get; set; }
    }
}
