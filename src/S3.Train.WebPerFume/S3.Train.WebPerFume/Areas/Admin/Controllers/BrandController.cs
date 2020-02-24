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
    public class BrandController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;

        public BrandController() { }
        public BrandController(IProductService productService, IBrandService brandService,)
        {
            _productService = productService;
            _brandService = brandService;
        }
        // GET: Admin/Brand
        public ActionResult Index()
        {
            var model = GetBrands(_brandService.SelectAll());
            return View(model);
        }
        [HttpGet]
        public ActionResult AddOrEditBrand(Guid? id)
        {
            BrandViewModel model = new BrandViewModel();
            model.MyItems = new List<SelectListItem>
            {

            };
            if (id.HasValue)
            {
                var brand = _brandService.GetById(id.Value);
                model.Id = brand.Id;
                model.Name = brand.Name;
 
                model.Summary = brand.Summary;
                model.Logo = brand.Logo;
                model.CreateDate = brand.CreatedDate;
                model.IsActive = brand.IsActive;
                return View(model);
            }
            else
                return View();
        }

        public List<SelectListItem> DropDownList_Brand()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _brandService.SelectAll())
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return items;
        }

        /// <summary>
        /// If id != null Update else Create new
        /// </summary>
        /// <param name="id">Guid</param>
        /// <param name="model">ProductViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddOrEditBrand(Guid? id, BrandViewModel model)
        {
            try
            {
                bool isNew = !id.HasValue;

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var brand = isNew ? new Brand
                {
                    UpdatedDate = DateTime.Now
                } : _brandService.GetById(id.Value);

                brand.Name = model.Name;

                brand.Summary = model.Summary;
                brand.Logo= model.Logo;
                brand.IsActive = true;

                if (isNew)
                {
                    brand.CreatedDate = DateTime.Now;
                    brand.Id = Guid.NewGuid();
                    _brandService.Insert(brand);
                }
                else
                {
                    _brandService.Update(brand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        private IList<BrandViewModel> GetBrands(IList<Brand> brands)
        {
            return brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name,         
                Summary = x.Summary,
                Logo= x.Logo,
                CreateDate = x.CreatedDate,
             
                IsActive = x.IsActive
            }).ToList();
        }
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