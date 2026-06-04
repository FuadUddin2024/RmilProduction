using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class CompanyDa : BaseDa
    {
        public CompanyDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(Company company)
        {
            try
            {
                ObjRmilcsDbEntities.Company.Add(company);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Company company)
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

        public bool Delete(Company company)
        {
            try
            {
                ObjRmilcsDbEntities.Company.Remove(company);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<Company> GetAllCompany()
        {
            List<Company> companyList = ObjRmilcsDbEntities.Company.ToList();
            return companyList;
        }
        public Company GetCompanyById(int id)
        {
            var q = ObjRmilcsDbEntities.Company.Find(id);
            return q;

        }
    }
}