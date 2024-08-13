using EcommerceApp.Data;
using EcommerceApp.Models;
using EcommerceApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TodoListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TodoList> todoList = _context.TodoLists.ToList();
            return View(todoList);
        }


        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TodoList obj)
        {
            if (ModelState.IsValid)
            {
                _context.TodoLists.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


        public IActionResult Edit(Guid id) 
        {
            if (id == null)
            {
                //create
                return NotFound();
            }
            var todoFromDb = _context.TodoLists.Find(id);
            if (todoFromDb == null)
            {
                return NotFound();
            }
            return View(todoFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TodoList todoList)
        {
            if (ModelState.IsValid)
            {
                _context.TodoLists.Update(todoList);
                _context.SaveChanges();
                TempData["success"] = "Todo Updated Success";
                return RedirectToAction("Index");
            }
            return View(todoList);
        }

        //GET
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todoFromDb = _context.TodoLists.Find(id);
            if (todoFromDb == null)
            {
                return NotFound();
            }
            return View(todoFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid id)
        {
            var obj = _context.TodoLists.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.TodoLists.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "TodoList Deleted Successfully";
            return RedirectToAction("Index");
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var objFromDb = _context.TodoLists.ToList();
            return Json(new { data = objFromDb });
        }
        #endregion
    }
}
