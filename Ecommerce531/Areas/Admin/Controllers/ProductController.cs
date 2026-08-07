using Ecommerce531.DataAccess;
using Ecommerce531.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static System.Net.WebRequestMethods;

namespace Ecommerce531.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly  ApplicationDbContext _context = new ApplicationDbContext();
        public IActionResult Index(ProcutFilterVM filter)
        {
            var products = _context.Products.AsQueryable() ;
            products = products.Include(p => p.Category).Include(p => p.Brand);
            // filter 
            if (filter.ProductName != null)
            {
                products = products.Where(p => p.Name.Contains(filter.ProductName));
                ViewBag.ProductName = filter.ProductName;
            }
            if (filter.MinPrice > 0)
            {
                products = products.Where(p => p.Price - (p.Price * p.Discount / 100) >= filter.MinPrice);
                ViewBag.MinPrice = filter.MinPrice;

            }
            if (filter.MaxPrice > 0)
            {
                products = products.Where(p => p.Price - (p.Price * p.Discount / 100) >= filter.MaxPrice);
                ViewBag.MaxPrice = filter.MaxPrice;

            }
            if (filter.CategoryId > 0)
            {
                products = products.Where(p => p.CategoryId == filter.CategoryId);
                ViewBag.CategoryId = filter.CategoryId;

            }
            if (filter.BrandId > 0)
            {
                products = products.Where(p => p.BrandId == filter.BrandId);
                ViewBag.BrandId = filter.BrandId;

            }
            if (filter.IsLowQuantity)
            {
                products = products.OrderBy(p => p.Quantity);
                ViewBag.IsLowQuantity = filter.IsLowQuantity;

            }
            // pagination 
            ViewBag.Categories = _context.Categories;
            ViewData["Categories"] = _context.Categories; 
            ViewBag.Brands = _context.Brands;

            ViewBag.TotalPages = (int)Math.Ceiling(products.Count() / 8.0);
            ViewBag.CurrentPage = filter.Page;
            products = products.Skip((filter.Page - 1) * 8).Take(8);
            return View(products.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList(); 
            var brands = _context.Brands.ToList(); 
            return View( new ProductVM()
            {
                Categories = categories,
                Brands = brands
            });
        }
        [HttpPost]
        public IActionResult Create(Product product , IFormFile ImageFile , List<IFormFile> SubImageFiles , List<string> Colors )
        {
            if(ImageFile != null)
            {
                // 1.png // 1221-g12.png
                //var fileName = Guid.NewGuid().ToString()+Path.GetExtension(ImageFile.FileName); 
                // adding in wwwroot
                var fileName = Guid.NewGuid().ToString()+"-"+ImageFile.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot\\images\\" , fileName);
                using (var stream = System.IO.File.Create(filePath)) 
                {
                    ImageFile.CopyTo(stream); 
                }
                product.MainImg = fileName; 

            }
            var SavedProduct = _context.Products.Add(product);
            _context.SaveChanges();

            // product SubImages 
            if (SubImageFiles != null && SubImageFiles.Count>0 )
            {
                foreach(var image in SubImageFiles)
                {
                    if (image != null)
                    {
                        var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\product_sub_images\\", fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            image.CopyTo(stream);
                        }
                        _context.ProductSubImages.Add(new ProductSubImage()
                        {
                            ProductId = SavedProduct.Entity.Id, 
                            Img = fileName 
                        });  

                    }
                }
            }

            // Product Colors 
            if (Colors != null && Colors.Count > 0)
            {
                foreach(var color in Colors)
                {
                    _context.ProductColors.Add(new ProductColor()
                    {
                        ProductId = SavedProduct.Entity.Id , 
                        Color = color
                    }); 

                }
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id); 
            if(product  == null)
            {
                return RedirectToAction("NotFoundPage" , "Home");
            }
            return View(new ProductVM()
            {
                Categories = _context.Categories.ToList() , 
                Brands = _context.Brands.ToList() ,
                ProductSubImages = _context.ProductSubImages.Where(ps=>ps.ProductId == id).ToList() , 
                ProductColors = _context.ProductColors.Where(pc=>pc.ProductId == id).ToList()  , 
                Product = product
            }); 
        }
        [HttpPost]
        public IActionResult Update(Product product , IFormFile ImageFile , List<IFormFile> SubImageFiles , List<string> Colors)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(b => b.Id == product.Id);

            if (ImageFile != null)
            {
                // 1.png // 1221-g12.png
                //var fileName = Guid.NewGuid().ToString()+Path.GetExtension(ImageFile.FileName); 
                // adding in wwwroot
                var fileName = Guid.NewGuid().ToString() + "-" + ImageFile.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    ImageFile.CopyTo(stream);
                }
                product.MainImg = fileName;

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", productInDb.MainImg);


                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath); 
                }
            }
            else
            {
                product.MainImg = productInDb.MainImg; 
            }
            _context.Products.Update(product);
            _context.SaveChanges();

            // product SubImages 
            if (SubImageFiles != null && SubImageFiles.Count > 0)
            {
                var oldSubImages = _context.ProductSubImages.Where(ps => ps.ProductId == productInDb.Id);
                //remove from Db 
                _context.ProductSubImages.RemoveRange(oldSubImages); 
                // remove from wwwroot
                foreach(var item in oldSubImages)
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\product_sub_images\\", item.Img);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                foreach (var image in SubImageFiles)
                {
                    if (image != null)
                    {
                        //save in WWWroot
                        var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\product_sub_images\\", fileName);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            image.CopyTo(stream);
                        }
                        //save in DB
                        _context.ProductSubImages.Add(new ProductSubImage()
                        {
                            ProductId = productInDb.Id,
                            Img = fileName
                        });

                    }
                }
            }

            // Product Colors 
            if (Colors != null && Colors.Count > 0)
            {
                // remove from db
                var oldColors = _context.ProductColors.Where(pc => pc.ProductId == productInDb.Id);
                _context.ProductColors.RemoveRange(oldColors);
                // save in db
                foreach (var color in Colors)
                {
                    _context.ProductColors.Add(new ProductColor()
                    {
                        ProductId = productInDb.Id,
                        Color = color
                    });

                }
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", product.MainImg);


            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            var oldSubImages = _context.ProductSubImages.Where(ps => ps.ProductId == product.Id);
            // remove from wwwroot
            foreach (var item in oldSubImages)
            {
                var oldFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\product_sub_images\\", item.Img);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }

            _context.Products.Remove(product);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
