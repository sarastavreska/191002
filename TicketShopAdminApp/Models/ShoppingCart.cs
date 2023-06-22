using System.Collections.Generic;

namespace TicketShopAdminApp.Models
{
    public class ShoppingCart
    {
        public string OwnerId { get; set; }
        public TicketShopAppUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
