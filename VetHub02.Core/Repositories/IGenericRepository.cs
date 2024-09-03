using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;
using VetHub02.Core.Specifications;

namespace VetHub02.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
        Task<User> GetUserByEmailAsync(string email);

        Task<int> GetCountWithSpecAsync(ISpecification<T>spec);
    }
}
