using Microsoft.AspNetCore.Mvc;

namespace FashionEcommerceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm = "", int categoryId = 0)
        {
            // Log the search term and category ID
            _logger.LogInformation($"Search term: {sterm}, Category ID: {categoryId}");

            IEnumerable<Product> products = await _homeRepository.GetProducts(sterm, categoryId);
            IEnumerable<Category> categories = await _homeRepository.Categories();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
                Products = products,
                Categories = categories,
                STerm = sterm,
                CategoryId = categoryId
            };
            return View(productModel);
        }
    }
}
