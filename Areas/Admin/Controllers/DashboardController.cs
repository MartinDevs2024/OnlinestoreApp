using EcommerceApp.Data;
using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;
using EcommerceApp.Models.ViewModels;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
     [Authorize(Roles = SD.Role_Admin)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
   
        public DashboardController(ApplicationDbContext context,
            IUnitOfWork unitOfWork)
        {
            _context = context;
           
        }
        public async Task<ActionResult> Index()
        {
            var model = new DashboardViewModel();

            model.Products = _context.Products.ToList(); ; 
            model.TodoLists = _context.TodoLists.ToList();
            model.MyMessages = _context.MyMessages.ToList();
            model.Posts = _context.Posts.ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> PlotlyBarChart()
        {
            var products = await _context.Products.ToListAsync();

            var plotData = new List<Object>
            {
                new
                {
                    x = products.Select(p=>p.Name).ToArray(),
                    y = products.Select(p=>p.Price).ToArray(),
                    type = "bar",
                    name = "Price"
                },
                new
                {
                    x = products.Select(p => p.Name).ToArray(),
                    y = products.Select(p => p.ListPrice).ToArray(),
                    type = "bar",
                    name = "List Price"
                }
            };

            return Json(plotData);
        }
    }
}
