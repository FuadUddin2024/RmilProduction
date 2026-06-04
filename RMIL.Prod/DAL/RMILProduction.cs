using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class RMILProduction : BaseDa
    {
        public RMILProduction(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public List<usp_GetUnprintedRmPmLast7Days_Result> GetAllUnPrintExcelBarcode()
        {
            // List<GetUnprintedRmilProdMaster_CurrentMonth_Result> ProductionStepList = ObjRmilcsDbEntities.GetUnprintedRmilProdMaster_CurrentMonth().ToList();
            List<usp_GetUnprintedRmPmLast7Days_Result> ProductionStepList = ObjRmilcsDbEntities.usp_GetUnprintedRmPmLast7Days().ToList();
            return ProductionStepList;
        }
        public bool BarcodePrintUpdate(int problemId)
        {
            try
            {
                var entity = new RmilProdMaster
                {
                    RmPmId = problemId,
                    IsPrint = true
                };

                ObjRmilcsDbEntities.RmilProdMaster.Attach(entity);

                // Mark ONLY IsPrint as modified
                ObjRmilcsDbEntities.Entry(entity)
                    .Property(x => x.IsPrint)
                    .IsModified = true;

                ObjRmilcsDbEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // log ex if needed
                return false;
            }
        }

    }
}