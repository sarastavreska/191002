using System.Collections.Generic;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<TicketInShoppingCart> Tickets { get; set; }

        public double TotalPrice { get; set; }
    }
}
