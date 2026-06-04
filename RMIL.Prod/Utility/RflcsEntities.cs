using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.Utility
{
    internal class RflcsEntities
    {

        public RflcsEntities()
        {
            // Prevent outside instantiation     
        }

        private static RMILCSDbEntities _singleton;

        public static RMILCSDbEntities GetEntity()
        {
            if (_singleton == null)
            {
                _singleton = new RMILCSDbEntities();
            }
            return _singleton;
        }

        public static RMILCSDbEntities GetFreshEntity()
        {
            _singleton = new RMILCSDbEntities();
            return _singleton;
        }
    }
}