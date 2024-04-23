using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Entities;

public partial class BookLibraryContext : DbContext
{
    public BookLibraryContext()
    {
    }

    public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Base");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.HasKey(e => e.BookId);

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("title");
            entity.Property(e => e.Author)
                 .IsRequired()
                 .HasMaxLength(100)
                 .HasColumnName("author");
            entity.Property(e => e.TotalCopies).HasColumnName("total_copies");
            entity.Property(e => e.CopiesInUse).HasColumnName("copies_in_use");
            entity.Property(e => e.Category)
                  .HasMaxLength(50)
                  .HasColumnName("category");
            entity.Property(e => e.Type)
                  .HasMaxLength(50)
                  .HasColumnName("type");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("name");
            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100)
                  
                  .HasColumnName("email");
            entity.Property(e => e.Password)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("password");
        });
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
