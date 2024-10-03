using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<ProductOrderDTO> Products { get; set; }
        public string Status { get; set; } // "Pending", "Shipped", "Delivered"
        public DateTime CreatedDate { get; set; }
    }

}