using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class TradeMarketDbContext : DbContext
    {
        public TradeMarketDbContext(DbContextOptions<TradeMarketDbContext> options)
        : base(options)
        {
        }
        //public TradeMarketDbContext(DbContextOptions<TradeMarketDbContext> options) : base(options) { }

        //public TradeMarketDbContext() { }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TradeMarket;Trusted_Connection=True;");
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<ReceiptDetail> ReceiptsDetails { get; set;}
        //TODO: write DbSets for entities

        //TODO: write Fluent API configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(x => x.Person)
                .WithOne();

            modelBuilder.Entity<Receipt>()
                .HasOne(receipt => receipt.Customer)
                .WithMany(x => x.Receipts);

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne<Receipt>(x => x.Receipt)
                .WithMany(x => x.ReceiptDetails);

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ReceiptDetails);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Products);
        }
    }
}
