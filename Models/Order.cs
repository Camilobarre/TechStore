using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<ProductOrder> Products { get; set; }
        public string Status { get; set; } // "Pending", "Shipped", "Delivered"
        public DateTime CreatedDate { get; set; }
    }

}