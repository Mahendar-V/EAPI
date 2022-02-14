using EAPI.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess.DataAccess.Interfaces
{
   public interface IProductsDataAccess
    {
        ProductItem AddProduct(ProductItem product);
        List<ProductItem> GetProducts();
        bool DeleteProduct(int productId);
    }
}
