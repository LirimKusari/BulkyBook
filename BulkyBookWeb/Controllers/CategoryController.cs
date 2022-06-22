using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();
                return RedirectToAction("Index"); // same controller
                                                  // return RedirectToAction("Index", "ControllerName"); // different controller
            }

            TempData["success"] = "Category created successfully";

            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
             var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            //var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            
            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                return RedirectToAction("Index"); // same controller
                                                  // return RedirectToAction("Index", "ControllerName"); // different controller
            }
            TempData["success"] = "Category updated successfully";
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            //categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            //var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index"); // same controller
                                              // return RedirectToAction("Index", "ControllerName"); // different controller
        }






    }
}
