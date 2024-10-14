using e_commerce.BLL.Entities;
using NETMentor.BLL;

namespace e_commerce.BLL;

public interface ICartService
{

    IEnumerable<Item> GetCartItems(Guid idCart);
    Result<Cart> AddCartItem(Guid cartId, Item item);
    void RemoveCartItem( Item item);

    Result<Cart> GetCart(Guid cartId);

    void CreateCart(Guid id);
}