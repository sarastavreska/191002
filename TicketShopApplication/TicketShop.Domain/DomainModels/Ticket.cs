using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketShop.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        
        [Required]
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public DateTime VallidTill { get; set; }
        [Required]
        public int TicketPrice { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual ICollection<TicketInOrder> Orders { get; set; }

    }
}
