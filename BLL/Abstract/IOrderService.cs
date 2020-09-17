using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Abstract
{
    public interface IOrderService
    {
        void Add(Order order);
        List<Order> GetOrders();
        List<OrderDetail> GetOrderDetail();
        Order GetById(Guid id);
        void Update(Order order);
    }
}
