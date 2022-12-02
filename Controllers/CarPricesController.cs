using CodeChallenge.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers
{

    [Route("api/[controller]")]
    public class CarPricesController : ControllerBase
    {

        private readonly CarPriceContext _context;
        private readonly ISeedData _seedService;

        public CarPricesController(CarPriceContext context, ISeedData seedService )
        {
            _context = context;
            _seedService = seedService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CarPrice>> GetCarPrices()
        {
            var carPrices = _context.CarPrices;
            return Ok(carPrices);
        }

        [HttpGet("seed")]
        public IActionResult SeedCarPrices()
        {
            _seedService.SeedData();
            return Ok();
        }
        
        [HttpGet("{id}")]
        public ActionResult<CarPrice> GetCarPrice(int id)
        {
            CarPrice price = _context.CarPrices.Where(x => x.Id == id).First();

            return Ok(price);
        }
    }

    public class CarPrice
    {
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? ModelYear { get; set; }
        public decimal? Price { get; set; }
    }

    public class CarPriceContext : DbContext
    {
        public CarPriceContext(DbContextOptions<CarPriceContext> options)
            : base(options)
        {
        }

        public DbSet<CarPrice> CarPrices { get; set; } = null!;
    }
}
