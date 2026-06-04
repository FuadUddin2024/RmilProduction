using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;
using System.Data;

namespace RMIL.Prod.DAL
{
    public class QcMasterDa : BaseDa
    {
        public DateTime Before30Days = DateTime.Now.Date.AddDays(-30);
        public DateTime Before7Days = DateTime.Now.Date.AddDays(-7);
        public QcMasterDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcMaster qcMaster)
        {
            try
            {
                ObjRmilcsDbEntities.QcMaster.Add(qcMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public bool InsertData(QcMaster qcMaster)
        {
            try
            {
                ObjRmilcsDbEntities.QcMaster.Add(qcMaster);
                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public bool Update(QcMaster qcMaster)
        {
            try
            {
                var local = ObjRmilcsDbEntities.QcMaster
                                .Local
                                .FirstOrDefault(x => x.MId == qcMaster.MId);

                if (local != null)
                {
                    ObjRmilcsDbEntities.Entry(local).State = EntityState.Detached;
                }

                // Attach and mark as modified
                ObjRmilcsDbEntities.QcMaster.Attach(qcMaster);
                ObjRmilcsDbEntities.Entry(qcMaster).State = EntityState.Modified;
                ObjRmilcsDbEntities.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                // Optional: log ex.Message
                return false;
            }
        }
        //public bool Update(QcMaster qcMaster)
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
        public void UpdateData()
        {
            ObjRmilcsDbEntities.SaveChanges();
        }
        public bool Delete(QcMaster qcMaster)
        {
            try
            {
                ObjRmilcsDbEntities.QcMaster.Remove(qcMaster);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcMaster> GetAllQcMaster()
        {
            List<QcMaster> qcTestMasterList = ObjRmilcsDbEntities.QcMaster.ToList();
            return qcTestMasterList;
        }
        public QcMaster GetQcMasterById(int id)
        {
            var q = ObjRmilcsDbEntities.QcMaster.Find(id);
            return q;

        }
        public QcMaster GetQcStepByBarcode(string barcode)
        {
            QcMaster q = ObjRmilcsDbEntities.QcMaster.FirstOrDefault(a => a.BarcodeNo == barcode);
            return q;
        }
        public QcMaster GetProbQcStepByBarcode(string barcode)
        {
            QcMaster q = ObjRmilcsDbEntities.QcMaster.FirstOrDefault(a => a.BarcodeNo == barcode && a.IsProb == "P");
            return q;
        }
        public List<vw_QcExistList> GetQcProbListByBarcode(string barcode)
        {
            List<vw_QcExistList> qcPointsList = ObjRmilcsDbEntities.vw_QcExistList.Where(c => c.BarcodeNo == barcode && c.IsProb == "P").ToList();
            return qcPointsList;
        }
        public List<vw_QcExistList> GetQcPointsListByBarcodeStId(string barcode, int stepId)
        {
            List<vw_QcExistList> qcPointsList = ObjRmilcsDbEntities.vw_QcExistList.Where(c => c.BarcodeNo == barcode && c.StpId == stepId).ToList();
            return qcPointsList;
        }
        public QcMaster GetLastRecord()
        {
            var q = ObjRmilcsDbEntities.QcMaster.OrderByDescending(c => c.EntryDate).FirstOrDefault();
            return q;

        }
        public List<QcMaster> GetQcStepListByBarcode(string barcode)
        {
            List<QcMaster> qcStepListByBarcode =
                ObjRmilcsDbEntities.QcMaster.Where(c => c.BarcodeNo == barcode).ToList();
            return qcStepListByBarcode;
        }
         #region monthly QC status
        public List<QcMaster> GetMonthlySuccessQcList()
        {
            List<QcMaster> successList = ObjRmilcsDbEntities.QcMaster.Where(a => a.EntryDate >= Before30Days && a.IsProb == "N" && a.Active == true).ToList();
            return successList;
        }
        public List<QcMaster> GetMonthlyProblemQcList()
        {
            List<QcMaster> problemList = ObjRmilcsDbEntities.QcMaster.Where(a => a.EntryDate >= Before30Days && a.IsProb == "P" && a.Active == false).ToList();
            return problemList;
        }
        public List<QcMaster> GetMonthlySuccessOnProbQcList()
        {
            List<QcMaster> problemList = ObjRmilcsDbEntities.QcMaster.Where(a => a.EntryDate >= Before30Days && a.IsProb == "S" && a.Active == true).ToList();
            return problemList;
        }
        #endregion
        #region daily QC status
        public List<QcMaster> GetDailySuccessQcList()
        {
            List<QcMaster> successList = ObjRmilcsDbEntities.QcMaster.Where(a => a.IsProb == "N" && a.Active == true).ToList();
            return successList;
        }
        public List<QcMaster> GetDailyProblemQcList()
        {
            List<QcMaster> problemList = ObjRmilcsDbEntities.QcMaster.Where(a => a.IsProb == "P" && a.Active == false).ToList();
            return problemList;
        }
        public List<QcMaster> GetDailySuccessOnProbQcList()
        {
            List<QcMaster> problemList = ObjRmilcsDbEntities.QcMaster.Where(a => a.IsProb == "S" && a.Active == true).ToList();
            return problemList;
        }
        public List<QcMaster> GetLast7DaysQcSuccessInfo()
        {
            List<QcMaster> qcTestMasterList = ObjRmilcsDbEntities.QcMaster.OrderByDescending(c => c.MId).Where(c => c.EntryDate >= Before7Days && c.IsProb == "N" && c.Active == true).ToList();
            return qcTestMasterList;
        }
        #endregion
        #region daily Production status
        public List<QcMaster> GetSuccessProdList()
        {
            List<QcMaster> prodList = ObjRmilcsDbEntities.QcMaster.Where(a => a.IsProb == "N" && a.Active == true || a.IsProb == "S" && a.Active == true).ToList();
            return prodList;
        }
        #endregion
        public List<vw_QcHistDetails> GetQcInfoBetweenTwoDate(DateTime startDate, DateTime endDate, string status)
        {
            List<vw_QcHistDetails> qcList = ObjRmilcsDbEntities.vw_QcHistDetails.Where(c => c.EntryDate >= startDate && c.EntryDate <= endDate && c.IsProb == status && c.Active == true).ToList();
            return qcList;
        }
        public List<vw_QcHistDetails> GetQcInfoBetweenTwoDateWithProb(DateTime startDate, DateTime endDate, string status)
        {
            List<vw_QcHistDetails> qcList = ObjRmilcsDbEntities.vw_QcHistDetails.Where(c => c.EntryDate >= startDate && c.EntryDate <= endDate && c.IsProb == status && c.Active == false).ToList();
            return qcList;
        }
        public List<vw_QcHistDetails> GetAllQcInfoBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            List<vw_QcHistDetails> qcList = ObjRmilcsDbEntities.vw_QcHistDetails.Where(c => c.EntryDate >= startDate && c.EntryDate <= endDate).ToList();
            return qcList;
        }
        public QcMaster GetQcFinishByBarcode(string barcode)
        {
            QcMaster q = ObjRmilcsDbEntities.QcMaster.FirstOrDefault(a => a.BarcodeNo == barcode && a.Active == true);
            return q;
        }
        public List<QcMaster> GetDailySuccessProdList()
        {
            List<QcMaster> successList = ObjRmilcsDbEntities.QcMaster.Where(a => a.Active == true).ToList();
            return successList;
        }

    }
}