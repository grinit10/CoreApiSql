using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Remotion.Linq.Clauses;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        private ISchoolDbContext RepositoryContext { get; set; }

        public RepositoryBase(ISchoolDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            var query = RepositoryContext.Set<T>().Where(expression);
            typeof(T).GetProperties().ToList().ForEach(p => query.Include(p.Name));
            return await query.ToListAsync();
        }

        public T Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            return entity;
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
