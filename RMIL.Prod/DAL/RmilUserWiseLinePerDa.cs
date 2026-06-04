using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RmilUserWiseLinePerDa : BaseDa
    {
        public RmilUserWiseLinePerDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(RmilUserWiseLinePer rmilUserWiseLinePer)
        {
            try
            {
                ObjRmilcsDbEntities.RmilUserWiseLinePer.Add(rmilUserWiseLinePer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(RmilUserWiseLinePer rmilUserWiseLinePer)
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

        public bool Delete(RmilUserWiseLinePer rmilUserWiseLinePer)
        {
            try
            {
                ObjRmilcsDbEntities.RmilUserWiseLinePer.Remove(rmilUserWiseLinePer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<RmilUserWiseLinePer> GetAllUserWiseLinePer()
        {
            List<RmilUserWiseLinePer> q = ObjRmilcsDbEntities.RmilUserWiseLinePer.ToList();
            return q;
        }
        public List<vw_UserWiseLineList> GetUserWiseLinePerList()
        {
            List<vw_UserWiseLineList> q = ObjRmilcsDbEntities.vw_UserWiseLineList.ToList();
            return q;
        }
        public RmilUserWiseLinePer GetUserWiseLinePerById(int id)
        {
            var q = ObjRmilcsDbEntities.RmilUserWiseLinePer.Find(id);
            return q;

        }
        public RmilUserWiseLinePer GetUserWiseLineInfo(string userId)
        {
            var q = ObjRmilcsDbEntities.RmilUserWiseLinePer.FirstOrDefault(c => c.UserId == userId && c.Active == true);
            return q;

        }
        public RmilUserWiseLinePer GetUserWiseLinePerById(string userId, int lineId)
        {
            var q = ObjRmilcsDbEntities.RmilUserWiseLinePer.FirstOrDefault(c => c.UserId == userId && c.LineId == lineId);
            return q;

        }
        public List<RmilUserWiseLinePer> GetUserWiseLinePerListByUserId(string userId)
        {
            List<RmilUserWiseLinePer> q =
                ObjRmilcsDbEntities.RmilUserWiseLinePer.Where(c => c.UserId == userId && c.Active == true).ToList();
            return q;
        }
    }
}