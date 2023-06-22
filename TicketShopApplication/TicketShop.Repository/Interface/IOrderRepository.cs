using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;

namespace TicketShop.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> GetAllActiveOrders();
        public Order GetDetailsForOrders(BaseEntity model);
    }
}
