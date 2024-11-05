using CartService.BLL.Entities;
using CartService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCartService
{
    public class RepoMock : IRepository<Cart, Guid> , IRepository<Item, int>
    {

        IList<Cart> Carts = new List<Cart>();
        IList<Item> Items = new List<Item>();



        public void Add(Cart entity)
        {
            Carts.Add(entity);

        }

        public void Add(Item entity)
        {
            Items.Add(entity);
        }

        public void Delete(Cart entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Item entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cart> GetAll()
        {
            return Carts;

        }

        public Cart GetByKey(Guid id)
        {
            return Carts.FirstOrDefault(x => x.Id == id);
        }

        public Item GetByKey(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Cart entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Item> IRepository<Item, int>.GetAll()
        {
            return Items;   

        }
    }
}
