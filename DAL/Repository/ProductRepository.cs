using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System;

namespace WarehouseProProject.DAL.Repository
{
    public class ProductRepository : RepositoryBase<Products>
    {
        public ProductRepository(WarehouseContext context) : base(context) { }

        public int GetTotalProducts()
        {
            return _context.Products.Count();
        }

        public IEnumerable<ProductWithCategoryName> GetAllWithCategoryName()
        {
            return _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductWithCategoryName
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CategoryName = p.Category.CategoryItem
                })
                .ToList();
        }
        public bool IsProductNameExists(string productName)
        {
            return _context.Products.Any(p => p.ProductName == productName);
        }

        public int FindIdByName(string productName)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductName == productName);
            return product?.ProductID ?? 0;
        }

        public int GetQuantityByProductId(int productID)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productID);
            return product != null ? product.Quantity : 0;
        }

        public void UpdateProductQuantity(int productId, int quantitySold)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                product.Quantity -= quantitySold;
                _context.SaveChanges();            
            }
        }

        public DataTable SearchProducts(string searchTerm)
        {
            DataTable dt = new DataTable();

            var products = (from p in _context.Products
                            join c in _context.Categories on p.CategoryID equals c.CategoryID
                            where p.ProductName.Contains(searchTerm) || c.CategoryItem.Contains(searchTerm)
                            select new
                            {
                                p.ProductID,
                                p.ProductName,
                                p.Quantity,
                                p.Price,
                                CategoryName = c.CategoryItem
                            }).ToList();

            dt = ConvertToDataTable(products);

            return dt;
        }

        private DataTable ConvertToDataTable<T>(List<T> products)
        {
            DataTable dt = new DataTable();

            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Add rows to DataTable
            foreach (var product in products)
            {
                var values = properties.Select(p => p.GetValue(product, null)).ToArray();
                dt.Rows.Add(values);
            }

            return dt;
        }


    }
}
