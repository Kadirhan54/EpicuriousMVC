using Epicurious.Application.Repositories;
using Epicurious.Domain.Entities;
using Epicurious.Infrastructure.Contexts.Application;
using Epicurious.Infrastructure.Contexts.Identity;
using Epicurious.Persistence.Repositories;

namespace Epicurious.Persistence.UnitofWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly EpicuriousIdentityContext context;
        private IRepository<Recipe> recipeRepository;
        private IRepository<Comment> commentRepository;

        public UnitOfWork(EpicuriousIdentityContext _context)
        {
            context = _context;
        }

        public IRepository<Recipe> RecipeRepository
        {
            get
            {

                if (recipeRepository == null)
                {
                    recipeRepository = new Repository<Recipe>(context);
                }
                return recipeRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {

                if (commentRepository == null)
                {
                    commentRepository = new Repository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
