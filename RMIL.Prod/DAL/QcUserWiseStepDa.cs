using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcUserWiseStepDa : BaseDa
    {
        public QcUserWiseStepDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcUserWiseStep qcUserWiseStepPer)
        {
            try
            {
                ObjRmilcsDbEntities.QcUserWiseStep.Add(qcUserWiseStepPer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcUserWiseStep qcUserWiseStepPer)
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

        public bool Delete(QcUserWiseStep qcUserWiseStepPer)
        {
            try
            {
                ObjRmilcsDbEntities.QcUserWiseStep.Remove(qcUserWiseStepPer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcUserWiseStep> GetAllQcUserWiseStep()
        {
            List<QcUserWiseStep> qcUserWiseStepPerList = ObjRmilcsDbEntities.QcUserWiseStep.ToList();
            return qcUserWiseStepPerList;
        }
        public QcUserWiseStep GetQcUserWiseStepById(int id)
        {
            var q = ObjRmilcsDbEntities.QcUserWiseStep.Find(id);
            return q;

        }
        public QcUserWiseStep GetUserWiseStepPerInfoById(string userId, int stpId)
        {
            var q = ObjRmilcsDbEntities.QcUserWiseStep.FirstOrDefault(c => c.UserId == userId && c.StpId == stpId);
            return q;

        }
        public List<QcUserWiseStep> GetUserWiseStepPerListByUserId(string userId)
        {
            List<QcUserWiseStep> qcUserWiseStepPerList =
                ObjRmilcsDbEntities.QcUserWiseStep.Where(c => c.UserId == userId && c.Active == true).ToList();
            return qcUserWiseStepPerList;
        }
    }
}