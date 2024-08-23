using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Troja.Models;

namespace Troja.Data;

public class AppDbContext : DbContext
{
    //Tablas en la base de datos
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<AuthorsBooksRelation> AuthorsBooksRelation { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //Funcion de la relacion entre tablas de la base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura la columna ID como autoincremental
        modelBuilder.Entity<User>()
            .Property(u => u.UserId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Book>()
            .Property(b => b.BookId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Author>()
            .Property(a => a.AuthorId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Loan>()
            .Property(l => l.LoanId)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<AuthorsBooksRelation>()
            .Property(ab => ab.Id)
            .ValueGeneratedOnAdd();
        // Configurar claves primarias
        modelBuilder.Entity<Loan>().HasKey(l => l.LoanId);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<Book>().HasKey(b => b.BookId);
        modelBuilder.Entity<Author>().HasKey(a => a.AuthorId);

        // Configurar relaciones
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.User)
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}