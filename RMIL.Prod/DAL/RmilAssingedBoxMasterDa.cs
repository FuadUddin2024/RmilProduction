using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RmilAssingedBoxMasterDa : BaseDa
    {
        public RmilAssingedBoxMasterDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(MasterBoxAssigned rmilProdMaster)
        {
            try
            {
                ObjRmilcsDbEntities.MasterBoxAssigned.Add(rmilProdMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }
        }
        public long CountAssingedProduct(string BoxBarcode)
        {
            long lastTransactionId =
                ObjRmilcsDbEntities.MasterBoxAssigned.Where(x=>x.BoxBarCode== BoxBarcode).Count();
            return lastTransactionId;
        }
        public List<vw_MasterBoxDetailsV> GetALLAssingedProducts(DateTime StartDate,DateTime EndDate)
        {
            var q = ObjRmilcsDbEntities.vw_MasterBoxDetailsV.Where(c => c.EntryDate>= StartDate && c.EntryDate <= EndDate).ToList();
            return q;
        }
        public bool CheckAllBarcode(string Barcode)
        {
            bool IsFound = false;
            var CheckBarcode = ObjRmilcsDbEntities.MasterBoxAssigned.Where(x=> x.ProductModelBarcode == Barcode).FirstOrDefault();
            if (CheckBarcode != null)
            {
                IsFound = true;
            }
            return IsFound;
        }
        public bool CheckAllMasterBarcode(string Barcode)
        {
            bool IsFound = false;
            var CheckBarcode = ObjRmilcsDbEntities.MasterBoxAssigned.Where(x => x.BoxBarCode == Barcode).FirstOrDefault();
            if (CheckBarcode != null)
            {
                IsFound = true;
            }
            return IsFound;
        }


        //public bool Update(RmilProdMaster rmilProdMaster)
        //{
        //    try
        //    {
        //        ObjRmilcsDbEntities.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Convert.ToBoolean("Error:" + ex);
        //    }
        //}

        //public bool Delete(RmilProdMaster rmilProdMaster)
        //{
        //    try
        //    {
        //        ObjRmilcsDbEntities.RmilProdMaster.Remove(rmilProdMaster);
        //        ObjRmilcsDbEntities.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Convert.ToBoolean("Error:" + ex);
        //    }

        //}
        //public List<RmilProdMaster> GetAllRmilProdMaster()
        //{
        //    List<RmilProdMaster> rmilProdMasterList = ObjRmilcsDbEntities.RmilProdMaster.ToList();
        //    return rmilProdMasterList;
        //}
        //public RmilProdMaster GetRmilProdMasterById(long id)
        //{
        //    var q = ObjRmilcsDbEntities.RmilProdMaster.Find(id);
        //    return q;

        //}
        //public List<RmilProdMaster> GetRmilProdMasterListById(int itemId)
        //{
        //    var q = ObjRmilcsDbEntities.RmilProdMaster.Where(c => c.PrModelId == itemId).ToList();
        //    return q;

        //}
        //public RmilProdMaster GetLastRecord()
        //{
        //    var q = ObjRmilcsDbEntities.RmilProdMaster.OrderByDescending(c => c.EntryDate).FirstOrDefault();
        //    return q;

        //}
        //public long LastTransactionId()
        //{
        //    long lastTransactionId =
        //        ObjRmilcsDbEntities.RmilProdMaster.OrderByDescending(c => c.RmPmId).Select(c => c.RmPmId).FirstOrDefault();
        //    return lastTransactionId;
        //}
        //public List<vw_RptAcProd> GetAllAcProdInfoBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_RptAcProd> qList = ObjRmilcsDbEntities.vw_RptAcProd.Where(c => c.PDate >= startDate && c.PDate <= endDate).ToList();
        //    return qList;
        //}
    }
}