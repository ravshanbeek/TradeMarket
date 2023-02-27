using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Data.Data
{
    //TODO: create class UnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TradeMarketDbContext context;
        private readonly ICustomerRepository customerRepository;
        private readonly IPersonRepository personRepository;
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IReceiptRepository receiptRepository;
        private readonly IProductRepository productRepository;
        private readonly IReceiptDetailRepository receiptDetailRepository;

        public UnitOfWork(TradeMarketDbContext tradeMarketDbContext)
        {
            this.context = tradeMarketDbContext;
        }
        public ICustomerRepository CustomerRepository
        {
           get { return customerRepository ?? new CustomerRepository(context);  }
        }

        public IPersonRepository PersonRepository
        {
            get { return personRepository ?? new PersonRepository(context); }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                return productRepository ?? new ProductRepository(context);
            }
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get { return productCategoryRepository ?? new ProductCategoryRepository(context); }
        }

        public IReceiptRepository ReceiptRepository
        {
            get
            {
                return receiptRepository?? new ReceiptRepository(context);
            }
        }

        public IReceiptDetailRepository ReceiptDetailRepository
        {
            get { return receiptDetailRepository ?? new ReceiptDetailRepository(context); }
        }
        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
