using ECommerce.Model;

namespace ECommerce.DAL.Services.Interfaces
{
    public interface IProductRepo : IRepository<Product>
    {
        IEnumerable<Product> IncludeCat();
        void Remove(int id);
    }
}
