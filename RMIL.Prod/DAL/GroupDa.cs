using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class GroupDa : BaseDa
    {
        public GroupDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Group group)
        {
            try
            {
                ObjRmilcsDbEntities.Group.Add(group);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Group group)
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

        public bool Delete(Group group)
        {
            try
            {
                ObjRmilcsDbEntities.Group.Remove(group);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Group> GetAllGroup()
        {
            List<Group> groupList = ObjRmilcsDbEntities.Group.ToList();
            return groupList;
        }
        public Group GetGroupById(int id)
        {
            var q = ObjRmilcsDbEntities.Group.Find(id);
            return q;

        }
    }
}