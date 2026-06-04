using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcPointAsnDa : BaseDa
    {
        public QcPointAsnDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcPointAsn qcPointsAssign)
        {
            try
            {
                ObjRmilcsDbEntities.QcPointAsn.Add(qcPointsAssign);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcPointAsn qcPointsAssign)
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

        public bool Delete(QcPointAsn qcPointsAssign)
        {
            try
            {
                ObjRmilcsDbEntities.QcPointAsn.Remove(qcPointsAssign);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcPointAsn> GetAllQcPointAsnTha()
        {
            List<QcPointAsn> qcPointsAssignList = ObjRmilcsDbEntities.QcPointAsn.ToList();
            return qcPointsAssignList;
        }
        public List<vw_StWisePointAsnList> GetQcPointsAssignList()
        {
            List<vw_StWisePointAsnList> qcPointsAssignList = ObjRmilcsDbEntities.vw_StWisePointAsnList.ToList();
            return qcPointsAssignList;
        }
        public QcPointAsn GetQcPointAsnThaById(int id)
        {
            var q = ObjRmilcsDbEntities.QcPointAsn.Find(id);
            return q;

        }
        public QcPointAsn GetGroupWiseQcPointAsnThaByStpsId(int groupId, int stepsId, int pointsId)
        {
            var q = ObjRmilcsDbEntities.QcPointAsn.FirstOrDefault(a => a.CategoryId == groupId && a.StpId == stepsId && a.PtsId == pointsId);
            return q;

        }
        public List<vw_StWisePointAsnList> GetQcPointsListByStId(int stpid)
        {
            List<vw_StWisePointAsnList> qcPointsList = ObjRmilcsDbEntities.vw_StWisePointAsnList.Where(c => c.StpId == stpid).ToList();
            return qcPointsList;
        }
        public List<vw_StWisePointAsnList> GetGroupWiseQcPointsListByStId(int groupId, int stpid)
        {
            List<vw_StWisePointAsnList> qcPointsList = ObjRmilcsDbEntities.vw_StWisePointAsnList.Where(c => c.CategoryId == groupId && c.StpId == stpid).ToList();
            return qcPointsList;
        }
    }
}