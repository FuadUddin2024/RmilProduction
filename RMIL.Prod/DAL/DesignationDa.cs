using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class DesignationDa : BaseDa
    {
        public DesignationDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Designation designation)
        {
            try
            {
                ObjRmilcsDbEntities.Designation.Add(designation);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Designation designation)
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

        public bool Delete(Designation designation)
        {
            try
            {
                ObjRmilcsDbEntities.Designation.Remove(designation);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Designation> GetAllDesignation()
        {
            List<Designation> designationList = ObjRmilcsDbEntities.Designation.ToList();
            return designationList;
        }
        public Designation GetDesignationtById(int id)
        {
            var q = ObjRmilcsDbEntities.Designation.Find(id);
            return q;

        }
    }
}