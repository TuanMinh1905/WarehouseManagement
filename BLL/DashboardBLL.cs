using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseProProject.DAL.Repository;

namespace BLL
{
    public class DashboardBLL
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;

        public DashboardBLL(TransactionRepository transaction, ProductRepository product, CategoryRepository category)
        {
            _productRepository = product;
            _transactionRepository = transaction;
            _categoryRepository = category;
        }

        // Lấy tổng sản phẩm
        public int GetTotalProducts()
        {
            return _productRepository.GetTotalProducts();
        }

        // Lấy tổng doanh thu
        public decimal GetTotalRevenue()
        {
            return _transactionRepository.GetTotalRevenue();
        }

        // Lấy tổng giao dịch
        public int GetTotalTransactions()
        {
            return _transactionRepository.GetTotalTransactions();
        }

        // Lấy tổng loại sản phẩm
        public int GetTotalCategories()
        {
            return _categoryRepository.GetTotalCategories();
        }
    }

}
