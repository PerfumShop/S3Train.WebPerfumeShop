using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Domain
{
    public class Banner: EntityBase
    {
        public string Image { get; set; }
        public string Link { get; set; }
    }
}
