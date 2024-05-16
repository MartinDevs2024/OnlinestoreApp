using EcommerceApp.Data;
using EcommerceApp.Data.Repository.IRepository;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace EcommerceApp.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
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
				_unitOfWork.Category.Add(obj);
				_unitOfWork.Save();
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

			var categoryFromDb = _unitOfWork.Category
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
				_unitOfWork.Category.Update(obj);
				_unitOfWork.Save();
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

			var categoryFromDb = _unitOfWork.Category
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
			var obj = _unitOfWork.Category.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Category.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}

		#region
		[HttpGet]
		public IActionResult GetAll()
		{
			var objFromDb = _unitOfWork.Category.GetAll();
			return Json(new { data = objFromDb });
		}

		[HttpDelete]
		public IActionResult DeleteCategory(int? id)
		{
			var objFromDb = _unitOfWork.Category.Get
				(c => c.Id == id);
			if (objFromDb == null)
			{
				TempData["Error"] = "Error deleting Category";
				return Json(new { success = false, message = "Error While Deleting" });
			}
			_unitOfWork.Category.Remove(objFromDb);
			_unitOfWork.Save();
			TempData["success"] = "Category successfully deleted";
			return Json(new { success = true, message = "Delete sucessful" });		  
		}

		#endregion
	}
}
