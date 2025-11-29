using KeaTracks.API.Data;
using KeaTracks.API.Entities;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace KeaTracks.API.Services;

public class DocSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DocSyncService> _logger;

    // YOUR KEY from the curl command
    private const string ApiKey = "VZ0G27D3vc3pucd2jfVrH86rCDiriWw09YHB66ZU"; 

    public DocSyncService(IServiceProvider serviceProvider, ILogger<DocSyncService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🦜 KeaTracks: Service Started. Waiting 2 seconds...");
        await Task.Delay(2000); // Wait for DB to warm up
        await SyncTracks();
    }

    private async Task SyncTracks()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // === 0. THE FIX: Check if we already have tracks ===
            int existingCount = await db.Tracks.CountAsync();
            if (existingCount > 0)
            {
                _logger.LogInformation($"⏩ Data exists ({existingCount} tracks). Skipping DOC Sync to save time.");
                return; // EXIT FUNCTION IMMEDIATELY
            }

            var client = new HttpClient();

            // 1. ADD HEADERS (Exactly like your CURL command)
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                _logger.LogInformation("🦜 Fetching data from DOC...");

                var response = await client.GetAsync("https://api.doc.govt.nz/v1/tracks?limit=100");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"❌ API Failed: {response.StatusCode}");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                int count = 0;

                foreach (var item in doc.RootElement.EnumerateArray())
                {
                    // 2. PARSE DATA (Handling potential nulls)
                    var assetId = item.GetProperty("assetId").GetString();
                    if (string.IsNullOrEmpty(assetId)) continue;

                    // Check if track exists
                    var existingTrack = await db.Tracks.FirstOrDefaultAsync(t => t.DocAssetId == assetId);
                    if (existingTrack == null)
                    {
                        // Handle Region (It is an Array ["RegionName"])
                        string regionName = "NZ";
                        if (item.TryGetProperty("region", out var regions) && regions.ValueKind == JsonValueKind.Array)
                        {
                            // Get the first item in the array
                            foreach (var r in regions.EnumerateArray())
                            {
                                regionName = r.GetString() ?? "NZ";
                                break; 
                            }
                        }

                        // Handle Coordinates (NZTM to Lat/Long)
                        double nztmX = 0;
                        double nztmY = 0;
                        if (item.TryGetProperty("x", out var xVal)) nztmX = xVal.GetDouble();
                        if (item.TryGetProperty("y", out var yVal)) nztmY = yVal.GetDouble();
                        
                        // Convert to Lat/Long
                        var geo = NztmToLatLong(nztmX, nztmY);

                        var track = new Track
                        {
                            DocAssetId = assetId,
                            Name = item.GetProperty("name").GetString() ?? "Unknown Track",
                            Region = regionName,
                            // Sometimes 'introduction' is missing, use empty string
                            Introduction = item.TryGetProperty("introduction", out var i) ? i.GetString() ?? "" : "",
                            Latitude = geo.lat,
                            Longitude = geo.lon,
                            LengthKm = 5.0, // DOC API doesn't always give length in this endpoint, default to 5
                            Difficulty = "Medium",
                            IsDogFriendly = true
                        };

                        db.Tracks.Add(track);
                        count++;
                    }
                }

                await db.SaveChangesAsync();
                _logger.LogInformation($"✅ SUCCESS! Sync Complete. Added {count} new tracks.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Error parsing data: {ex.Message}");
            }
        }
    }

    // --- HELPER: CONVERT NZTM (DOC format) TO LAT/LONG (Mapbox format) ---
    // This is a simplified math conversion for New Zealand coordinates
    private (double lat, double lon) NztmToLatLong(double x, double y)
    {
        if (x == 0 || y == 0) return (-37.78, 175.27); // Default to Hamilton if missing

        // Approximate conversion for NZ (sufficient for MVP)
        // Ideally, we use a library like 'DotSpatial.Projections', but this keeps it simple.
        // A rough offset logic for NZ center:
        double lon = (x - 1800000) * 0.000012 + 173.0; 
        double lat = (y - 5500000) * 0.000009 - 41.0; 

        return (lat, lon);
    }
}