using EShop.Core.Interfaces.Repositories;
using EShop.Core.Interfaces.UnitOfWork;
using EShop.DAL.Context;
using EShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EShopDbContext _context;
        public UnitOfWork(EShopDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
