using ReSushi.Models;

namespace ReSushi.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderByID(int ID);
        Task<Order> InsertOrder(Order objOrder);
        Task<Order> UpdateOrder(Order objOrder);
        bool DeleteOrder(int ID);
    }
}
