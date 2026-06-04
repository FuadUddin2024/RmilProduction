using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class UserInfoDa : BaseDa
    {
        public UserInfoDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }

        public bool Insert(UserInfo objUser)
        {
            try
            {
                ObjRmilcsDbEntities.UserInfo.Add(objUser);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(UserInfo obj)
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

        public bool Delete(UserInfo objUserInfo)
        {
            try
            {
                ObjRmilcsDbEntities.UserInfo.Remove(objUserInfo);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<UserInfo> GetAllUserInfo()
        {
            List<UserInfo> listUsersInfo = ObjRmilcsDbEntities.UserInfo.Where(a => a.IsActive == true && a.ServiceUser == "A" || a.IsActive == true && a.ServiceUser == "P").ToList();
            return listUsersInfo;
        }
        //public List<vw_UserList> GetUserList()
        //{
        //    List<vw_UserList> userList = ObjRmilcsDbEntities.vw_UserList.ToList();
        //    return userList;
        //}
        public UserInfo GetUserInfoByCode(int code)
        {
            var q = ObjRmilcsDbEntities.UserInfo.Find(code);
            return q;

        }
        public UserInfo GetUserByUserId(string userId)
        {
            var q = ObjRmilcsDbEntities.UserInfo.FirstOrDefault(a => a.UserId == userId);
            return q;

        }
    }
}