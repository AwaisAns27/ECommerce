using System.Linq.Expressions;

namespace ECommerce.DAL.Services.Interfaces
{
    public interface IRepository<Tentity> where Tentity : class

    {
        IEnumerable<Tentity> GetAll();
        IEnumerable<Tentity> GetAll(string? includeProp= null);
        Tentity Get(int id);
        Tentity Get(Expression<Func<Tentity, bool>> filter);
        Tentity Get(Expression<Func<Tentity, bool>> filter, string? includeProp);
        void Add(Tentity entity);
        void Update(Tentity entity);
        

    }
}
