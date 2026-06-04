using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class ProductModelDa : BaseDa
    {
        public ProductModelDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }

        public bool Insert(ProductModel productModel)
        {
            try
            {
                ObjRmilcsDbEntities.ProductModel.Add(productModel);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(ProductModel productModel)
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

        public bool Delete(ProductModel productModel)
        {
            try
            {
                ObjRmilcsDbEntities.ProductModel.Remove(productModel);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public List<ProductModel> GetAllProductModel()
        {
            List<ProductModel> productModelList = ObjRmilcsDbEntities.ProductModel.ToList();
            return productModelList;
        }
        public List<ProductModel> GetProductModelListById(int productModelId)
        {
            List<ProductModel> modelList = ObjRmilcsDbEntities.ProductModel.Where(c => c.PrModelId == productModelId).ToList();
            return modelList;
        }
        public List<vw_ProductModelList> GetProductModelList()
        {
            List<vw_ProductModelList> modelDetails = ObjRmilcsDbEntities.vw_ProductModelList.ToList();
            return modelDetails;
        }
        public List<vw_ProductModelList> GetProductModelByModelCode(int modelCode)
        {
            List<vw_ProductModelList> modelDetails = ObjRmilcsDbEntities.vw_ProductModelList.Where(c => c.ModelCode == modelCode).ToList();
            return modelDetails;
        }
        public ProductModel GetProductModelById(int id)
        {
            var q = ObjRmilcsDbEntities.ProductModel.Find(id);
            return q;
        }
        public List<ProductModel> GetProductModelByProductId(int productId)
        {
            var q = ObjRmilcsDbEntities.ProductModel.Where(a => a.ProductId == productId).ToList();
            return q;
        }
        public ProductModel GetModelByModelCode(int code)
        {
            var q = ObjRmilcsDbEntities.ProductModel.FirstOrDefault(a => a.ModelCode == code);
            return q;
        }
        //public List<string> GetProductModelListByProductId(int productid)
        //{
        //    var q =
        //        ObjCsManageDbEntities.ProductModel.Where(p => p.ProductId == productid)
        //            .Select(p => p.PrModelName)
        //            .ToList();
        //    return q;
        //}
        //public int LastModelCode()
        //{
        //    int lastModelCode =
        //        (int)ObjCsManageDbEntities.ProductModel.OrderByDescending(c => c.ModelCode).Select(c => c.ModelCode).FirstOrDefault();
        //    return lastModelCode;
        //}
    }
}