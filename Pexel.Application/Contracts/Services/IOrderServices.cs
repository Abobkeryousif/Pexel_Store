using Pexel.Core.DTOs.Orders;
using Pexel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Application.Contracts.Services
{
    public interface IOrderServices
    {
        Task<Order> CreateOrder(OrderDto orderDto,string CustomerEmail); 
        Task<IReadOnlyList<Order>> GetAllUserOrders(string CustomerEmail);  
        Task<Order> GetOrderById (int id,string CustomerEmail);
        
    }
}
