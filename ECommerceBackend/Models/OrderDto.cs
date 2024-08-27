namespace ECommerceBackend.Middleware;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; } // NOT NULL constraint
    public DateTime OrderDate { get; set; } // NOT NULL constraint
    public decimal TotalAmount { get; set; } // NOT NULL constraint
    public string Status { get; set; } // NOT NULL constraint
    public List<OrderItemDto> OrderItems { get; set; } // NOT NULL constraint
}

