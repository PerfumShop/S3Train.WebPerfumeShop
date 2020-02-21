using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Service
{
    public class ProductVariationService : GenenicServiceBase<ProductVariation>, IProductVariationService
    {
        public ProductVariationService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
