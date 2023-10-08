using Microsoft.EntityFrameworkCore;
using ReSushi.Models;

namespace ReSushi.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly EFDataContext _efDataContext;
        public OrderRepository(EFDataContext context)
        {
            _efDataContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _efDataContext.Orders.ToListAsync();
        }
        public async Task<Order> GetOrderByID(int ID)
        {
            return await _efDataContext.Orders.FindAsync(ID);
        }
        public async Task<Order> InsertOrder(Order objOrder)
        {
            _efDataContext.Orders.Add(objOrder);
            await _efDataContext.SaveChangesAsync();
            return objOrder;
        }
        public async Task<Order> UpdateOrder(Order objOrder)
        {
            _efDataContext.Entry(objOrder).State = EntityState.Modified;
            await _efDataContext.SaveChangesAsync();
            return objOrder;
        }
        public bool DeleteOrder(int ID)
        {
            bool result = false;
            var order = _efDataContext.Orders.Find(ID);
            if (order != null)
            {
                _efDataContext.Entry(order).State = EntityState.Deleted;
                _efDataContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
