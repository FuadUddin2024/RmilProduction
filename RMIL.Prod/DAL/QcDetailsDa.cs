using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcDetailsDa : BaseDa
    {
        public QcDetailsDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcDetails qcDetails)
        {
            try
            {
                ObjRmilcsDbEntities.QcDetails.Add(qcDetails);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcDetails qcDetails)
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
        public void UpdateData()
        {
            ObjRmilcsDbEntities.SaveChanges();
        }
        public bool Delete(QcDetails qcDetails)
        {
            try
            {
                ObjRmilcsDbEntities.QcDetails.Remove(qcDetails);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcDetails> GetAllQcDetails()
        {
            List<QcDetails> qcDetailsList = ObjRmilcsDbEntities.QcDetails.ToList();
            return qcDetailsList;
        }
        public QcDetails GetQcDetailsById(int id)
        {
            var q = ObjRmilcsDbEntities.QcDetails.Find(id);
            return q;

        }
        public List<QcDetails> GetQcDetailsListByBarcode(string barcode)
        {
            List<QcDetails> q = ObjRmilcsDbEntities.QcDetails.Where(a => a.DBarcodeNo == barcode).ToList();
            return q;
        }
        public List<QcDetails> GetQcDetailsListByStpId(int stpid)
        {
            List<QcDetails> qcDetailsByStpId =
                ObjRmilcsDbEntities.QcDetails.Where(c => c.StpId == stpid).ToList();
            return qcDetailsByStpId;
        }
        public List<vw_QcExistList> GetQcProbListByBarcode(string barcode)
        {
            List<vw_QcExistList> qcPointsList = ObjRmilcsDbEntities.vw_QcExistList.Where(c => c.BarcodeNo == barcode && c.IsProb == "P").ToList();
            return qcPointsList;
        }
        public List<vw_QcHistoryDetails> GetQcSuccessInfoBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            List<vw_QcHistoryDetails> qcList = ObjRmilcsDbEntities.vw_QcHistoryDetails.Where(c => c.EntryDate >= startDate && c.EntryDate <= endDate && c.IsProb == "N").ToList();
            return qcList;
        }
        public List<vw_QcHistoryDetails> GetQcProblemInfoBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            List<vw_QcHistoryDetails> qcList = ObjRmilcsDbEntities.vw_QcHistoryDetails.Where(c => c.EntryDate >= startDate && c.EntryDate <= endDate && c.IsProb == "N").ToList();
            return qcList;
        }
    }
}