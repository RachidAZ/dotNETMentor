using e_commerce.BLL.Entities;
using e_commerce.DAL;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NETMentor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.BLL
{
    internal class CartService : ICartService
    {

        private readonly IRepository<Cart, Guid> _cartRepository;
        private readonly IRepository<Item, int> _itemRepository;

        public CartService(IRepository<Cart, Guid> repositoryCart, IRepository<Item, int> repositoryItem)
        {
            this._cartRepository = repositoryCart;
            this._itemRepository = repositoryItem;
        }

        public void CreateCart(Guid id)
        {
            var c = new Cart(id);
            _cartRepository.Add(c);
        }

        public void AddCartItem(Guid cartId, Item item)
        {
            
            var cart = GetCart(cartId);
            cart.Items.Add(item);

            _itemRepository.Add(item);
            _cartRepository.Update(cart);

        }

        public Cart GetCart(Guid cartId)
        {
            return _cartRepository.GetByKey(cartId);
        }

        public IEnumerable<Item> GetCartItems(Guid idCart)
        {
            //Cart cart = _cartRepository.GetByKey(idCart);
            return _itemRepository.GetAll().Where(x => x.CartId == idCart);
            //return cart.Items;   
        }

        public void RemoveCartItem( Item item)
        {
            // _cartRepository.GetByKey(cartId).Items.Remove(item);
            _itemRepository.Delete(item);

        }
    }
}
