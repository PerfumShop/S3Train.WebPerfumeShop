using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Order:EntityBase
    {
        [ForeignKey("ShoppingCart")]
        public Guid ShoppingCart_Id { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryPhone { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
