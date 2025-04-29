using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pexel.Application.Contracts.Interfaces;
using Pexel.Application.Contracts.Services;
using Pexel.Core.DTOs.Orders;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Infrastructrue.Implementation
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderServices(ApplicationDbContext dbContext, IBasketRepository basketRepository, IProductRepository productRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Order> CreateOrder(OrderDto orderDto, string CustomerEmail)
        {
            var basket = await _basketRepository.GetBasketAsync(orderDto.basketId);
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var item in basket.Items) 
            {
                var product = await _productRepository.GetByIdAsync(item.Id);
                var orderitem = new OrderItem(product.ProductId,product.Name,item.Price,item.Quantity);
                orderItems.Add(orderitem);
            }

            var deliveryMethod = await _dbContext.DeliverMethods.FirstOrDefaultAsync(d => d.Id == orderDto.deliveryId);
            if (deliveryMethod == null)
                throw new Exception("Not Found!");

            var subtotal = orderItems.Sum(p=> p.Price * p.Quantity) + deliveryMethod.Price;
            var Address = _mapper.Map<CustomerAddress>(orderDto.customerAddress);

            var order = new Order(CustomerEmail,subtotal,Address,deliveryMethod,orderItems);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await _basketRepository.DeleteBasketAsync(orderDto.basketId);
            return order;
        }

      public async Task<IReadOnlyList<Order>> GetAllUserOrders(string CustomerEmail)
        {
            var order = await _dbContext.Orders.Where(o=> o.CustomerEmail == CustomerEmail).Include(i=> i.orderItems).Include(i=> i.deliveryMethod).ToListAsync();
            return order;
        }

        public async Task<Order> GetOrderById(int id, string CustomerEmail)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == id && o.CustomerEmail == CustomerEmail)
                .Include(i => i.orderItems).Include(i => i.deliveryMethod).FirstOrDefaultAsync();
            if (order == null)
                throw new Exception("Not Found Any Order");

            return order;
        }
    }
}
