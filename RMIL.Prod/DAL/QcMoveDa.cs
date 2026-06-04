using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcMoveDa : BaseDa
    {
        public QcMoveDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcMove qcMove)
        {
            try
            {
                ObjRmilcsDbEntities.QcMove.Add(qcMove);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcMove qcMove)
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

        public bool Delete(QcMove qcMove)
        {
            try
            {
                ObjRmilcsDbEntities.QcMove.Remove(qcMove);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcMove> GetAllQcMove()
        {
            List<QcMove> qcMoveList = ObjRmilcsDbEntities.QcMove.ToList();
            return qcMoveList;
        }
        public List<vw_QcMoveList> GetQcMoveList()
        {
            List<vw_QcMoveList> qcMoveList = ObjRmilcsDbEntities.vw_QcMoveList.ToList();
            return qcMoveList;
        }
        public QcMove GetQcMoveById(int id)
        {
            var q = ObjRmilcsDbEntities.QcMove.Find(id);
            return q;

        }
        public QcMove GetQcMoveBySteps(int stepId)
        {
            var q = ObjRmilcsDbEntities.QcMove.FirstOrDefault(a => a.FromStId == stepId);
            return q;

        }
        public QcMove GetGroupWiseQcMoveBySteps(int groupId, int stepId)
        {
            var q = ObjRmilcsDbEntities.QcMove.FirstOrDefault(a => a.CategoryId == groupId && a.FromStId == stepId);
            return q;

        }
        public QcMove GetQcMoveByStpsId(int fromStId, int toStId)
        {
            var q = ObjRmilcsDbEntities.QcMove.FirstOrDefault(a => a.FromStId == fromStId && a.ToStId == toStId);
            return q;

        }
        public QcMove GetGroupWiseQcMoveByStpsId(int groupId, int fromStId, int toStId)
        {
            var q = ObjRmilcsDbEntities.QcMove.FirstOrDefault(a => a.CategoryId == groupId && a.FromStId == fromStId && a.ToStId == toStId);
            return q;

        }
    }
}