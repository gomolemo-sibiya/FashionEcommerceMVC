using Microsoft.EntityFrameworkCore;

namespace FashionEcommerceMVC.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeRepository> _logger;
        public HomeRepository(ApplicationDbContext db, ILogger<HomeRepository> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int categoryId = 0)
        {
            // Log the search term and category ID
            _logger.LogInformation($"Search term: {sTerm}, Category ID: {categoryId}");

            sTerm = sTerm.ToLower();
            IQueryable<Product> query = (from product in _db.Products
                                         join category in _db.Categories
                                         on product.CategoryId equals category.Id
                                         where string.IsNullOrWhiteSpace(sTerm) || (product != null && product.ProductName.ToLower().StartsWith(sTerm))
                                         select new Product
                                         {
                                             Id = product.Id,
                                             Image = product.Image,
                                             Brand = product.Brand,
                                             ProductName = product.ProductName,
                                             CategoryId = product.CategoryId,
                                             Price = product.Price,
                                             CategoryName = category.CategoryName
                                         });

            if (categoryId > 0)
            {
                query = query.Where(a => a.CategoryId == categoryId);
            }

            // Log the generated SQL query
            _logger.LogInformation($"SQL Query: {query.ToQueryString()}");

            return await query.ToListAsync();
        }

    }
}
