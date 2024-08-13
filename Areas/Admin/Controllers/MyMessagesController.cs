using EcommerceApp.Data;
using EcommerceApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MyMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromDb = _context.MyMessages.ToList();
            return Json(new { data = objFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.MyMessages.FirstOrDefault(m => m.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _context.MyMessages.Remove(objFromDb);
            _context.SaveChanges();

            return Json(new { success = true, message = "Delete Successfully" });
        }

        #endregion

    }
}
