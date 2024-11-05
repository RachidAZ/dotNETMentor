using CartService.BLL.Entities;
using CartService.BLL;

namespace CartService.BLL;

public interface ICartService
{

    IEnumerable<Item> GetCartItems(Guid idCart);
    Result<Cart> AddCartItem(Guid cartId, Item item);
    void RemoveCartItem( Item item);

    Result<Cart> GetCart(Guid cartId);

    void CreateCart(Guid id);
}