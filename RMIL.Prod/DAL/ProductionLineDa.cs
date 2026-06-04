using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class ProductionLineDa : BaseDa
    {
        public ProductionLineDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }
        public bool Insert(ProductionLine productionLine)
        {
            try
            {
                ObjRmilcsDbEntities.ProductionLine.Add(productionLine);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(ProductionLine productionLine)
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

        public bool Delete(ProductionLine productionLine)
        {
            try
            {
                ObjRmilcsDbEntities.ProductionLine.Remove(productionLine);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public List<ProductionLine> GetAllProductionLine()
        {
            List<ProductionLine> productionLineList = ObjRmilcsDbEntities.ProductionLine.ToList();
            return productionLineList;
        }
        public List<ProductionLine> GetLineListbyId(int lineid)
        {
            List<ProductionLine> lList = ObjRmilcsDbEntities.ProductionLine.Where(c => c.LineId == lineid).ToList();
            return lList;
        }
        public ProductionLine GetProductionLineById(int id)
        {
            var q = ObjRmilcsDbEntities.ProductionLine.Find(id);
            return q;

        }
    }
}