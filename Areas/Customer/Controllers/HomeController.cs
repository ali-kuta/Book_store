using System.Diagnostics;
using firs_dot_net_project.Models;
using firs_dot_net_project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace firs_dot_net_project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _IUnitOfWork;

        public HomeController(ILogger<HomeController> logger , IUnitOfWork IUnitOfWork)
        {
            _logger = logger;
            _IUnitOfWork = IUnitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _IUnitOfWork.Product.GetAll(Categoryproparty: "categories");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            Product Product = _IUnitOfWork.Product.Get(u=>u.Id==id,Categoryproparty: "categories");
            return View(Product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
