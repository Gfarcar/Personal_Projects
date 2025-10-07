using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EzFitAPI.Models;

public partial class EzFitContext : DbContext
{
    public EzFitContext()
    {
    }

    public EzFitContext(DbContextOptions<EzFitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<IngedientesComida> IngedientesComida { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IngedientesComida>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NutricionComida");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CaloricValue).HasColumnName("Caloric_Value");
            entity.Property(e => e.Food).HasColumnName("food");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
