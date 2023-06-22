using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Domain.Identity;
using TicketShop.Repository;
using TicketShop.Service.Interface;

namespace TicketShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly UserManager<TicketShopAppUser> _userManager;
        public ShoppingCartController(IShoppingCartService shoppingCartService, UserManager<TicketShopAppUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);                     
            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }
        public IActionResult DeleteFromShoppingCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=this._shoppingCartService.deleteTicketFromSoppingCart(userId, id);
            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");

            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

           
      
        }
        public IActionResult OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.order(userId);
            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");

            }
            else
            {
                return RedirectToAction("Index", "ShoppingCart");

            }


        }

    }
}
