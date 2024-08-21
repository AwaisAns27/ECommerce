using ECommerce.Model;

namespace ECommerce.DAL.Services.Interfaces
{
    public interface ICategoryRepo : IRepository<Category>
    {
        public void Remove(Category category);
        void Remove(int id);
    }
}
