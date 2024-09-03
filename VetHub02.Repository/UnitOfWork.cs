using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Repositories;
using VetHub02.Core.UnitOfWork;
using VetHub02.Repository.Data;

namespace VetHub02.Repository
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly StoreContext context;
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);

                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity> ?? null! ;
        }
        public void Complete()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message ?? ex.Message;
                // Log the error (innerException)
                throw new Exception("An error occurred while saving changes to the database: " + innerException);
            }
        }

        public async ValueTask DisposeAsync()
        => await context.DisposeAsync();
    }
}
