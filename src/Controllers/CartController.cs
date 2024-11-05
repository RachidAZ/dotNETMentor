using CartService.BLL;
using CartService.BLL.Entities;
using CartService.DAL;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{

    private ICartService _cartService;

    public CartController(ICartService cartService)
    {
        this._cartService = cartService;
    }




    [HttpGet("GetCartItems" )]
    public ActionResult GetCartItems (Guid id) {

        return Ok(_cartService.GetCartItems(id));

    }

    [HttpPost("AddCartItems")]
    public ActionResult AddCartItems( Item item)
    {

        _cartService.AddCartItem(item.CartId, item);
        return Ok();

    }

    [HttpPost("RemoveCartItem")]
    public ActionResult RemoveCartItem(int itemId, Guid cartId)
    {

        _cartService.RemoveCartItem( _cartService.GetCartItems(cartId).FirstOrDefault(x=> x.Id == itemId));
        return Ok();

    }


    [HttpPost("CreateCart")]
    public ActionResult CreateCart(Guid id)
    {

        _cartService.CreateCart(id);
        return Ok();


    }

}