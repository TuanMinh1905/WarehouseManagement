using System.Collections.Generic;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class OrderBLL
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _unitOfWork.Orders.GetAll();
        }

        public void AddOrder(Order order)
        {
            _unitOfWork.Orders.Add(order);
        }

        public List<Order> GetOrdersByTransactionId(string transactionId)
        {
            return _unitOfWork.Orders.GetOrdersByTransactionId(transactionId);
        }
    }
}
