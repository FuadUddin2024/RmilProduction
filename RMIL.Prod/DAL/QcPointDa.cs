using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class QcPointDa : BaseDa
    {
        public QcPointDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(QcPoint qcPoint)
        {
            try
            {
                ObjRmilcsDbEntities.QcPoint.Add(qcPoint);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(QcPoint qcPoint)
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

        public bool Delete(QcPoint qcPoint)
        {
            try
            {
                ObjRmilcsDbEntities.QcPoint.Remove(qcPoint);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<QcPoint> GetAllQcCheckingPoints()
        {
            List<QcPoint> qcCheckingPointsList = ObjRmilcsDbEntities.QcPoint.ToList();
            return qcCheckingPointsList;
        }
        public QcPoint GetQcCheckingPointsById(int id)
        {
            var q = ObjRmilcsDbEntities.QcPoint.Find(id);
            return q;

        }
        public QcPoint GetQcCheckingPointsByName(string name)
        {
            var q = ObjRmilcsDbEntities.QcPoint.FirstOrDefault(c => c.PointsName == name);
            return q;

        }
    }
}