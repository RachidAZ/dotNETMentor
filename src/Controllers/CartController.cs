using Asp.Versioning;
using CartService.BLL;
using CartService.BLL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;


[ApiVersion(1)]
[ApiVersion(2)]
[ApiController]
[Route("api/v{v:apiVersion}/Cart")]
public class CartController : ControllerBase
{

    private ICartService _cartService;

    public CartController(ICartService cartService)
    {
        this._cartService = cartService;
    }



    [MapToApiVersion(1)]
    [HttpGet("GetCartInfo")]
    public ActionResult GetCartInfoV1 (Guid id) {

        return Ok(_cartService.GetCartItemsAsCart(id));

    }

    [MapToApiVersion(2)]
    [HttpGet("GetCartInfo")]
    public ActionResult GetCartInfoV2(Guid id)
    {

        return Ok(_cartService.GetCartItems(id));

    }

    [MapToApiVersion(1)]
    [HttpPost("AddCartItems")]
    public ActionResult AddCartItems( Guid cartId,  Item item)
    {

        _cartService.AddCartItem(cartId, item);
        return Ok();

    }
    [MapToApiVersion(1)]
    [HttpPost("RemoveCartItem")]
    public ActionResult RemoveCartItem(Guid cartId, int itemId)
    {

        _cartService.RemoveCartItem( _cartService.GetCartItems(cartId).FirstOrDefault(x=> x.Id == itemId));
        return Ok();

    }

    [MapToApiVersion(1)]
    [HttpPost("CreateCart")]
    public ActionResult CreateCart(Guid id)
    {

        _cartService.CreateCart(id);
        return Ok();


    }

}