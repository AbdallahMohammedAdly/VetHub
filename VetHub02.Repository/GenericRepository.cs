using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Repositories;
using VetHub02.Core.Specifications;
using VetHub02.Repository.Data;

namespace VetHub02.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
        {
        private readonly StoreContext dbcontext;
        private interface IUser
        {
            string Email { get; set; }
        }
        public GenericRepository(StoreContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbcontext.Set<T>().ToListAsync();
        }

  

        public async Task<T> GetByIdAsync(int id)
            => await dbcontext.Set<T>().FindAsync(id) ?? null!;
        



        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> sepc)
        {

            return await ApplySpecification(sepc).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> sepc)
        {
            return await ApplySpecification(sepc).FirstOrDefaultAsync() ?? null!;
        }
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) 
        {
            return SpecificationEvalutor<T>.GetQuery(dbcontext.Set<T>(), spec);
        }


        public void Add(T entity)
          => dbcontext.Set<T>().Add(entity);
          
        

        public void Update(T entity)
                =>   dbcontext.Set<T>().Update(entity);
            
        

        public void Delete(T entity)
            => dbcontext.Set<T>().Remove(entity);

        //public  async Task<T> GetUserIdByEmail(string email)
        //=> await  dbcontext.Set<T>().FirstOrDefaultAsync(U => (U as IUser).Email == email);
       
        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (email is not null)
            {
            var user = await dbcontext.Users.FirstOrDefaultAsync(U => U.Email == email);
                if ( user is not null)
                {
                return user;

                }
            }

            return new User();
        }

      
    }
}
