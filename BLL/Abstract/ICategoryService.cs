using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Abstract
{
    public interface ICategoryService
    {
        //Listeleme
        List<Category> GetActive();

        //Aktif olanları listeleme
        List<Category> GetDefault(Expression<Func<Category, bool>> exp);
        //Ekleme
        void Add(Category category);
        //Güncelleme
        void Update(Category category);
        //Silme
        void Remove(Guid id);
        //Getirme
        Category GetById(Guid id);

        //Toplu silme
        void RemoveAll(Expression<Func<Category, bool>> exp);

        //Şart
        bool Any(Expression<Func<Category, bool>> exp);
    }
}
