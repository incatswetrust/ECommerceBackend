using AutoMapper;
using ECommerceBackend.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderItemsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems(int orderId)
    {
        var orderItems = await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Product)
            .ToListAsync();

        var orderItemDtos = _mapper.Map<List<OrderItemDto>>(orderItems);
        return Ok(orderItemDtos);
    }

    [HttpPost("{orderId}")]
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(int orderId, OrderItemDto orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        orderItem.OrderId = orderId;

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderItems), new { orderId = orderItem.OrderId }, orderItemDto);
    }

    [HttpPut("{orderId}/{itemId}")]
    public async Task<IActionResult> UpdateOrderItem(int orderId, int itemId, OrderItemDto orderItemDto)
    {
        if (itemId != orderItemDto.ProductId)
        {
            return BadRequest();
        }

        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.Id == itemId);

        if (orderItem == null)
        {
            return NotFound();
        }

        _mapper.Map(orderItemDto, orderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{orderId}/{itemId}")]
    public async Task<IActionResult> DeleteOrderItem(int orderId, int itemId)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.Id == itemId);

        if (orderItem == null)
        {
            return NotFound();
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
