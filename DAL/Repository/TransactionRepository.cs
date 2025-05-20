using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace WarehouseProProject.DAL.Repository
{
    public class TransactionRepository : RepositoryBase<Transactions>
    {
        public TransactionRepository(WarehouseContext context) : base(context) { }
        public decimal GetTotalRevenue()
        {
            return _context.Transactions
                .Where(t => t.Total != null)
                .AsEnumerable()
                .Sum(t => decimal.TryParse(t.Total, out var total) ? total : 0);
        }

        public int GetTotalTransactions()
        {
            return _context.Transactions.Count();
        }

        private readonly HashSet<string> generatedIds = new HashSet<string>();

        public string GenerateTransactionId()
        {
            string uniqueTransactionId;

            do
            {
                uniqueTransactionId = Guid.NewGuid().ToString();
            }
            while (generatedIds.Contains(uniqueTransactionId));

            generatedIds.Add(uniqueTransactionId);

            return uniqueTransactionId;
        }

        public void SaveTransaction(Transactions transactions)
        {
            var transaction = new Transactions();
            {
                transaction.TransactionId = transactions.TransactionId;
                transaction.Subtotal = transactions.Subtotal;
                transaction.Cash = transactions.Cash;
                transaction.DiscountPercent = transactions.DiscountPercent;
                transaction.DiscountAmount = transactions.DiscountAmount;
                transaction.Total = transactions.Total;
                transaction.Change = transactions.Change;
                transaction.Date = DateTime.Now;
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}