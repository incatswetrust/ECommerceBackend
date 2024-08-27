using AutoMapper;
using ECommerceBackend.Middleware;
using ECommerceBackend.Repositories;

namespace ECommerceBackend.Services;

public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);
    Task<OrderItemDto> GetOrderItemByIdAsync(int id);
    Task AddOrderItemAsync(int orderId, OrderItemDto orderItemDto);
    Task UpdateOrderItemAsync(int id, OrderItemDto orderItemDto);
    Task DeleteOrderItemAsync(int id);
}

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);
        return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
    }

    public async Task<OrderItemDto> GetOrderItemByIdAsync(int id)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
        {
            return null;
        }
        return _mapper.Map<OrderItemDto>(orderItem);
    }

    public async Task AddOrderItemAsync(int orderId, OrderItemDto orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        orderItem.OrderId = orderId;
        await _orderItemRepository.AddAsync(orderItem);
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemDto orderItemDto)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(id);
        if (orderItem == null)
        {
            return;
        }

        _mapper.Map(orderItemDto, orderItem);
        await _orderItemRepository.UpdateAsync(orderItem);
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        await _orderItemRepository.DeleteAsync(id);
    }
}

