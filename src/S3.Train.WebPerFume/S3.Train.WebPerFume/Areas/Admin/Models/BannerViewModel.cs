using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S3.Train.WebPerFume.Areas.Admin.Models
{
    public class BannerViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Image")]
        [Required]
        public string Image { get; set; }
        [Display(Name = "Link")]
        [Required]
        public string Link { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

    }
}