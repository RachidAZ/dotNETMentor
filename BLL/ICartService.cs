using e_commerce.BLL.Entities;

namespace e_commerce.BLL;

public interface ICartService
{

    IEnumerable<Item> GetCartItems(Guid idCart);
    void AddCartItem(Guid cartId, Item item);
    void RemoveCartItem( Item item);

    Cart GetCart(Guid cartId);

    void CreateCart(Guid id);
}