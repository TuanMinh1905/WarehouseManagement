namespace WarehouseProProject.DAL.Repository
{
    public class DiscountRepository : RepositoryBase<Discounts>
    {
        public DiscountRepository(WarehouseContext context) : base(context) { }
    }
}