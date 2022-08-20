using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModel;
using MyAppWeb.Data;
using System.IO;

using System.Collections.Generic;

namespace MyAppWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitofwork;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork context, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = context;
            _webHostEnvironment = webHostEnvironment;
        }
        #region ApiCall
        public IActionResult AllProducts()
        {
            var products = _unitofwork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        #endregion
        public IActionResult Index()
        {

            //ProductVM vm = new ProductVM();
            //vm.Products = _unitofwork.Product.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                Categories = _unitofwork.Category.GetAll().Select(x =>
                  new SelectListItem()
                  {
                      Text = x.Name,
                      Value = x.Id.ToString()
                  })
            };

            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitofwork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {

                    return View(vm);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;
                if (file != null)
                {
                    #region api-122
                    string uploadpath = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    #endregion
                    string filePath = Path.Combine(uploadpath, fileName);
                    if (vm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;

                }
                if (vm.Product.Id == 0)
                {
                    _unitofwork.Product.Add(vm.Product);
                    TempData["success"] = "Product Created successfully";
                }
                else
                {
                    _unitofwork.Product.Update(vm.Product);
                    TempData["success"] = "Product Updated successfully";
                }
                _unitofwork.Save();
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        #region DeleteApiCall
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var product = _unitofwork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "error in fetching data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofwork.Product.Delete(product);
                _unitofwork.Save();
                return Json(new { success = true, message = "Product Deleted" });
            }
        }
        #endregion
    }
}
