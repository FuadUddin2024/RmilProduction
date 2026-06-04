using RMIL.Prod.EntityFramework;
using RMIL.Prod.Utility;

namespace RMIL.Prod.DAL
{
    public class BaseDa
    {
        protected RMILCSDbEntities ObjRmilcsDbEntities = null;
        /// <summary>
        /// isNewNewContext == true, if you need newcontext
        /// isLazyLoadingEnable = true, is you want to load all data e.g. parent + child
        /// </summary>
        /// <param name="isNewNewContext"></param>

        public BaseDa(bool isNewNewContext = false)
        {
            ObjRmilcsDbEntities = (isNewNewContext == false) ? RflcsEntities.GetEntity() : RflcsEntities.GetFreshEntity();

        }
    }
}