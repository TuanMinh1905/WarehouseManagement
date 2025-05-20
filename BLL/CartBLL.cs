using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class CartBLL
    {
        private readonly UnitOfWork _unitOfWork;
        public CartBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Carts> GetAllCarts()
        {
            return _unitOfWork.Carts.GetAll();
        }

        public void UpdateCart(Carts cart)
        {
            _unitOfWork.Carts.Update(cart);
            _unitOfWork.Save();
        }

        public void DeleteCart(int cartId)
        {
            _unitOfWork.Carts.Delete(cartId);
        }

        public void DeleteAllCarts()
        {
            _unitOfWork.Carts.ResetID();
            _unitOfWork.Carts.DeleteAll<Carts>();
        }
        public decimal GetTotalCartPrice()
        {
            return _unitOfWork.Carts.GetTotalCartPrice();
        }

        public void CalculateDiscount(string totalText, string discountName, out string discountText, out string totalAfterDiscountText)
        {
            _unitOfWork.Carts.CalculateDiscount(totalText, discountName, out discountText, out totalAfterDiscountText);
        }
        public void CalculateChange(string totalText, string receiveText, out string changeText)
        {
            _unitOfWork.Carts.CalculateChange(totalText, receiveText, out changeText);
        }
    }
}
        