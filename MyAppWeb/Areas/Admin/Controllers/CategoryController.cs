using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModel;
using MyAppWeb.Data;

using System.Collections.Generic;

namespace MyApp.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork context)
        {
            _unitofwork = context;
        }
        #region ApiCall
        public IActionResult AllCategories()
        {
            var category = _unitofwork.Category.GetAll();
            return Json(new { data = category });
        }
        #endregion
        public IActionResult Index()
        {

           // CategoryVM vm = new CategoryVM();
           //vm.categories =_unitofwork.Category.GetAll();
            return View();
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (category==null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _unitofwork.Category.Add(category);
        //            _unitofwork.Save();
        //            TempData["success"] = "Category created successfully";
        //            return RedirectToAction("index");
        //        }
        //    }                  
        //    return View();
        //}
        [HttpGet]
       
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null||id==0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unitofwork.Category.GetT(x => x.Id == id); 
                if (vm.Category == null)
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
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0)
                {
                    _unitofwork.Category.Add(vm.Category);                  
                    TempData["success"] = "Category Created successfully";
                }
                else
                {
                    _unitofwork.Category.Update(vm.Category);                 
                    TempData["success"] = "Category Updated successfully";                   
                }
                _unitofwork.Save();
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _unitofwork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Delete(category);
            _unitofwork.Save();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("index");
        }
    }
}
