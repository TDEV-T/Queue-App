using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QueueService.Models;

public partial class QueueDbContext : DbContext
{
    public QueueDbContext(DbContextOptions<QueueDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<QueueState> QueueStates { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QueueState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("queue_states_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LastUpdatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Version).HasDefaultValue(1);


            entity.HasData(
                new QueueState
                {
                    Id = 1,
                    CurrentPrefix = 'A',
                    CurrentNumber = -1,
                    Version = 1,
                }
            );
        });


        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
