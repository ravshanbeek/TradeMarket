using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TradeMarketDbContext context;
        public ProductRepository(TradeMarketDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Product entity)
        {
            await context.Products.AddAsync(entity);
        }

        public void Delete(Product entity)
        {
            context.Products.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Products.RemoveRange(context.Products.Where(p => p.Id == id));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await context.Products.AsNoTracking()
                .Include(x => x.ReceiptDetails)
                .Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products.AsNoTracking().Where(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return await context.Products.AsNoTracking().Include(x => x.ReceiptDetails).Include(x =>x.Category).FirstOrDefaultAsync(x =>x.Id ==id);
        }

        public async void Update(Product entity)
        {
            await context.SaveChangesAsync();
        }
    }
}
