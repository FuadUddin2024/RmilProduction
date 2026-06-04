using RMIL.Prod.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMIL.Prod.DAL
{
    public class DepoDa : BaseDa
    {
        public DepoDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        //public List<DepotWeight> GetAllDepoList()
        //{
        //    List<DepotWeight> categoryList = ObjRmilcsDbEntities.DepotWeight.ToList();
        //    return categoryList;
        //}
        public List<DepotWeight> GetAllDepoList()
        {
            List<DepotWeight> categoryList = ObjRmilcsDbEntities.DepotWeight
                .ToList()
                .Select(x => new DepotWeight
                {
                    DepotId = x.DepotId,
                    DepotName = x.DepoCode == null
                        ? x.DepotName
                        : x.DepotName + " - " + x.DepoCode,

                    DepoCode = x.DepoCode,
                    EntryDate = x.EntryDate
                }).ToList();

            return categoryList;
        }
        public DepoWiseUserAssign GetUserWiseDepoPerById(string userId, int lineId)
        {
            var q = ObjRmilcsDbEntities.DepoWiseUserAssign.FirstOrDefault(c => c.UserId == userId && c.DepoID == lineId);
            return q;
        }
        public DepoWiseUserAssign GetUserWiseDepoPerByUserId(int userId)
        {
            var q = ObjRmilcsDbEntities.DepoWiseUserAssign.FirstOrDefault(c => c.DepoPermissionID == userId);
            return q;
        }
        public bool AddNewUserPermission(DepoWiseUserAssign UserID)
        {
            try
            {
                ObjRmilcsDbEntities.DepoWiseUserAssign.Add(UserID);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }
        }
        public List<vw_DepoWiseUserAssign> GetDepoWiseList()
        {
            List<vw_DepoWiseUserAssign> q = ObjRmilcsDbEntities.vw_DepoWiseUserAssign.ToList();
            return q;
        }
        public DepotWeight GetAllDepoBydepo(int ID)
        {
            DepotWeight q = ObjRmilcsDbEntities.DepotWeight.Where(x=>x.DepotId== ID).FirstOrDefault();
            return q;
        }
        public bool Update(DepoWiseUserAssign rmilUserWiseLinePer)
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
        public DepoWiseUserAssign GetUserWiseDepoID(string UserID)
        {
            var assingeddepo = ObjRmilcsDbEntities.DepoWiseUserAssign.Where(x => x.UserId == UserID).FirstOrDefault();
            return assingeddepo;
        }
        public DepotWeight GetSingleDepo(int DepoID)
        {
            var assingeddepo = ObjRmilcsDbEntities.DepotWeight.Where(x => x.DepotId == DepoID).FirstOrDefault();
            return assingeddepo;
        }
    }
    
}