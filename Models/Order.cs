using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public List<ProductOrder> Products { get; set; } = new List<ProductOrder>();

        [Required]
        public string Status { get; set; } // "Pending", "Shipped", "Delivered"

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}