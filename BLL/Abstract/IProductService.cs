using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Abstract
{
    public interface IProductService
    {
        //Listeleme
        List<Product> GetActive();

        //Aktif olanları listeleme
        List<Product> GetDefault(Expression<Func<Product, bool>> exp);
        //Ekleme
        void Add(Product Product);
        //Güncelleme
        void Update(Product Product);
        //Silme
        void Remove(Guid id);
        //Getirme
        Product GetById(Guid id);

        //Toplu silme
        void RemoveAll(Expression<Func<Product, bool>> exp);

        //Şart
        bool Any(Expression<Func<Product, bool>> exp);
    }
}
