using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCRUD.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayName("Customer Address")]
        public string CustomerAddress { get; set; }
        [DisplayName("PhoneNo")]
        public int PhoneNo { get; set; }
    }
}
