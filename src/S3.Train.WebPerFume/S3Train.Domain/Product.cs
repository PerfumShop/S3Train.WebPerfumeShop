using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3Train.Domain
{
    public class Product : EntityBase
    {
        [ForeignKey("Vendor")]
        public Guid Vendor_Id { get; set; }
        [ForeignKey("Brand")]
        public Guid  Brand_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
    }
}