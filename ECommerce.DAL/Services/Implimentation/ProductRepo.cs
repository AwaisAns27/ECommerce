using ECommerce.DAL.Data;
using ECommerce.DAL.Services.Interfaces;
using ECommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Services.Implimentation
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private readonly ApplicationDbContext _context;
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<Product> IncludeCat()
        {
            var list = _context.Products.Include(c => c.Category).Where(c => c.IsActive == true).ToList();
            return (list);
        }
        public void Remove(int id)
        {
            var productToBeDeleted = _context.Products.Find(id);
            productToBeDeleted.IsActive = false;

        }

    }
}
