using CartService.BLL.Entities;
using CartService.DAL;
using Microsoft.EntityFrameworkCore;

namespace CartService.DAL;

public class RepositoryCart: IRepository<Cart, Guid>
{

    private readonly ApplicationDBContext _context;

    public RepositoryCart(ApplicationDBContext applicationDBContext)
    {
        this._context = applicationDBContext;
        _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    public void Add(Cart entity)
    {
        _context.Add(entity);
    }

    public void Delete(Cart entity)
    {
        _context.Remove(entity);

    }

    public IEnumerable<Cart> GetAll()
    {
         var all=_context.Carts.ToList();
         return all;

    }

    public Cart GetByKey(Guid id)
    {
        return _context.Carts.Find(id);
    }

    public void Update(Cart entity)
    {
        throw new NotImplementedException();
    }
}