using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace B2BWholesale.Core.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [Required, MaxLength(150)]
        public string ItemName { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Size { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Table("Agent")]
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        [Required, MaxLength(150)]
        public string AgentName { get; set; } = string.Empty;
        [Required, MaxLength(250)]
        public string Address { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
    }

    [Table("Users")]
    public class AppUser
    {
        [Key]
        public int UserID { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string email { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        public string password { get; set; } = string.Empty;
        [Column("lock")]
        public bool IsLocked { get; set; }
        [Required, MaxLength(20)]
        public string Role { get; set; } = "Agent";
        public int? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public Agent? Agent { get; set; }
    }

    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int AgentID { get; set; }
        [ForeignKey("AgentID")]
        public Agent? Agent { get; set; }
        [Required, MaxLength(30)]
        public string OrderStatus { get; set; } = "Pending";
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }

    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order? Order { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item? Item { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitAmount { get; set; }
    }
}