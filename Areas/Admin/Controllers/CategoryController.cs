using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class CategoryController(IUnitOfWork unitOfWork) : Controller
	{
       
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Create() 
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
			if (ModelState.IsValid)
			{
				unitOfWork.Category.Add(obj);
				unitOfWork.Save();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		
		}

		//GET
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var categoryFromDb = unitOfWork.Category
				.Get(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{ 
		   if(ModelState.IsValid)
			{
				unitOfWork.Category.Update(obj);
				unitOfWork.Save();
				TempData["success"] = "Category updated successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//GET
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var categoryFromDb = unitOfWork.Category
				.Get(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(int? id)
		{
			var obj = unitOfWork.Category.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			unitOfWork.Category.Remove(obj);
			unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			var objFromDb = unitOfWork.Category.GetAll();
			return Json(new { data = objFromDb });
		}

		[HttpDelete]
		public IActionResult DeleteCategory(int? id)
		{
			var objFromDb = unitOfWork.Category.Get
				(c => c.Id == id);
			if (objFromDb == null)
			{
				TempData["Error"] = "Error deleting Category";
				return Json(new { success = false, message = "Error While Deleting" });
			}
			unitOfWork.Category.Remove(objFromDb);
			unitOfWork.Save();
			TempData["success"] = "Category successfully deleted";
			return Json(new { success = true, message = "Delete sucessful" });		  
		}

		#endregion
	}
}
