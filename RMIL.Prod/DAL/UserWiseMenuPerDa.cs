using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;
using System.Data.SqlClient;

namespace RMIL.Prod.DAL
{
    public class UserWiseMenuPerDa : BaseDa
    {
        public UserWiseMenuPerDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(UserWiseMenuPer userWiseMenuPer)
        {
            try
            {
                ObjRmilcsDbEntities.UserWiseMenuPer.Add(userWiseMenuPer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(UserWiseMenuPer userWiseMenuPer)
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

        public bool Delete(UserWiseMenuPer userWiseMenuPer)
        {
            try
            {
                ObjRmilcsDbEntities.UserWiseMenuPer.Remove(userWiseMenuPer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<UserWiseMenuPer> GetAllUserWiseMenuPer()
        {
            List<UserWiseMenuPer> qList = ObjRmilcsDbEntities.UserWiseMenuPer.ToList();
            return qList;
        }
        public UserWiseMenuPer GetUserWiseMenuPerById(int id)
        {
            var q = ObjRmilcsDbEntities.UserWiseMenuPer.Find(id);
            return q;

        }
        public UserWiseMenuPer GetUserWiseMenuPerInfoById(string userId, int menuId)
        {
            var q = ObjRmilcsDbEntities.UserWiseMenuPer.FirstOrDefault(c => c.UserId == userId && c.MenuId == menuId);
            return q;

        }
        public UserWiseMenuPer GetUserWiseMenuPerByUserId(string userId)
        {
            var q = ObjRmilcsDbEntities.UserWiseMenuPer.FirstOrDefault(c => c.UserId == userId && c.Active == true);
            return q;

        }

        public List<UserWiseMenuPer> GetUserWiseMenuPerListByUserId(string userId)
        {
            List<UserWiseMenuPer> qList =
                ObjRmilcsDbEntities.UserWiseMenuPer.Where(c => c.UserId == userId && c.Active == true).ToList();
            return qList;
        }
        public List<UserWiseMenuPer> GetNonActiveUserWiseMenuPerListByUserId(string userId)
        {
            List<UserWiseMenuPer> qList =
                ObjRmilcsDbEntities.UserWiseMenuPer.Where(c => c.UserId == userId && c.Active == false).ToList();
            return qList;
        }
        public List<UserWiseMenuPer> GetNotExistUserByUserId(string userId)
        {
            List<UserWiseMenuPer> qList =
                ObjRmilcsDbEntities.UserWiseMenuPer.Where(c => c.UserId == userId).ToList();
            return qList;
        }

        // Newly Added Code 4.5.26
        public bool DeletePreviousPermission(string userId)
        {
            bool IsDeleted = true;
            try
            {
                var fromDateParam = new SqlParameter("@UserId", userId);
                var rowsAffected = ObjRmilcsDbEntities.Database.ExecuteSqlCommand(
                     "EXEC dbo.sp_DeleteUserWiseMenuPer @UserId",
                     fromDateParam
                 );
            }
            catch(Exception ex)
            {
                IsDeleted = false;
            }
            return IsDeleted;
        }
    }
}