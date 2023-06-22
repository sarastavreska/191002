using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.Identity;

namespace TicketShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<TicketShopAppUser> GetAll();
        TicketShopAppUser Get(string id);
        void Insert(TicketShopAppUser entity);
        void Update(TicketShopAppUser entity);
        void Delete(TicketShopAppUser entity);
    }
}
