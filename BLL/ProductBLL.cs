using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Contexts;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class ProductBLL
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductWithCategoryName> GetAllProducts()
        {
            return _unitOfWork.Products.GetAllWithCategoryName();
        }

        public void AddProduct(Products product)
        {
            _unitOfWork.Products.Add(product);
        }

        public void UpdateProduct(Products product)
        {
            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }

        public void DeleteProduct(int productId)
        {
            _unitOfWork.Products.Delete(productId);
        }

        public bool AddItemToCart(int productID, string name, int price)
        {
            return _unitOfWork.Carts.AddItemToCart(productID, name, price);
        }

        public bool IsProductNameExists(string productName)
        {
            return _unitOfWork.Products.IsProductNameExists(productName);
        }

        public int FindIdByName(string productName)
        {
            return _unitOfWork.Products.FindIdByName(productName);
        }

        public Products GetProductById(int productId)
        {
            return _unitOfWork.Products.GetById(productId);
        }

        public int GetQuantityById(int productId)
        {
            return _unitOfWork.Products.GetQuantityByProductId(productId);
        }

        public void UpdateProductQuantity(int productId, int quantitySold)
        {
            _unitOfWork.Products.UpdateProductQuantity(productId, quantitySold);
        }

        public DataTable SearchProducts(string searchTerm)
        {
            return _unitOfWork.Products.SearchProducts(searchTerm);
        }
    }
}
