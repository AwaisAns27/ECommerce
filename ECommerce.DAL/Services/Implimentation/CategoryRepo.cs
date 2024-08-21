using ECommerce.DAL.Data;
using ECommerce.DAL.Services.Interfaces;
using ECommerce.Model;

namespace ECommerce.DAL.Services.Implimentation
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public void Remove(Category category)
        {
            var categoryToBedeleted = _context.Catogeries.Remove(category);
        }

        public void Remove(int id)
        {
            var categoryInDb = _context.Catogeries.Find(id);
            categoryInDb.IsActive= false;
        }
    }
}
