using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        protected ISchoolDbContext RepositoryContext { get; set; }

        public RepositoryBase(ISchoolDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAync(Expression<Func<T, bool>> expression)
        {
            return await RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
