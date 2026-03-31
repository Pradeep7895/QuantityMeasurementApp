using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuantityMeasurementApp.Model.Entities;

public class AppDbContext : IdentityDbContext
{
    public DbSet<QuantityHistoryRecord> QuantityHistory { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}