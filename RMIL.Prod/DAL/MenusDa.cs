using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class MenusDa : BaseDa
    {
        public MenusDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Menus menus)
        {
            try
            {
                ObjRmilcsDbEntities.Menus.Add(menus);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Menus menus)
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

        public bool Delete(Menus menus)
        {
            try
            {
                ObjRmilcsDbEntities.Menus.Remove(menus);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Menus> GetAllMenus()
        {
            List<Menus> menusList = ObjRmilcsDbEntities.Menus.ToList();
            return menusList;
        }
        public List<vw_MenuList> GetMenusList()
        {
            List<vw_MenuList> menusList = ObjRmilcsDbEntities.vw_MenuList.ToList();
            return menusList;
        }
        public List<Menus> GetMenuListByParentMenuId(int parentMenuId)
        {
            List<Menus> menusList = ObjRmilcsDbEntities.Menus.Where(c => c.ParentMenuId == parentMenuId).ToList();
            return menusList;
        }
        public Menus GetMenusById(int id)
        {
            var q = ObjRmilcsDbEntities.Menus.Find(id);
            return q;

        }
        public Menus GetMenusByTitle(string title)
        {
            Menus q = ObjRmilcsDbEntities.Menus.FirstOrDefault(a => a.Title == title);
            return q;
        }
        public Menus GetMenusByNameParentMenuId(string title, int parentMenuId)
        {
            var q = ObjRmilcsDbEntities.Menus.FirstOrDefault(c => c.Title == title && c.ParentMenuId == parentMenuId);
            return q;

        }
        public List<prc_MenuListByUser_Result> GetMenuListByUser(string userId, int parentMenuId)
        {
            List<prc_MenuListByUser_Result> menuList = ObjRmilcsDbEntities.prc_MenuListByUser(userId, parentMenuId).ToList();
            return menuList;
        }
    }
}