using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class TransactionBLL
    {
        private readonly UnitOfWork _unitOfWork;
        public TransactionBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Transactions> GetAllTransaction()
        {
            return _unitOfWork.Transaction.GetAll();
        }

        public void UpdateTransaction(Transactions transaction)
        {
            _unitOfWork.Transaction.Update(transaction);
            _unitOfWork.Save();
        }

        public void DeleteTransaction(int Id)
        {
            _unitOfWork.Transaction.Delete(Id);
        }

        public string GenerateTransactionId()
        {
            return _unitOfWork.Transaction.GenerateTransactionId();
        }
        public void SaveTransaction(Transactions transaction)
        {
            _unitOfWork.Transaction.SaveTransaction(transaction);
        }
    }
}