using AutoMapper;
using ECommerceBackend.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrdersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        var orders = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        return Ok(orderDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            return NotFound();
        }
        var orderDto = _mapper.Map<OrderDto>(order);
        return Ok(orderDto);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
    {
        if (id != orderDto.Id)
        {
            return BadRequest();
        }

        var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            return NotFound();
        }

        _mapper.Map(orderDto, order);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
