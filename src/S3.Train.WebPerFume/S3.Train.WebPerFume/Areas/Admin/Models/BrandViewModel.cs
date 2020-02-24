using DocumentFormat.OpenXml.Wordprocessing;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class BrandViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [UIHint("FormControlTextBox")]
        [Required]
        public string Name { get; set; }

        [UIHint("FormControlTextBox")]
        [Required]
        public string Summary { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Logo")]
        [Required]
        public string Logo { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [UIHint("DropDownList")]
        public List<SelectListItem> MyItems { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}