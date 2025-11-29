using KeaTracks.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeaTracks.API.Data;

// This class acts as the "Session" with the Database.
public class AppDbContext : DbContext
{
    // The constructor passes configuration (like connection strings) to the base class
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This line tells .NET: "Create a table called 'Tracks' based on the Track class"
    public DbSet<Track> Tracks { get; set; }
}