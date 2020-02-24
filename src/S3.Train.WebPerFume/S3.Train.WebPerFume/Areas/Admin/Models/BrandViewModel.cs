using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class BrandViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Logo { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }

        public IList<ProductViewModel> Products{ get; set; }
    }
}