using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Services.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepo ICategoryRepo { get;}
        public IProductRepo IProductRepo { get; }
        void Save();

    }
}
