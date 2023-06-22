﻿using System;
using System.Collections.Generic;
using TicketShop.Domain.Identity;

namespace TicketShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        
        public string OwnerId { get; set; }
        public TicketShopAppUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts{ get; set; }
    }
}
