using System.Data.Entity;
using System.Security.Principal;

public class WarehouseContext : DbContext
{
    public WarehouseContext() : base("name=WarehouseDB") { }
    public DbSet<Products> Products { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Histories> Histories { get; set; }
    public DbSet<Carts> Carts { get; set; }
    public DbSet<Transactions> Transactions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Discounts> Discounts { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Bảng Users
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(50);

        // Bảng Categories
        modelBuilder.Entity<Categories>()
            .Property(c => c.CategoryItem)
            .IsRequired()
            .HasMaxLength(50);

        // Bảng Products
        modelBuilder.Entity<Products>()
            .Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Products>()
            .Property(p => p.Quantity)
            .IsRequired();

        modelBuilder.Entity<Products>()
            .HasRequired(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryID);

        // Bảng History
        modelBuilder.Entity<Histories>()
            .HasRequired(h => h.Product)
            .WithMany(p => p.Histories)
            .HasForeignKey(h => h.ProductID);

        // Bảng Cart
        modelBuilder.Entity<Carts>()
            .HasRequired(c => c.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(c => c.ProductID);

        base.OnModelCreating(modelBuilder);
    }
}