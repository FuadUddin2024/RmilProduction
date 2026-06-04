using RMIL.Prod.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMIL.Prod.DAL
{
    public class DealerSetupDa:BaseDa
    {
        public bool AddNewUser(DealerDetails Dealer)
        {
            try
            {
                ObjRmilcsDbEntities.DealerDetails.Add(Dealer);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }
        }
        public DealerDetails GetSingleDealerList(int DealerID)
        {
            DealerDetails departmentList = ObjRmilcsDbEntities.DealerDetails.Find(DealerID);
            return departmentList;
        }
        public List<vw_DealerDepotDetails> GetAllDealer()
        {
            List<vw_DealerDepotDetails> departmentList = ObjRmilcsDbEntities.vw_DealerDepotDetails.ToList();
            return departmentList;
        }
        public bool Update(DealerDetails product)
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
        public DealerDetails GetSingleDealer(int DealerCode)
        {
            var DealerDetails = ObjRmilcsDbEntities.DealerDetails.Where(x => x.DealerCode == DealerCode).FirstOrDefault();
            return DealerDetails;
        }
    }
}