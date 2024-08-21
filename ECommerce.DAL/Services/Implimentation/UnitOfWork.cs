using ECommerce.DAL.Data;
using ECommerce.DAL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Services.Implimentation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepo ICategoryRepo { get; private set; }

        public IProductRepo IProductRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ICategoryRepo = new CategoryRepo(context);
            IProductRepo = new ProductRepo(context);
            
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
