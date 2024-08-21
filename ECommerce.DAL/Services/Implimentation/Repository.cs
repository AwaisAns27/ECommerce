using ECommerce.DAL.Data;
using ECommerce.DAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.DAL.Services.Implimentation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
          _dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProp)
        {
            IQueryable<T> query = _dbSet;
            if(!string.IsNullOrEmpty(includeProp))
            {
                foreach( var item in includeProp.Split(new char[] {',','-'},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.SingleOrDefault(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> GetAll(string? includeProp=null)
        {
           IQueryable<T> query = _dbSet;
            if (!string.IsNullOrEmpty(includeProp))
            {
                foreach (var item in includeProp.Split(new char[]{',','-' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }

            return query.ToList();
        }

      
    }
}
