using System;
using System.Collections.Generic;
using System.Linq;
using S3Train.Contract;
using S3Train.Domain;

namespace S3Train.Service
{
    public class ProductService : GenenicServiceBase<Product>, IProductService
    {
        public ProductService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IList<Product> GetProductsByBrandId(Guid brand_Id)
        {
            return this.EntityDbSet.Where(x => x.Brand_Id == brand_Id && x.IsActive == true).ToList();
        }
    }
}