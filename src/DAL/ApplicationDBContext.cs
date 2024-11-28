using CartService.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Metadata;

namespace CartService.DAL;

public class ApplicationDBContext : DbContext
{

    public DbSet<Cart> Carts { get; set; }
    public DbSet<Item> Items { get; set; }


    public ApplicationDBContext(DbContextOptions options) : base(options) { }


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    string conn = "mongodb://localhost:27017";


    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseMongoDB(conn, "CartService");
    //    }
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Cart>()
           .ToCollection("Carts");

        modelBuilder.Entity<Item>()
            .ToCollection("Items");

        //modelBuilder.Entity<Cart>()
        //    .HasMany(c => c.Items)
        //    .WithOne()
        //    .OnDelete(DeleteBehavior.Cascade);


       

        base.OnModelCreating(modelBuilder);

    }



}
