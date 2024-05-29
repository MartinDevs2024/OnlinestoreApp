using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.UI.Controllers
{
    [Area("UI")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(ILogger<ProductController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");
            return View(productList);
        }
    }
}
