using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RmilProdMasterDa : BaseDa
    {
        public RmilProdMasterDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(RmilProdMaster rmilProdMaster)
        {
            try
            {
                ObjRmilcsDbEntities.RmilProdMaster.Add(rmilProdMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(RmilProdMaster rmilProdMaster)
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

        public bool Delete(RmilProdMaster rmilProdMaster)
        {
            try
            {
                ObjRmilcsDbEntities.RmilProdMaster.Remove(rmilProdMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public List<RmilProdMaster> GetAllRmilProdMaster()
        {
            List<RmilProdMaster> rmilProdMasterList = ObjRmilcsDbEntities.RmilProdMaster.ToList();
            return rmilProdMasterList;
        }
        public RmilProdMaster GetRmilProdMasterById(long id)
        {
            var q = ObjRmilcsDbEntities.RmilProdMaster.Find(id);
            return q;

        }
        public List<RmilProdMaster> GetRmilProdMasterListById(int itemId)
        {
            var q = ObjRmilcsDbEntities.RmilProdMaster.Where(c => c.PrModelId == itemId).ToList();
            return q;

        }
        public RmilProdMaster GetLastRecord()
        {
            var q = ObjRmilcsDbEntities.RmilProdMaster.OrderByDescending(c => c.EntryDate).FirstOrDefault();
            return q;

        }
        public long LastTransactionId()
        {
            long lastTransactionId =
                ObjRmilcsDbEntities.RmilProdMaster.OrderByDescending(c => c.RmPmId).Select(c => c.RmPmId).FirstOrDefault();
            return lastTransactionId;
        }
        //public List<vw_RptAcProd> GetAllAcProdInfoBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_RptAcProd> qList = ObjRmilcsDbEntities.vw_RptAcProd.Where(c => c.PDate >= startDate && c.PDate <= endDate).ToList();
        //    return qList;
        //}
    }
}