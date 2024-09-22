using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using X.PagedList;

namespace EcommerceApp.Areas.UI.Controllers
{
    [Area("UI")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private const int PageSize = 8;

        public ProductController(ILogger<ProductController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int? page, string searchQuery, string categoryFilter)
        {
            int pageNumber = page ?? 1;

            // Fetch the list of categories from the database
            var categories = _unitOfWork.Category.GetAll(); // Replace with your actual data fetching logic

            // Fetch the product list including related entities (Category, ProductImages)
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");

            // Search filtering
            if (!string.IsNullOrEmpty(searchQuery))
            {
                productList = productList.Where(p => p.Name.Contains(searchQuery) || p.Author.Contains(searchQuery));
            }

            // Category filtering
            if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "all")
            {
                productList = productList.Where(p => p.Category.Name.ToLower() == categoryFilter.ToLower());
            }

            // Pagination
            var pagedList = productList.ToPagedList(pageNumber, PageSize);

            // Pass categories, search query, and category filter to the view
            ViewBag.Categories = categories;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.CategoryFilter = categoryFilter;

            return View(pagedList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Cart updated successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
