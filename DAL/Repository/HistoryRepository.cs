namespace WarehouseProProject.DAL.Repository
{
    public class HistoryRepository : RepositoryBase<Histories>
    {
        public HistoryRepository(WarehouseContext context) : base(context) { }
    }
}