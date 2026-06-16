using RMIL.Prod.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMIL.Prod.Dashboard.Model
{
    public class NightShiftViewModel
    {
        public List<sp_GetProductionByShiftNight_Result> ShiftData { get; set; }
        public int? TotalProduction { get; set; }
    }
    public class DayShiftViewModel
    {
        public List<sp_GetProductionByShiftDay_Result> ShiftData { get; set; }
        public int? TotalProduction { get; set; }
    }
    public class NightShiftViewModelDistribution
    {
        public List<sp_GetDistributionProductionNightShift_Result> ShiftData { get; set; }
        public int? TotalProduction { get; set; }
    }
    public class DayShiftViewModelDistribution
    {
        public List<sp_GetDistributionProductionDayShift_Result> ShiftData { get; set; }
        public int? TotalProduction { get; set; }
    }
}