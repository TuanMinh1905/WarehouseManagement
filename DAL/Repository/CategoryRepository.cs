using System.Linq;

namespace WarehouseProProject.DAL.Repository
{
    public class CategoryRepository : RepositoryBase<Categories>
    {
        public CategoryRepository(WarehouseContext context) : base(context) { }
        public int GetTotalCategories()
        {
            return _context.Categories.Count();
        }

        public int FindIdByName(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryItem == categoryName);
            return category != null ? category.CategoryID : 0;
        }
        public bool IsCategoryNameExists(string categoryName)
        {
            return _context.Categories.Any(c => c.CategoryItem == categoryName);
        }
    }
}