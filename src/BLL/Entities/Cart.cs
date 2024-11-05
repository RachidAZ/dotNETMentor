using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Entities;

public class Cart
{

    public Guid Id { get; set; }
    public IList<Item> Items { get; set; }

    public Cart(Guid id) {
    
        this.Id = id;
        Items = new List<Item>();
    }   


}
