using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
   public class ProductImage:EntityBase
    {
        [ForeignKey("ProductVariation")]
        public Guid ProductVariation_Id { get; set; }
        public string ImagePath { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
