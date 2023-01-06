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
    public class ReceiptDetailRepository : IReceiptDetailRepository
    {
        private readonly TradeMarketDbContext context;
        public ReceiptDetailRepository(TradeMarketDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ReceiptDetail entity)
        {
            await context.AddAsync(entity);
        }

        public void Delete(ReceiptDetail entity)
        {
            context.ReceiptsDetails.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.ReceiptsDetails.RemoveRange(context.ReceiptsDetails.Where(x=> x.Id == id));
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllAsync()
        {
            return await context.ReceiptsDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await context.ReceiptsDetails.AsNoTracking()
                .Include(x => x.Receipt)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .ToListAsync();
        }

        public async Task<ReceiptDetail> GetByIdAsync(int id)
        {
            return await context.ReceiptsDetails.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async void Update(ReceiptDetail entity)
        {
            await context.SaveChangesAsync();
        }
    }
}
