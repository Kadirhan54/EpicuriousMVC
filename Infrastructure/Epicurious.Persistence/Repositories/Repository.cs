using Epicurious.Application.Repositories;
using Epicurious.Infrastructure.Contexts.Application;
using Epicurious.Infrastructure.Contexts.Identity;
namespace Epicurious.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EpicuriousIdentityContext _context;

        public Repository(EpicuriousIdentityContext context)
        {
            _context = context;
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
