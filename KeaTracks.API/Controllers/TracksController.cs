using KeaTracks.API.Data;
using KeaTracks.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KeaTracks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TracksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TracksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tracks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Track>>> GetAll()
    {
        return await _context.Tracks.Take(50).ToListAsync();
    }
    
    // GET: api/tracks/search?region=Waikato
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Track>>> Search([FromQuery] string region)
    {
        return await _context.Tracks
            .Where(t => t.Region.Contains(region))
            .ToListAsync();
    }

    // GET: api/tracks/nearby?lat=...&lon=...
    [HttpGet("nearby")]
    public async Task<ActionResult<IEnumerable<Track>>> GetNearby(double lat, double lon)
    {
        // For now, just return top 20 tracks (we will add spatial logic later)
        return await _context.Tracks.Take(20).ToListAsync();
    }
}