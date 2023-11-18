using Microsoft.EntityFrameworkCore;
using System;

public class TruckHelperDbContext : DbContext
{
	public DbSet<Truck> Trucks { get; set; }

	public DbSet<Load> Loads { get; set; }

	public TruckHelperDbContext(DbContextOptions<TruckHelperDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		
	}
}
