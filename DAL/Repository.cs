using e_commerce.BLL.Entities;
using e_commerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace NETMentor.DAL
{
    public class Repository<T,TKey> : IRepository<T, TKey> where T : class
    {

        private readonly ApplicationDBContext _context;

        public Repository(ApplicationDBContext applicationDBContext)
        {
            this._context = applicationDBContext;
            _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
             
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByKey(TKey id)
        {
            return _context.Set<T>().Find(id);
            
        }

        public void Update(T entity)
        {

            _context.Set<T>().Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();


        }
    }
}
