using ECommerceBackend.Middleware;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Repositories;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
    Task<OrderItem> GetByIdAsync(int id);
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
}

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _context;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Product)
            .ToListAsync();
    }

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .FirstOrDefaultAsync(oi => oi.Id == id);
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
