using System;

namespace TicketShopAdminApp.Models
{
    public class TicketInOrder
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
