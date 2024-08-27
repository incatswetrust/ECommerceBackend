using AutoMapper;
using ECommerceBackend.Middleware;
using ECommerceBackend.Repositories;

namespace ECommerceBackend.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task AddOrderAsync(OrderDto orderDto);
    Task UpdateOrderAsync(int id, OrderDto orderDto);
    Task DeleteOrderAsync(int id);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return null;
        }
        return _mapper.Map<OrderDto>(order);
    }

    public async Task AddOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _orderRepository.AddAsync(order);
    }

    public async Task UpdateOrderAsync(int id, OrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return;
        }

        _mapper.Map(orderDto, order);
        await _orderRepository.UpdateAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
    }
}
