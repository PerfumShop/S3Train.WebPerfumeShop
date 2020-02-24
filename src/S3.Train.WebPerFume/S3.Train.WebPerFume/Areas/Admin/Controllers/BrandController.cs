using S3.Train.WebPerFume.Areas.Admin.Models;
using S3Train.Contract;
using S3Train.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ProductController _productController;

        public BrandController()
        {

        }

        public BrandController(IProductService productService, IBrandService brandService, ProductController productController)
        {
            _productService = productService;
            _brandService = brandService;
            _productController = productController;
        }

        // GET: Admin/Brand
        public ActionResult Index()
        {
            var model = GetBrands(_brandService.SelectAll());
            return View(model);
        }

        private IList<BrandViewModel> GetBrands(IList<Brand> brands)
        {
            return brands.Select(x => new BrandViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Summary = x.Summary,
                Logo = x.Logo,
                CreateDate = x.CreatedDate,
                UpdateDate = x.UpdatedDate,
                Products = _productController.GetProduct_SummaryInfo(_productService.GetProductsByBrandId(x.Id)),
                IsActive = x.IsActive
            }).ToList();
        }
    }
}