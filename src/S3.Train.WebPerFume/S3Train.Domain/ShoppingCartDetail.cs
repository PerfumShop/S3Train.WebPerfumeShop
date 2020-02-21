using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class ShoppingCartDetail:EntityBase
    {
        [ForeignKey("ShoppingCart")]
        public Guid ShoppingCart_Id { get; set; }
        public int Quantity { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual ProductVariation ProductVariations { get; set; }
    }
}
