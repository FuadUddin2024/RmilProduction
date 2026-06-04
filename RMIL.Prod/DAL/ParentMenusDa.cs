using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class ParentMenusDa : BaseDa
    {
        public ParentMenusDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(ParentMenus menus)
        {
            try
            {
                ObjRmilcsDbEntities.ParentMenus.Add(menus);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(ParentMenus menus)
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

        public bool Delete(ParentMenus menus)
        {
            try
            {
                ObjRmilcsDbEntities.ParentMenus.Remove(menus);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<ParentMenus> GetAllParentMenus()
        {
            List<ParentMenus> menusList = ObjRmilcsDbEntities.ParentMenus.ToList();
            return menusList;
        }
        public ParentMenus GetParentMenusById(int id)
        {
            var q = ObjRmilcsDbEntities.ParentMenus.Find(id);
            return q;

        }
    }
}