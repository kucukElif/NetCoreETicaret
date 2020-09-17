using BLL.Abstract;
using DAL.Context;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.Repository
{
    public class OrderRepository : IOrderService
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public Order GetById(Guid id)
        {
            return context.Orders.Find(id);
        }

        public List<OrderDetail> GetOrderDetail()
        {
           return context.OrderDetails.ToList();
        }

        public List<Order> GetOrders()
        {
           return context.Orders.ToList();
        }

        public void Update(Order order)
        {
            context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
