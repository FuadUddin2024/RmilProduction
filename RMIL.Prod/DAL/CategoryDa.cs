using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class CategoryDa : BaseDa
    {
        public CategoryDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Category category)
        {
            try
            {
                ObjRmilcsDbEntities.Category.Add(category);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Category category)
        {
            try
            {
                ObjRmilcsDbEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }
        }

        public bool Delete(Category category)
        {
            try
            {
                ObjRmilcsDbEntities.Category.Remove(category);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Category> GetAllCategory()
        {
            List<Category> categoryList = ObjRmilcsDbEntities.Category.ToList();
            return categoryList;
        }
        public List<Category> GetCategoryListById(int categoryId)
        {
            List<Category> categoryList = ObjRmilcsDbEntities.Category.Where(c => c.CategoryId == categoryId).ToList();
            return categoryList;
        }
        public List<vw_CategoryList> GetCategoryList()
        {
            List<vw_CategoryList> categoryList = ObjRmilcsDbEntities.vw_CategoryList.ToList();
            return categoryList;
        }
        public Category GetCategoryById(int id)
        {
            var q = ObjRmilcsDbEntities.Category.Find(id);
            return q;
        }
    }
}