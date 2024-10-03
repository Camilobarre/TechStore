using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechStore.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<ProductOrderDTO> Products { get; set; } = new List<ProductOrderDTO>();

        [Required]
        public string Status { get; set; } // "Pending", "Shipped", "Delivered"

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}