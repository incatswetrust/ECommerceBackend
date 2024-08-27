namespace ECommerceBackend.Middleware;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } // NOT NULL constraint
    public string Description { get; set; } // NOT NULL constraint
    public decimal Price { get; set; } // NOT NULL constraint
    public int StockQuantity { get; set; } // NOT NULL constraint
}
