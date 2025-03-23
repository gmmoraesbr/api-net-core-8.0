namespace Base.Domain.Contracts.Repositories;

using Base.Domain.Entities.Aggregates.Order;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task AddAsync(Order order);
    Task<IEnumerable<Order>> GetAllAsync();
}