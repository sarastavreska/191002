using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DTO;

namespace TicketShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteTicketFromSoppingCart(string userId, Guid productId);
        bool order(string userId);
    }
}
