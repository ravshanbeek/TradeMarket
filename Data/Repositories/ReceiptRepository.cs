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
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly TradeMarketDbContext context;
        public ReceiptRepository(TradeMarketDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Receipt entity)
        {
            await context.AddAsync(entity);
        }

        public async void Delete(Receipt entity)
        {
            context.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.RemoveRange(context.Receipts.Where(x => x.Id == id));
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await context.Receipts.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await context
                .Receipts
                .Include(x => x.Customer)
                .Include(x => x.ReceiptDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .ToListAsync();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await context.Receipts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<Receipt> GetByIdWithDetailsAsync(int id)
        {
            return await context.Receipts.AsNoTracking()
                .Include(x => x.Customer)
                .Include(x =>x.ReceiptDetails)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync(x =>x.Id ==id);
        }

        public async void Update(Receipt entity)
        {
            context.SaveChangesAsync();
        }
    }
}
