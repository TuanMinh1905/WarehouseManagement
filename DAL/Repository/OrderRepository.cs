using System.Collections.Generic;
using System.Linq;

namespace WarehouseProProject.DAL.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(WarehouseContext context) : base(context) { }

        public List<Order> GetOrdersByTransactionId(string transactionId)
        {
            var orders = _context.Orders
                                 .Where(order => order.TransactionId == transactionId)
                                 .ToList();
            return orders;
        }
    }
}