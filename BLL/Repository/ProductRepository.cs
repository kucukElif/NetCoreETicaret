using BLL.Abstract;
using DAL.Context;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Repository
{
    public class ProductRepository : IProductService
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Product Product)
        {
            context.Products.Add(Product);
            context.SaveChanges();
        }

        public bool Any(Expression<Func<Product, bool>> exp)
        {
            return context.Products.Any(exp);
        }

        public List<Product> GetActive()
        {
            return context.Products.Where(x => x.Status == DAL.Entity.Enum.Status.Active).ToList();
        }

        public Product GetById(Guid id)
        {
            return context.Products.FirstOrDefault(x => x.ID == id);
        }

        public List<Product> GetDefault(Expression<Func<Product, bool>> exp)
        {
            return context.Products.Where(exp).ToList();
        }

        public void Remove(Guid id)
        {
            Product Product = GetById(id);
            Product.Status = DAL.Entity.Enum.Status.Deleted;
            Update(Product);
        }

        public void RemoveAll(Expression<Func<Product, bool>> exp)
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = DAL.Entity.Enum.Status.Deleted;
                Update(item);
            }
        }

        public void Update(Product Product)
        {
            context.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
