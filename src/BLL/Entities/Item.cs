using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Entities;

public class Item
{

    public int Id {  get; set; }
    public string Name {  get; set; }
    public string ImageUrl {  get; set; }
    public string ImageAltText {  get; set; }
    public Price Price {  get; set; }
    public int Quantity {  get; set; }

    // Foreign key for the Cart entity
    public Guid CartId { get; set; }

    //public Guid CartId { get; set; }
    //public Cart Cart { get; set; }  


    public Item()
    {

        
    }

}


public record Price(int PriceAmount, int Currency);

