using System;
using System.Collections.Generic;

namespace TicketShopAdminApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public TicketShopAppUser User { get; set; }

        public  ICollection<TicketInOrder> Tickets { get; set; }
    }
}
