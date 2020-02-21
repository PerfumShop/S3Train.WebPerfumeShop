using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
