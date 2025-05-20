using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class DiscountBLL
    {
        private readonly UnitOfWork _unitOfWork;

        public DiscountBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Discounts> GetAllDiscounts()
        {
            return _unitOfWork.Discounts.GetAll();
        }
    }
}