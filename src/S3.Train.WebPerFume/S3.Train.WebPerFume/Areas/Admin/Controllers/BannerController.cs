using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        private readonly IBannerService _bannerService;
    

        public BannerController() { }

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
          
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            var model = GetBanners(_bannerService.SelectAll());
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditBanner(Guid? id)
        {
            BannerViewModel model = new BannerViewModel();

          

            if (id.HasValue)
            {
                var banner = _bannerService.GetById(id.Value);
                model.Id = banner.Id;
                model.Image = banner.Image;
                model.Link = banner.Link;
                model.CreateDate = banner.CreatedDate;
                model.IsActive = banner.IsActive;
                return View(model);
            }
            else
                return View(model);
        }

        /// <summary>
        /// If id != null Update else Create new
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="model">ProductViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOrEditBanner(Guid? id, BannerViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = "~/Content/img/product-men";

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var banner = isNew ? new Banner
                {
                    UpdatedDate = DateTime.Now
                } : _bannerService.GetById(id.Value);
                banner.Image = UpFile(image, localFile);
                banner.Link = model.Link;
                banner.IsActive = true;

                if (isNew)
                {
                    banner.CreatedDate = DateTime.Now;
                    banner.Id = Guid.NewGuid();
                    _bannerService.Insert(banner);
                }
                else
                {
                    _bannerService.Update(banner);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public PartialViewResult DeleteBanner(Guid id)
        {
            var banner = _bannerService.GetById(id);
            var model = new BannerViewModel
            {
                Image = $"{banner.Image}"
            };
            return PartialView("~/Areas/Admin/Views/Banner/_DeleteBanner.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteBanner(BannerViewModel model)
        {
            var banner = _bannerService.GetById(model.Id);
            _bannerService.Delete(banner);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Drop Down List Brand
        /// </summary>
        /// <returns></returns>
        

        /// <summary>
        /// Drop Down List Vendor
        /// </summary>
        /// <returns></returns>
      

        private IList<BannerViewModel> GetBanners(IList<Banner> banners)
        {
            return banners.Select(x => new BannerViewModel
            {
                Id = x.Id,
                Link = x.Link,

                Image = x.Image,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();
        }


        /// <summary>
        /// Upload file and save in folder
        /// </summary>
        /// <param name="a">choose file</param>
        /// <param name="url">local save file </param>
        /// <returns>file name</returns>
        public string UpFile(HttpPostedFileBase a, string url)
        {
            string fileName = "";
            if (a != null && a.ContentLength > 0)
            {
                fileName = Path.GetFileName(a.FileName).ToString();
                string path = Path.Combine(Server.MapPath(url), fileName);
                a.SaveAs(path);
                return fileName;
            }
            else
            {
                return fileName;
            }
        }
    }
}