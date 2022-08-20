using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models.ViewModel;
using System.Security.Claims;

namespace MyAppWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _unitofwork;
        public CartVM vm;
        public CartController(IUnitOfWork unitofWork)
        {
            _unitofwork = unitofWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            vm = new CartVM()
            {
                ListofCarts = _unitofwork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product"),
                orderHeader=new MyApp.Models.OrderHeader()
            };
            vm.orderHeader.ApplicationUser = _unitofwork.ApplicationUser.GetT(x => x.Id == claims.Value);
            vm.orderHeader.Name = vm.orderHeader.ApplicationUser.Name;
            vm.orderHeader.Phone = vm.orderHeader.ApplicationUser.PhoneNumber;
            vm.orderHeader.Address = vm.orderHeader.ApplicationUser.Address;
            vm.orderHeader.City = vm.orderHeader.ApplicationUser.City;
            vm.orderHeader.State = vm.orderHeader.ApplicationUser.State;
            vm.orderHeader.PostalCode = vm.orderHeader.ApplicationUser.PinCode;
            vm.orderHeader.Name = vm.orderHeader.ApplicationUser.Name;
            foreach (var item in vm.ListofCarts)
            {
                vm.orderHeader.OrderTotal += (item.Count * item.Product.Price);
            }
            return View(vm);
        }
        public IActionResult plus(int id)
        {
            var cart = _unitofwork.Cart.GetT(x => x.Id == id);
            _unitofwork.Cart.IncrementCartItem(cart, 1);
            _unitofwork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int id)
        {
            var cart = _unitofwork.Cart.GetT(x => x.Id == id);
            if (cart.Count <= 1)
            {
                _unitofwork.Cart.Delete(cart);
            }
            else
            {
                _unitofwork.Cart.DecrementCartItem(cart, 1);
            }
            _unitofwork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int id)
        {
            var cart=_unitofwork.Cart.GetT(x=>x.Id==id);
            _unitofwork.Cart.Delete(cart);
            _unitofwork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult sort()
        {
           
             var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            vm = new CartVM()
            {
                ListofCarts = _unitofwork.Cart.GetAll(y=>y.ApplicationUserId==claims.Value, includeProperties: "Product").OrderBy(x=>x.Product.Price),
                 orderHeader=new MyApp.Models.OrderHeader()
            };
             vm.orderHeader.ApplicationUser = _unitofwork.ApplicationUser.GetT(x => x.Id == claims.Value);
            vm.orderHeader.Name = vm.orderHeader.ApplicationUser.Name;
            vm.orderHeader.Phone = vm.orderHeader.ApplicationUser.PhoneNumber;
            vm.orderHeader.Address = vm.orderHeader.ApplicationUser.Address;
            vm.orderHeader.City = vm.orderHeader.ApplicationUser.City;
            vm.orderHeader.State = vm.orderHeader.ApplicationUser.State;
            vm.orderHeader.PostalCode = vm.orderHeader.ApplicationUser.PinCode;
            vm.orderHeader.Name = vm.orderHeader.ApplicationUser.Name;
            foreach (var item in vm.ListofCarts)
            {
                vm.orderHeader.OrderTotal += (item.Count * item.Product.Price);
            }            
            return View(vm);
        }
        public IActionResult Summary()
        {
            return View();
        }


    }
}
