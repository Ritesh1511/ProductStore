using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Services;

 
namespace ProductStore.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDBContext context;

        public ProductController(ApplicationDBContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList(); //var products = context.Products.FromSqlRaw("SELECT * FROM PRODUCTS").ToList();

            return View(products);
        }
    }
}

