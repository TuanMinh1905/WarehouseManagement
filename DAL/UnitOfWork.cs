using System;
using WarehouseProProject.DAL.Repository;

namespace WarehouseProProject.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly WarehouseContext _context;

        public UnitOfWork(WarehouseContext context)
        {
            _context = context;
            Products = new ProductRepository(context);
            Categories = new CategoryRepository(context);
            Users = new UserRepository(context);
            History = new HistoryRepository(context);
            Transaction = new TransactionRepository(context);
            Carts = new CartReposotory(context);
            Discounts = new DiscountRepository(context);
            Orders = new OrderRepository(context);
        }

        public ProductRepository Products { get; private set; }
        public CategoryRepository Categories { get; private set; }
        public UserRepository Users { get; private set; }
        public HistoryRepository History { get; private set; }
        public TransactionRepository Transaction { get; private set; }
        public CartReposotory Carts { get; private set; }
        public DiscountRepository Discounts { get; private set; }
        public OrderRepository Orders { get; private set; }


        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
