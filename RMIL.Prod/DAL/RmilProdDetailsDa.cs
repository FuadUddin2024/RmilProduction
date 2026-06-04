using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RmilProdDetailsDa : BaseDa
    {
        public RmilProdDetailsDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(RmilProdDetails rmilProdDetails)
        {
            try
            {
                ObjRmilcsDbEntities.RmilProdDetails.Add(rmilProdDetails);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(RmilProdDetails rmilProdDetails)
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

        public bool Delete(RmilProdDetails rmilProdDetails)
        {
            try
            {
                ObjRmilcsDbEntities.RmilProdDetails.Remove(rmilProdDetails);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public List<RmilProdDetails> GetAllRmilProdDetails()
        {
            List<RmilProdDetails> rmilProdDetailsList = ObjRmilcsDbEntities.RmilProdDetails.ToList();
            return rmilProdDetailsList;
        }
        public RmilProdDetails GetRmilProdDetailsById(int id)
        {
            var q = ObjRmilcsDbEntities.RmilProdDetails.Find(id);
            return q;

        }
        public RmilProdDetails GetRmilProdDetailsByBarcode(string barcode)
        {
            var q = ObjRmilcsDbEntities.RmilProdDetails.FirstOrDefault(c => c.DBarcode == barcode);
            return q;

        }
        public List<RmilProdDetails> GetRmilProdDetailsListById(int itemId)
        {
            var q = ObjRmilcsDbEntities.RmilProdDetails.Where(c => c.PrModelId == itemId).ToList();
            return q;

        }
        public List<vw_ProdDetails> GetRmilBarcodeListById(long mId)
        {
            var q = ObjRmilcsDbEntities.vw_ProdDetails.Where(c => c.RmPmId == mId).ToList();
            return q;

        }
        public List<RmilProdDetails> GetRmilProdDetailsList()
        {
            List<RmilProdDetails> rmilProdDetailsList = ObjRmilcsDbEntities.RmilProdDetails.Where(c => c.SendMsg == null && c.CustContactNo != null).ToList();
            return rmilProdDetailsList;
        }
        public RmilProdDetails GetRmilProdDetailsByMobileNo(string mobileNo)
        {
            var q = ObjRmilcsDbEntities.RmilProdDetails.FirstOrDefault(c => c.CustContactNo == mobileNo);
            return q;

        }
        public List<vw_RmBarProdInfo> GetModelInfoByBarCode(string barCode)
        {
            List<vw_RmBarProdInfo> qList = ObjRmilcsDbEntities.vw_RmBarProdInfo.Where(c => c.DBarcode == barCode).ToList();
            return qList;
        }
        //public List<vw_FrAssignWiseProdList> GetProductionListByItemId(int itemid)
        //{
        //    List<vw_FrAssignWiseProdList> productionList = ObjRmilcsDbEntities.vw_FrAssignWiseProdList.Where(c => c.FrId == itemid).ToList();
        //    return productionList;
        //}
        //public List<vw_AcWarrStartByTech> GetTechWarrantyDataBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_AcWarrStartByTech> wList = ObjRmilcsDbEntities.vw_AcWarrStartByTech.Where(c => c.SalesDate >= startDate && c.SalesDate <= endDate).ToList();
        //    return wList;
        //}
        //public List<vw_AcWarrStartByDistributor> GetDistWarrantyDataBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_AcWarrStartByDistributor> wList = ObjRmilcsDbEntities.vw_AcWarrStartByDistributor.Where(c => c.SalesDate >= startDate && c.SalesDate <= endDate).ToList();
        //    return wList;
        //}
        //public List<vw_AcWarrStartByDealer> GetDealerWarrantyDataBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_AcWarrStartByDealer> wList = ObjRmilcsDbEntities.vw_AcWarrStartByDealer.Where(c => c.SalesDate >= startDate && c.SalesDate <= endDate).ToList();
        //    return wList;
        //}
        //public List<vw_AcWarrStartByCustomer> GetCustomerWarrantyDataBetweenTwoDate(DateTime startDate, DateTime endDate)
        //{
        //    List<vw_AcWarrStartByCustomer> wList = ObjRmilcsDbEntities.vw_AcWarrStartByCustomer.Where(c => c.SalesDate >= startDate && c.SalesDate <= endDate).ToList();
        //    return wList;
        //}
    }
}