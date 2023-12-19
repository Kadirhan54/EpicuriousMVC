using Epicurious.Application.Repositories;
using Epicurious.Domain.Common;
using Epicurious.Infrastructure.Contexts.Application;
using Epicurious.Infrastructure.Contexts.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;
namespace Epicurious.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntityBase<Guid>
    {
        private readonly EpicuriousIdentityContext _context;

        public Repository(EpicuriousIdentityContext context)
        {
            _context = context;
        }

        public T GetById(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Where(e => e.Id == id);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
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
