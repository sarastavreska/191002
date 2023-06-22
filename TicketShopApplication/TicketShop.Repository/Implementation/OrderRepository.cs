using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;

namespace TicketShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllActiveOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.Tickets)
                .Include("Tickets.Ticket")

                .ToListAsync().Result;
        }

        public Order GetDetailsForOrders(BaseEntity model)
        {
            return entities
              .Include(z => z.User)
              .Include(z => z.Tickets)
              .Include("Tickets.Ticket")
              .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
