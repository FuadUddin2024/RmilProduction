using System;
using System.Collections.Generic;
using System.Linq;
using RMIL.Prod.EntityFramework;

namespace RMIL.Prod.DAL
{
    public class ProductDa : BaseDa
    {
        public ProductDa(bool isNewNewContext = false)
            : base(isNewNewContext)
        {

        }

        public bool Insert(Product product)
        {
            try
            {
                ObjRmilcsDbEntities.Product.Add(product);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }

        public bool Update(Product product)
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

        public bool Delete(Product product)
        {
            try
            {
                ObjRmilcsDbEntities.Product.Remove(product);
                ObjRmilcsDbEntities.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return Convert.ToBoolean("Error:" + ex);
            }

        }
        public List<Product> GetAllProduct()
        {
            List<Product> productList = ObjRmilcsDbEntities.Product.ToList();
            return productList;
        }
        public List<Product> GetProductListById(int productId)
        {
            List<Product> productList = ObjRmilcsDbEntities.Product.Where(c => c.ProductId == productId).ToList();
            return productList;
        }
        public List<vw_ProductList> GetProductList()
        {
            List<vw_ProductList> productDetails = ObjRmilcsDbEntities.vw_ProductList.ToList();
            return productDetails;
        }
        public Product GetProductById(int id)
        {
            var q = ObjRmilcsDbEntities.Product.Find(id);
            return q;
        }
    }
}