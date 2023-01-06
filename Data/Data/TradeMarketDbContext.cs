using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class TradeMarketDbContext : DbContext
    {
        public TradeMarketDbContext() { }
        public TradeMarketDbContext(DbContextOptions<TradeMarketDbContext> options) : base(options) { }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Databasa=Market;Trusted_Connection=True;");
        //}
        
        
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
            
            /*
            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.Person)
                .WithOne();

            modelBuilder.Entity<Customer>()
                .HasMany<Receipt>(customer => customer.Receipts)
                   .WithOne(receipt => receipt.Customer)
                   .HasForeignKey(receipt => receipt.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne<ProductCategory>(
                    product => product.Category)
                .WithMany(productCategory => productCategory.Products)
                .HasForeignKey(product => product.ProductCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Receipt>()
                .HasMany<ReceiptDetail>(
                    receipt => receipt.ReceiptDetails)
                .WithOne(receiptDetail => receiptDetail.Receipt)
                .HasForeignKey(
                    receiptDetail => receiptDetail.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne<Product>(
                    receiptDetail => receiptDetail.Product)
                .WithMany(product => product.ReceiptDetails)
                .HasForeignKey(
                receiptDetail => receiptDetail.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            */
        }
    }
}
