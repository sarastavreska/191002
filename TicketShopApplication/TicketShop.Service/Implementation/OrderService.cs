using System;
using System.Collections.Generic;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Repository.Interface;
using TicketShop.Service.Interface;

namespace TicketShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
       
        public List<Order> getAllOrders()
        {
            return this._orderRepository.GetAllActiveOrders();
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetDetailsForOrders(model);
        }
    }
}
