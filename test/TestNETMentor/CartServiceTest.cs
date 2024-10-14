using e_commerce.BLL;
using e_commerce.BLL.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NETMentor.BLL;
using Xunit;

namespace TestNETMentor;

public class CartServiceTest
{

    private readonly ICartService _cartService;
    public CartServiceTest()
    {

        _cartService = new CartService(new RepoMock(), new RepoMock());

      

    }



    [Fact]
    public void Test_AddItemToCart_ShouldSucceed_WhenCartIsFound()
    {

        //Arange
        Guid cartId = Guid.NewGuid();
        var item = new Item() { Id = 123, Name = "product1", CartId = cartId };
        _cartService.CreateCart(cartId);


        //Act
        var result=_cartService.AddCartItem(cartId, item);

        //Asert
        var searchItem = _cartService.GetCartItems(cartId).FirstOrDefault(x => x.Id == 123);
        Assert.NotNull(searchItem);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Test_AddItemToCart_ShouldFail_WhenCartUnfound()
    {

        //Arange
        Guid cartId = Guid.NewGuid();
        var item = new Item() { Id = 123, Name = "product1", CartId = cartId };

        //Act
        var result= _cartService.AddCartItem(cartId, item);

        //Asert
        var searchItem = _cartService.GetCartItems(cartId).FirstOrDefault(x => x.Id == 123);
        Assert.Null(searchItem);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Test_GetItemsFromCart()
    {

    }


    [Fact]
    public void Test_RemoveItemFromCart()
    {



    }
}