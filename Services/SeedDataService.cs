using CodeChallenge.Controllers;
using CodeChallenge.Interfaces;

namespace CodeChallenge.Services
{
    public class SeedDataService : ISeedData
    {
        private readonly CarPriceContext _context;
        public SeedDataService(CarPriceContext context)
        {
            _context = context;
        }
        public void SeedData()
        {
            var lines = File.ReadLines("carPrices.csv").ToArray();
            for(int i = 1; i < lines.Length; i++)
            {
                string[] vals = lines[i].Split(',');
                _context.CarPrices.Add(new CarPrice
                {
                    Id = Convert.ToInt32(vals[0]),
                    Make = vals[1],
                    Model = vals[2],
                    ModelYear = vals[3],
                    Price = Convert.ToDecimal(vals[4]),
                });
                
            }
            _context.SaveChanges();
        }
    }
}
