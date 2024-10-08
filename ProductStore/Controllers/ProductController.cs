using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
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
            var products = context.Products.OrderBy(p => p.Id).ToList();

            return View(products);
        }

        public IActionResult Create()
        {
 
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {

            if(productDto.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "Image is Required");
            }
            if(!ModelState.IsValid)
            {
                return View();
            }
            // save the product

            string newFilename = DateTime.Now.ToString("yyyyMMddHHmmss");
            newFilename = Path.GetFileNameWithoutExtension(productDto.ImageFile?.FileName) + //name of image
                          newFilename //time from above
                         + Path.GetExtension(productDto.ImageFile?.FileName); //extension of image
            Console.WriteLine($"File name created - {newFilename}");

            string fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products", newFilename);
            Console.WriteLine($"File path created - {fullImagePath}");


            // Save the file
            using (var filestream = new FileStream(fullImagePath, FileMode.Create))
            {
                productDto.ImageFile!.CopyTo(filestream);
            }

            // Optionally, log the file path or other actions
            Console.WriteLine($"File saved as: {newFilename}");

            //saving in the database
            Products product = new Products()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageFileName = newFilename,
                CreatedAt = DateTime.Now
            };

            context.Products.Add(product);
            context.SaveChanges();

            Console.WriteLine("Product list is updated");
            return RedirectToAction("Index", "Product");
        }



    }
}

