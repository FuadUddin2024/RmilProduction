using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class MonthCalcDa : BaseDa
    {
        public MonthCalcDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(MonthCalc monthCalc)
        {
            try
            {
                ObjRmilcsDbEntities.MonthCalc.Add(monthCalc);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(MonthCalc monthCalc)
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

        public bool Delete(MonthCalc monthCalc)
        {
            try
            {
                ObjRmilcsDbEntities.MonthCalc.Remove(monthCalc);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<MonthCalc> GetAllMonthCalc()
        {
            List<MonthCalc> monthCalcList = ObjRmilcsDbEntities.MonthCalc.ToList();
            return monthCalcList;
        }
        public MonthCalc GetMonthCalcById(int id)
        {
            var q = ObjRmilcsDbEntities.MonthCalc.Find(id);
            return q;

        }
    }
}