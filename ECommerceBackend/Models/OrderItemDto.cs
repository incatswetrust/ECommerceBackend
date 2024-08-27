namespace ECommerceBackend.Middleware;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; } // NOT NULL constraint
    public int ProductId { get; set; } // NOT NULL constraint
    public int Quantity { get; set; } // NOT NULL constraint
    public decimal UnitPrice { get; set; } // NOT NULL constraint
    public string ProductName { get; set; } // Can be used to display the product name
}

