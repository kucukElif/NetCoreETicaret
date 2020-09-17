using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.CartModel
{
    public class Cart
    {
        //Sepet
        public Dictionary<Guid, CartItem> _myCart = new Dictionary<Guid, CartItem>();
        
        //Sepetteki ürünleri Listeleme
        public List<CartItem> MyCart
        {
            get
            {
                return _myCart.Values.ToList();
            }
        }

        //Sepete ürün ekleme
        public void AddItem(CartItem item)
        {
            if (_myCart.ContainsKey(item.ID))
            {
                _myCart[item.ID].Quantity += 1;
                return;
            }
            _myCart.Add(item.ID, item);
        }

        //Update Item

    }
}
