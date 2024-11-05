using CartService.BLL.Entities;
using CartService.DAL;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using CartService.BLL;
using CartService.DAL;
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
        if (cart is null)
            return Result<Cart>.Failure("Cart Unfound");
        else
            return Result<Cart>.Success(cart);  
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
