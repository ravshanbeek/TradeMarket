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
    public class PersonRepository : IPersonRepository
    {
        private readonly TradeMarketDbContext context;
        public PersonRepository(TradeMarketDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Person entity)
        {
            context.AddAsync(entity);
        }

        public void Delete(Person entity)
        {
            context.Persons.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Persons.RemoveRange(context.Persons.Where(person => person.Id == id));
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await context.Persons.AsNoTracking().ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await context.Persons.AsNoTracking().FirstOrDefaultAsync(person => person.Id == id);
        }

        public void Update(Person entity)
        {
            context.SaveChangesAsync();
        }
    }
}
