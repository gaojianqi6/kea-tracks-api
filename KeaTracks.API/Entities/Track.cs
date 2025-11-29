using System.ComponentModel.DataAnnotations;

namespace KeaTracks.API.Entities;

public class Track
{
    // The Primary Key in the database
    [Key]
    public int Id { get; set; }

    // We store the external DOC ID to prevent duplicates when syncing
    [Required]
    public string DocAssetId { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)] // Limit text length to optimize database storage
    public string Name { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;

    // This can be very long (HTML content), so we don't put a limit
    public string Introduction { get; set; } = string.Empty;
    
    // This will be filled by AI later, so it is nullable (?)
    public string? AiSummary { get; set; }

    // Geospatial data (Simple coordinates for now)
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double LengthKm { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public bool IsDogFriendly { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}