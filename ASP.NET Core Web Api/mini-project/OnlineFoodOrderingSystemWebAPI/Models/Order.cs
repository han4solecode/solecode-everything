using System.ComponentModel.DataAnnotations;

namespace OnlineFoodOrderingSystemWebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }

        [AllowedValues("Processed", "Delivered", "Canceled", ErrorMessage = "Order status must be either 'Processed', 'Delivered', or 'Canceled'")]
        public string OrderStatus { get; set; } = "Processed";
        
        public List<Menu> Menus { get; set; } = [];

        [StringLength(300, ErrorMessage = "Note exceed 300 character limit")]
        public string? CustomerNote { get; set; }

        public string? OrderNumber { get; set; }
        
        public virtual void CalculatedTotalOrder()
        {
            foreach (Menu menu in Menus)
            {
                TotalPrice += menu.Price;
            }
        }
    }
}