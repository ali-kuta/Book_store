using firs_dot_net_project.Data;
using Microsoft.AspNetCore.Mvc;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;
using firs_dot_net_project.Repository;
using Microsoft.AspNetCore.Authorization;

namespace firs_dot_net_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork IUnitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            IUnitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> objCteforyList = IUnitOfWork.Category.GetAll().ToList();
            return View(objCteforyList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                IUnitOfWork.Category.Add(obj);
                IUnitOfWork.Category.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }






        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            } 
            Category? categoryFromDb = IUnitOfWork.Category.Get(u=>u.Id == Id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                IUnitOfWork.Category.Update(obj);
                IUnitOfWork.Category.Save();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }











        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = IUnitOfWork.Category.Get(u => u.Id == Id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? obj = IUnitOfWork.Category.Get(u => u.Id == Id);
            if(obj == null)
            {
                return NotFound();
            }
            IUnitOfWork.Category.Remove(obj);
            IUnitOfWork.Category.Save();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
