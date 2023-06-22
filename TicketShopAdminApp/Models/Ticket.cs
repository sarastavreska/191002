using System;

namespace TicketShopAdminApp.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public DateTime VallidTill { get; set; }
        
        public int TicketPrice { get; set; }
       
    }
}
