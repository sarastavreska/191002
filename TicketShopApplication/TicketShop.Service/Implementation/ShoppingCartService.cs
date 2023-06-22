using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.DTO;
using TicketShop.Repository.Interface;
using TicketShop.Service.Interface;

namespace TicketShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _emailRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<EmailMessage> emailRepository,IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<TicketInOrder> ticketInOrderRepository)
        {
            _emailRepository = emailRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
        }
        public bool deleteTicketFromSoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                var itemToDelete = userCart.TicketInShoppingCarts.Where(z => z.TicketId.Equals(id)).FirstOrDefault();

                userCart.TicketInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);
            var userShoppingCart = loggedInUser.UserCart;
            var ticketPrice = userShoppingCart.TicketInShoppingCarts.Select(z => new
            {
                TicketPrice = z.Ticket.TicketPrice,
                Quantity = z.Quantity
            }).ToList();


            double totalPrice = 0.0;

            foreach (var item in ticketPrice)
            {
                totalPrice += item.Quantity * item.TicketPrice;
            }          

            ShoppingCartDto dtoItem = new ShoppingCartDto
            {
                TotalPrice = totalPrice,
                Tickets = userShoppingCart.TicketInShoppingCarts.ToList()

            };
            return dtoItem;
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                EmailMessage message=new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Sucessfuly created order!";
                message.Status = false;

                Order order = new Order
                {
                    
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<TicketInOrder> ticketInOrders = new List<TicketInOrder>();

                var result = userCart.TicketInShoppingCarts.Select(z => new TicketInOrder
                {
                    TicketId = z.Ticket.Id,
                    Ticket = z.Ticket,
                    OrderId = order.Id,
                    Order = order
                }).ToList();


                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += 1* currentItem.Ticket.TicketPrice;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Ticket.MovieName + " with quantity of: " + "1 " + " and price of: $" + currentItem.Ticket.TicketPrice);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                message.Content = sb.ToString();



                ticketInOrders.AddRange(result);

                foreach (var item in ticketInOrders)
                {
                    this._ticketInOrderRepository.Insert(item);
                }


                loggedInUser.UserCart.TicketInShoppingCarts.Clear();
                this._userRepository.Update(loggedInUser);
                this._emailRepository.Insert(message);

                return true;
            }
            return false;

        }
    }
}
