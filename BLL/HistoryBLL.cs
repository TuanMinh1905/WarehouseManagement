using System.Collections.Generic;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class HistoryBLL
    {
        private readonly UnitOfWork _unitOfWork;

        public HistoryBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Histories> GetAllHistory()
        {
            return _unitOfWork.History.GetAll();
        }

        public void AddHistory(Histories history)
        {
            _unitOfWork.History.Add(history);
        }
    }
}
