using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcStepDa : BaseDa
    {
        public QcStepDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcStep qcStep)
        {
            try
            {
                ObjRmilcsDbEntities.QcStep.Add(qcStep);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcStep qcStep)
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

        public bool Delete(QcStep qcStep)
        {
            try
            {
                ObjRmilcsDbEntities.QcStep.Remove(qcStep);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcStep> GetAllQcStep()
        {
            List<QcStep> qcStepList = ObjRmilcsDbEntities.QcStep.ToList();
            return qcStepList;
        }
        public List<QcStep> GetGroupWiseQcStep(int categoryId)
        {
            List<QcStep> qcStepList = ObjRmilcsDbEntities.QcStep.Where(c => c.CategoryId == categoryId).ToList();
            return qcStepList;
        }
        public List<vw_QcStepsList> GetQcStepList()
        {
            List<vw_QcStepsList> qcStepList = ObjRmilcsDbEntities.vw_QcStepsList.ToList();
            return qcStepList;
        }
        public QcStep GetQcStepById(int id)
        {
            var q = ObjRmilcsDbEntities.QcStep.Find(id);
            return q;

        }
        public QcStep GetQcStepByName(string name)
        {
            var q = ObjRmilcsDbEntities.QcStep.FirstOrDefault(c => c.StepsName == name);
            return q;

        }
        public QcStep GetProdCategoryWiseQcStep(int categoryId, string name)
        {
            var q = ObjRmilcsDbEntities.QcStep.FirstOrDefault(c => c.CategoryId == categoryId && c.StepsName == name);
            return q;

        }
        public QcStep GetInitialQcStep()
        {
            var q = ObjRmilcsDbEntities.QcStep.FirstOrDefault(c => c.Active == true && c.InitialStep == true);
            return q;

        }
        public QcStep GetCategoryWiseInitialQcStep(int categoryId)
        {
            var q = ObjRmilcsDbEntities.QcStep.FirstOrDefault(c => c.CategoryId == categoryId && c.Active == true && c.InitialStep == true);
            return q;

        }
        public List<QcStep> GetCategoryWiseQcStepList(int categoryId)
        {
            List<QcStep> q = ObjRmilcsDbEntities.QcStep.Where(c => c.CategoryId == categoryId && c.Active == true).ToList();
            return q;

        }
        public List<vw_QcPerList> GetUserWiseStepPerList(string userId)
        {
            List<vw_QcPerList> qcUserWiseStepPerList =
                ObjRmilcsDbEntities.vw_QcPerList.Where(c => c.UserId == userId && c.Active == true && c.StpActive == true).ToList();
            return qcUserWiseStepPerList;
        }
    }
}