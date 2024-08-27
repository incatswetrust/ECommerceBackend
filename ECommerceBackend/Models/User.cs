namespace ECommerceBackend.Middleware;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisteredDate { get; set; }
    public bool IsAdmin { get; set; }
}