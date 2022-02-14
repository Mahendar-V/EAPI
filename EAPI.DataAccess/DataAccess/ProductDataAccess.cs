using EAPI.DataAccess.DataAccess.Interfaces;
using EAPI.DataAccess.Entities;
using EAPI.DataModels;
using EAPI.DataModels.Exceptions;
using EAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess.DataAccess
{
    public class ProductDataAccess : IProductsDataAccess
    {
        public readonly EAPIContext _dbContext;
        public ProductDataAccess(EAPIContext context)
        {
            _dbContext = context;
        }

        public ProductItem AddProduct(ProductItem product)
        {
            if (_dbContext.Products.Any(s => s.Name == product.Name))
            {
                throw new DuplicateRecordError($"{product.Name} already exists. duplicate name is not allowed.");
            }

            _dbContext.Products.Add(new Product() 
            {
              Name=product.Name,
              Price=product.Price,
              Description=product.Description,
            });
            _dbContext.SaveChanges();

           product.PId= _dbContext.Products.FirstOrDefault(s => s.Name == product.Name).PId;
           return product;
        }

        public bool DeleteProduct(int productId)
        {
            var d = _dbContext.Products.FirstOrDefault(s => s.PId == productId);
            if (d is null)
                throw new NotFoundError($"product is not found to delete");

            _dbContext.Products.Remove(d);
            _dbContext.SaveChanges();
            return true;
        }

        public List<ProductItem> GetProducts()
        {
           return _dbContext.Products.Select(s => new ProductItem 
            {
                Name= s.Name,Description= s.Description,
               Price= s.Price, PId = s.PId,
            }).ToList();
        }
    }
}
