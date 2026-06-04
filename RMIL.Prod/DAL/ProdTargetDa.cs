using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class ProdTargetDa : BaseDa
    {
        public DateTime Before30Days = DateTime.Now.Date.AddDays(-30);
        public DateTime Before7Days = DateTime.Now.Date.AddDays(-7);
        public ProdTargetDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(ProdTarget prodTarget)
        {
            try
            {
                ObjRmilcsDbEntities.ProdTarget.Add(prodTarget);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(ProdTarget prodTarget)
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

        public bool Delete(ProdTarget prodTarget)
        {
            try
            {
                ObjRmilcsDbEntities.ProdTarget.Remove(prodTarget);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<ProdTarget> GetAllProdTarget()
        {
            List<ProdTarget> prodTargetList = ObjRmilcsDbEntities.ProdTarget.ToList();
            return prodTargetList;
        }
        public List<vw_LineWDailyTargetList> GetProdTargetList()
        {
            List<vw_LineWDailyTargetList> thaProdTargetList = ObjRmilcsDbEntities.vw_LineWDailyTargetList.OrderByDescending(c => c.TptId).ToList();
            return thaProdTargetList;
        }
        public ProdTarget GetProdTargetById(int id)
        {
            var q = ObjRmilcsDbEntities.ProdTarget.Find(id);
            return q;

        }
        public ProdTarget GetDailyTargetByIds(int lineId, int modelId, DateTime targetDate)
        {
            var q = ObjRmilcsDbEntities.ProdTarget.FirstOrDefault(a => a.LineId == lineId && a.PrModelId == modelId && a.TargetDate == targetDate);
            return q;

        }
        public List<ProdTarget> GetDailyProdTargetList()
        {
            List<ProdTarget> prodList = ObjRmilcsDbEntities.ProdTarget.Where(a => a.TargetDate == DateTime.Today).ToList();
            return prodList;
        }
        public List<ProdTarget> GetWeeklyProdTargetList()
        {
            List<ProdTarget> prodList = ObjRmilcsDbEntities.ProdTarget.Where(a => a.TargetDate >= Before7Days).ToList();
            return prodList;
        }
        public List<ProdTarget> GetMonthlyProdTargetList()
        {
            List<ProdTarget> prodList = ObjRmilcsDbEntities.ProdTarget.Where(a => a.TargetDate >= Before30Days).ToList();
            return prodList;
        }
        public List<ProdTarget> GetLineWiseDailyProdTargetList(int lineId)
        {
            List<ProdTarget> ptList = ObjRmilcsDbEntities.ProdTarget.Where(a => a.LineId == lineId && a.TargetDate == DateTime.Today).ToList();
            return ptList;
        }
        public List<sp_RmilProd_Result> GetRmilDataByDate(DateTime tdate)
        {
            var q = ObjRmilcsDbEntities.sp_RmilProd(tdate).ToList();
            return q;

        }
    }
}