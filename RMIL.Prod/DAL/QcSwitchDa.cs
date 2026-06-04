using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcSwitchDa : BaseDa
    {
        public QcSwitchDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcSwitch qcTestSwitch)
        {
            try
            {
                ObjRmilcsDbEntities.QcSwitch.Add(qcTestSwitch);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcSwitch qcTestSwitch)
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

        public bool Delete(QcSwitch qcTestSwitch)
        {
            try
            {
                ObjRmilcsDbEntities.QcSwitch.Remove(qcTestSwitch);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcSwitch> GetAllQcSwitch()
        {
            List<QcSwitch> qcTestSwitchList = ObjRmilcsDbEntities.QcSwitch.ToList();
            return qcTestSwitchList;
        }
        public List<vw_QcSwitchList> GetQcSwitchList()
        {
            List<vw_QcSwitchList> qcSwitchList = ObjRmilcsDbEntities.vw_QcSwitchList.ToList();
            return qcSwitchList;
        }
        public QcSwitch GetQcSwitchById(int id)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.Find(id);
            return q;

        }
        public QcSwitch GetQcSwitchBySteps(int stepId)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.FirstOrDefault(a => a.FromStId == stepId);
            return q;

        }
        public QcSwitch GetGroupWiseQcSwitchBySteps(int groupId, int stepId)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.FirstOrDefault(a => a.CategoryId == groupId && a.FromStId == stepId);
            return q;

        }
        public QcSwitch GetQcSwitchByToSteps(int tostepId)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.FirstOrDefault(a => a.ToStId == tostepId);
            return q;

        }
        public QcSwitch GetQcSwitchByStpsId(int fromStId, int toStId)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.FirstOrDefault(a => a.FromStId == fromStId && a.ToStId == toStId);
            return q;

        }
        public QcSwitch GetGroupWiseQcSwitchByStpsId(int groupId, int fromStId, int toStId)
        {
            var q = ObjRmilcsDbEntities.QcSwitch.FirstOrDefault(a => a.CategoryId == groupId && a.FromStId == fromStId && a.ToStId == toStId);
            return q;

        }
    }
}