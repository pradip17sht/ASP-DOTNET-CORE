using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCRUD.Models
{
    public class SalesTransactionModel
    {
        public int TransactionID { get; set; }
        [DisplayName("Transaction Name")]
        public string TransactionName { get; set; }
        [DisplayName("Transaction Code")]
        public int Code { get; set; }
        [DisplayName("Transaction Amount")]
        public decimal Amount { get; set; }
    }
}
