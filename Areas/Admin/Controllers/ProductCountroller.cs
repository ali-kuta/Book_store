using firs_dot_net_project.Data;
using Microsoft.AspNetCore.Mvc;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;
using firs_dot_net_project.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using firs_dot_net_project.ViewModels;

namespace firs_dot_net_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork IUnitOfWork;
        private readonly IWebHostEnvironment _webhostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webhostEnvironment)
        {
            IUnitOfWork = db;
            _webhostEnvironment = webhostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objCteforyList = IUnitOfWork.Product.GetAll(Categoryproparty:"categories").ToList();
            IEnumerable<SelectListItem> CategoryDropDown = IUnitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(objCteforyList);
        }

        public IActionResult Upsert(int? id)
        {
            
            IEnumerable<SelectListItem> CategoryDropDown = IUnitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            //ViewBag.Categorylist = CategoryDropDown;
            //ViewData["Categorylist"] = CategoryDropDown; just anather wahy but is the same and stor the data in the momory in the same plase so you need tp not name the key the sasme name
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryDropDown
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                //Edit Product
                productVM.Product = IUnitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM , IFormFile? file)
        { 

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webhostEnvironment.WebRootPath;
                if(file != null)
                {
                    // If a file is uploaded, we need to save it
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    
                    if(!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        // If the product already has an image, we need to delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    //if (!Directory.Exists(productPath))
                    //{
                    //    Directory.CreateDirectory(productPath); 
                    //}
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (ProductVM.Product.Id == 0)
                {
                    // If the product is new, we need to add it
                    IUnitOfWork.Product.Add(ProductVM.Product);
                }
                else
                {
                    // If the product already exists, we need to update it
                    IUnitOfWork.Product.Update(ProductVM.Product);

                }
                
                IUnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            else
            {
                // If the model state is invalid, we need to repopulate the CategoryList
                ProductVM.CategoryList = IUnitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(ProductVM);
            }
            
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
