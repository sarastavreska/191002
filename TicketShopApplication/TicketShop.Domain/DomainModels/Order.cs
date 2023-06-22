using System;
using System.Collections.Generic;
using TicketShop.Domain.Identity;

namespace TicketShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public TicketShopAppUser User { get; set; }

        public virtual ICollection<TicketInOrder> Tickets { get; set; }
    }
}
