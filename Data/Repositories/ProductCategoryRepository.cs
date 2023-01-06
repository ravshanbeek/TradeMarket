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
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly TradeMarketDbContext context;
        public ProductCategoryRepository(TradeMarketDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ProductCategory entity)
        {
            await context.AddAsync(entity);
        }

        public void Delete(ProductCategory entity)
        {
            context.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.ProductCategories.RemoveRange(context.ProductCategories.Where(category => category.Id == id));
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await context.ProductCategories.AsNoTracking().ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await context.ProductCategories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id);
        }

        public async void Update(ProductCategory entity)
        {
            await context.SaveChangesAsync();
        }
    }
}
