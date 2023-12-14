using Epicurious.Domain.Entities;
using Epicurious.Infrastructure.Contexts.Application;
using Epicurious.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epicurious.Persistence.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext context;
        private Repository<Recipe> recipeRepository;
        private Repository<Comment> commentRepository;

        public UnitOfWork(ApplicationDbContext _context)
        {
            context = _context;
        }

        public Repository<Recipe> RecipeRepository
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

        public Repository<Comment> CommentRepository
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
