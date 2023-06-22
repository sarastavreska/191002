using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.Identity;
using TicketShop.Service.Interface;

namespace TicketShop.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<TicketShopAppUser> _userManager;

        public AdminController(IOrderService orderService, UserManager<TicketShopAppUser> userManager)
        {
            this._orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllActiveOrders()
        {
            return  this._orderService.getAllOrders();
            
        }
        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity model)
        {
            var result = this._orderService.getOrderDetails(model);
            return result;
        }





    }
}
