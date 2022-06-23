using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index"); // same controller
                                                  // return RedirectToAction("Index", "ControllerName"); // different controller
            }

            TempData["success"] = "CoverType created successfully";

            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index"); // same controller
                                                  // return RedirectToAction("Index", "ControllerName"); // different controller
            }
            TempData["success"] = "CoverType updated successfully";
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index"); // same controller
                                              // return RedirectToAction("Index", "ControllerName"); // different controller
        }







    }
}
