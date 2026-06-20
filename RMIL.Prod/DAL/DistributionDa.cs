using RMIL.Prod.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RMIL.Prod.DAL
{
    public class DistributionDa:BaseDa
    {
        public DistributionDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(DistributionWeight rmilProdMaster)
        {
            try
            {
                ObjRmilcsDbEntities.DistributionWeight.Add(rmilProdMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }
        }
        public List<DistributionDeliveryType> GetAllDistributionType()
        {
            var Types = ObjRmilcsDbEntities.DistributionDeliveryType.Where(x=>x.IsActive==1 && x.DeliveryTypeID !=1).ToList();
            return Types;
        }
        public List<MasterBoxAssigned> GetAllAssingedBox(string Boxbarcode)
        {
            var Types = ObjRmilcsDbEntities.MasterBoxAssigned.Where(x => x.BoxBarCode == Boxbarcode).ToList();
            return Types;
        }
        public List<MasterBoxAssigned> GetAllAssingedBoxSingle(string Boxbarcode)
        {
            var Types = ObjRmilcsDbEntities.MasterBoxAssigned.Where(x => x.ProductModelBarcode == Boxbarcode).ToList();
            return Types;
        }
        public List<DistributionWeight> GetAllSingleDistribution(string Boxbarcode)
        {
            var Types = ObjRmilcsDbEntities.DistributionWeight.Where(x => x.BarcodeNo == Boxbarcode).ToList();
            return Types;
        }
        public bool Update(DistributionWeight model)
        {
            try
            {
                var local = ObjRmilcsDbEntities.DistributionWeight
                                .Local
                                .FirstOrDefault(x => x.ThaDstbId == model.ThaDstbId);

                if (local != null)
                {
                    ObjRmilcsDbEntities.Entry(local).State = EntityState.Detached;
                }

                // Attach and mark as modified
                ObjRmilcsDbEntities.DistributionWeight.Attach(model);
                ObjRmilcsDbEntities.Entry(model).State = EntityState.Modified;

                ObjRmilcsDbEntities.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                // Optional: log ex.Message
                return false;
            }
        }


        // Depo Return
        public MasterBoxAssigned GetProductDetailsBox(string BarcodeNo)
        {
            var distribution = ObjRmilcsDbEntities.MasterBoxAssigned.FirstOrDefault(x => x.BoxBarCode == BarcodeNo);
            return distribution;
        }
        public DistributionWeight GetProductDetails(string BarcodeNo)
        {
            var distribution = ObjRmilcsDbEntities.DistributionWeight.FirstOrDefault(x=>x.BarcodeNo== BarcodeNo);
            return distribution;
        }
        public bool InsertLog(DepoReturnLog Logfile)
        {
            bool IsAdded = true;
            try
            {
                ObjRmilcsDbEntities.DepoReturnLog.Add(Logfile);
                ObjRmilcsDbEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                IsAdded = false;
            }

            return IsAdded;
        }
        public bool UpdateReturnProductInfo(string Barcode)
        {
            bool IsUpdated = true;
            try
            {
                int result = ObjRmilcsDbEntities.Database.ExecuteSqlCommand(
                        "EXEC SP_ResetDistributionWeightV @BarcodeNo",
                        new SqlParameter("@BarcodeNo", Barcode));
            }
            catch(Exception ex)
            {
                IsUpdated = false;
            }
            return IsUpdated;
        }


        //  Report
        public List<vw_DistributionDetailsVVVV> GetALLDistributionProducts(DateTime StartDate, DateTime EndDate)
        {
            var q = ObjRmilcsDbEntities.vw_DistributionDetailsVVVV.Where(c => c.EntryDate >= StartDate && c.EntryDate <= EndDate).OrderByDescending(x => x.SendDate).ToList();
            return q;
        }

        // Get ALl Product barcode
        public List<VW_ProductModelBarcodeDetails> GetProductDetailsMaster(string Barcode)
        {
            var q = ObjRmilcsDbEntities.VW_ProductModelBarcodeDetails.Where(c => c.DBarcode == Barcode).ToList();
            return q;
        }
        public List<VW_RmilProductBarcodeDetails> GetProductDetailsMasterProduct(string Barcode)
        {
            var q = ObjRmilcsDbEntities.VW_RmilProductBarcodeDetails.Where(c => c.DBarcode == Barcode).ToList();
            return q;
        }

        // Depo to Depo
        public bool InsertLog(DepoToDepoLogFileWeight LogFile)
        {
            try
            {
                ObjRmilcsDbEntities.DepoToDepoLogFileWeight.Add(LogFile);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }
        }
    }
}