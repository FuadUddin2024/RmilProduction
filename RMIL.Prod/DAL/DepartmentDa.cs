using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class DepartmentDa : BaseDa
    {
        public DepartmentDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Department department)
        {
            try
            {
                ObjRmilcsDbEntities.Department.Add(department);
                ObjRmilcsDbEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Department department)
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

        public bool Delete(Department department)
        {
            try
            {
                ObjRmilcsDbEntities.Department.Remove(department);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Department> GetAllDepartment()
        {
            List<Department> departmentList = ObjRmilcsDbEntities.Department.ToList();
            return departmentList;
        }
        public Department GetDepartmentById(int id)
        {
            var q = ObjRmilcsDbEntities.Department.Find(id);
            return q;

        }
    }
}