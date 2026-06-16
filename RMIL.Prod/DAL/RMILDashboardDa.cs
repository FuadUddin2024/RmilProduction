using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RMILDashboardDa : BaseDa
    {
        public RMILDashboardDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        //public long LastTransactionId()
        //{
        //    long lastTransactionId =
        //        ObjRmilcsDbEntities.RmilBoxMaster.OrderByDescending(c => c.RmPmId).Select(c => c.RmPmId).FirstOrDefault();
        //    return lastTransactionId;
        //}
        public List<sp_GetProductionByShiftDay_Result> GetDayShiftData()
        {
            List<sp_GetProductionByShiftDay_Result> lastTransactionId =
                ObjRmilcsDbEntities.sp_GetProductionByShiftDay().ToList();
            return lastTransactionId;
        }
        public List<sp_GetProductionByShiftNight_Result> GetNightShiftData()
        {
            List<sp_GetProductionByShiftNight_Result> lastTransactionId =
                ObjRmilcsDbEntities.sp_GetProductionByShiftNight().ToList();
            return lastTransactionId;
        }

        // Distribution 
        public List<sp_GetDistributionProductionDayShift_Result> GetDayShiftDataDistribution()
        {
            List<sp_GetDistributionProductionDayShift_Result> lastTransactionId =
                ObjRmilcsDbEntities.sp_GetDistributionProductionDayShift().ToList();
            return lastTransactionId;
        }
        public List<sp_GetDistributionProductionNightShift_Result> GetNightShiftDataDistribution()
        {
            List<sp_GetDistributionProductionNightShift_Result> lastTransactionId =
                ObjRmilcsDbEntities.sp_GetDistributionProductionNightShift().ToList();
            return lastTransactionId;
        }
    }
}