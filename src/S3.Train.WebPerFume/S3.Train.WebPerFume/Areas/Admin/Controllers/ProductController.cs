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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IVendorService _vendorService;

        public ProductController() { }

        public ProductController(IProductService productService, IBrandService brandService, IVendorService vendorService)
        {
            _productService = productService;
            _brandService = brandService;
            _vendorService = vendorService;
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            var model = GetProducts(_productService.SelectAll());
            return View(model);
        }

        [HttpGet]
        public ActionResult AddOrEditProduct(Guid? id)
        {
            ProductViewModel model = new ProductViewModel();

            model.DropDownBrand = DropDownList_Brand();
            model.DropDownVendor = DropDownList_Vendor();

            if (id.HasValue)
            {
                var product = _productService.GetById(id.Value);
                model.Id = product.Id;
                model.Name = product.Name;
                model.Brand_Id = product.Brand_Id;
                model.Vendor_Id = product.Vendor_Id;
                model.Description = product.Description;
                model.ImagePath = product.ImagePath;
                model.CreateDate = product.CreatedDate;
                model.IsActive = product.IsActive;
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
        public ActionResult AddOrEditProduct(Guid? id, ProductViewModel model, HttpPostedFileBase image)
        {
            try
            {
                bool isNew = !id.HasValue;
                string localFile = "~/Content/img/product-men";

                // isNew = true update UpdatedDate of product
                // isNew = false get it by id
                var product = isNew ? new Product
                {
                    UpdatedDate = DateTime.Now
                } : _productService.GetById(id.Value);

                product.Name = model.Name;
                product.Brand_Id = model.Brand_Id;
                product.Vendor_Id = model.Vendor_Id;
                product.Description = model.Description;
                product.ImagePath = UpFile(image, localFile);
                product.IsActive = true;

                if (isNew)
                {
                    product.CreatedDate = DateTime.Now;
                    product.Id = Guid.NewGuid();
                    _productService.Insert(product);
                }
                else
                {
                    _productService.Update(product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public PartialViewResult DeleteProduct(Guid id)
        {
            var product = _productService.GetById(id);
            var model = new ProductViewModel
            {
                Name = $"{product.Name}"
            };
            return PartialView("~/Areas/Admin/Views/Product/_DeleteProduct.cshtml", model);
        }

        [HttpPost]
        public ActionResult DeleteProduct(ProductViewModel model)
        {
            var product = _productService.GetById(model.Id);
            _productService.Delete(product);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Drop Down List Brand
        /// </summary>
        /// <returns></returns>
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
        /// Drop Down List Vendor
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> DropDownList_Vendor()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in _vendorService.SelectAll())
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            return items;
        }


        /// <summary>
        /// Convert List Product to List ProductViewModel All Properties
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public IList<ProductViewModel> GetProducts(IList<Product> products)
        {
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Brand = _brandService.GetById(x.Brand_Id),
                Vendor = _vendorService.GetById(x.Vendor_Id),
                Description = x.Description,
                ImagePath = x.ImagePath,
                CreateDate = x.CreatedDate,
                IsActive = x.IsActive
            }).ToList();
        }

        public IList<ProductViewModel> GetProduct_SummaryInfo(IList<Product> products)
        {
            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
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