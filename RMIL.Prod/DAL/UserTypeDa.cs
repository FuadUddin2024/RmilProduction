using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class UserTypeDa : BaseDa
    {
        public UserTypeDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(UserType userType)
        {
            try
            {
                ObjRmilcsDbEntities.UserType.Add(userType);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(UserType userType)
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

        public bool Delete(UserType userType)
        {
            try
            {
                ObjRmilcsDbEntities.UserType.Remove(userType);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<UserType> GetAllUserType()
        {
            List<UserType> userTypeList =
                ObjRmilcsDbEntities.UserType.Where(c => c.ServiceType == "A" || c.ServiceType == "P").ToList();
            return userTypeList;
        }
        public UserType GetUserTypeById(int id)
        {
            var q = ObjRmilcsDbEntities.UserType.Find(id);
            return q;

        }
    }
}