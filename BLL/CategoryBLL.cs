using System.Collections.Generic;
using WarehouseProProject.DAL;

namespace WarehouseProProject.BLL
{
    public class CategoryBLL
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryBLL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Categories> GetAllCategories()
        {
            return _unitOfWork.Categories.GetAll();
        }

        public void AddCategory(Categories category)
        {
            _unitOfWork.Categories.Add(category);
        }

        public void UpdateCategory(Categories category)
        {
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();
        }

        public void DeleteCategory(int categoryId)
        {
            _unitOfWork.Categories.Delete(categoryId);
        }

        public int FindIdByName(string name)
        {
            return _unitOfWork.Categories.FindIdByName(name);
        }
        public bool IsCategoryItemExists(string name)
        {
            return _unitOfWork.Categories.IsCategoryNameExists(name);
        }
    }
}
