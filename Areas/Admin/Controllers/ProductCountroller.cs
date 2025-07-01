using firs_dot_net_project.Data;
using Microsoft.AspNetCore.Mvc;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;
using firs_dot_net_project.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firs_dot_net_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork IUnitOfWork;
        public ProductController(IUnitOfWork db)
        {
            IUnitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Product> objCteforyList = IUnitOfWork.Product.GetAll().ToList();
            IEnumerable<SelectListItem> CategoryDropDown = IUnitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(objCteforyList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryDropDown = IUnitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ViewBag.Categorylist = CategoryDropDown;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                IUnitOfWork.Product.Add(obj);
                IUnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }






        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = IUnitOfWork.Product.Get(u => u.Id == Id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                IUnitOfWork.Product.Update(obj);
                IUnitOfWork.Save();
                TempData["success"] = "Product Updated successfully";
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
            Product? productFromDb = IUnitOfWork.Product.Get(u => u.Id == Id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? obj = IUnitOfWork.Product.Get(u => u.Id == Id);
            if (obj == null)
            {
                return NotFound();
            }
            IUnitOfWork.Product.Remove(obj);
            IUnitOfWork.Save();
            TempData["success"] = "Product Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
