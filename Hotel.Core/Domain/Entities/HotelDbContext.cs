using Microsoft.EntityFrameworkCore;
using Hotel.Core.Domain.Entities;

public class HotelDbContext : HotelScaffoldDbContext
{
    public HotelDbContext(
        DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrganizationMember>()
            .Property(member => member.Status)
            .HasConversion<string>()
            .HasMaxLength(9);
    }
}