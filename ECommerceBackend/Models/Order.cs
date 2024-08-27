namespace ECommerceBackend.Middleware;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    // Navigation property
    public User User { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}