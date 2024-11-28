using CartService.BLL.Entities;
using CartService.DAL;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL;

public class CartService : ICartService
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

    public Result<Cart> AddCartItem(Guid cartId, Item item)
    {
        
        var cartResult = GetCart(cartId);

        if (!cartResult.IsSuccess)
            return Result<Cart>.Failure(cartResult.Error);


        cartResult.Value.Items.Add(item);

        _itemRepository.Add(item);

        return Result<Cart>.Success(cartResult.Value);

       //_cartRepository.Update(cart);

    }

    public Result<Cart> GetCart(Guid cartId)
    {
        var cart = _cartRepository.GetByKey(cartId);
        //var carts = _cartRepository.GetAll();
        //var cart=carts.FirstOrDefault(k=> k.Id == cartId);
        if (cart is null)
            return Result<Cart>.Failure("Cart Unfound");
        else
            return Result<Cart>.Success(cart);  
    }

    public Cart GetCartItemsAsCart(Guid idCart)
    {
      
        var items= _itemRepository.GetAll().Where(x => x.CartId == idCart);
        var cart=new Cart(idCart) { Items = items.ToList() };
        return cart;


    }

    public IEnumerable<Item> GetCartItems(Guid idCart)
    {
     
        return _itemRepository.GetAll().Where(x => x.CartId == idCart);
     
    }

    public void RemoveCartItem( Item item)
    {
        // _cartRepository.GetByKey(cartId).Items.Remove(item);
        _itemRepository.Delete(item);

    }

    public void UpdateItem(Item item)
    {
        _itemRepository.Update(item);
    }

    public IEnumerable<Item> GetItems()
    {
        return _itemRepository.GetAll();
    }
}
