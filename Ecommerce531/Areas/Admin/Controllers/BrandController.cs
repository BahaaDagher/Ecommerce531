using Ecommerce531.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce531.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly  ApplicationDbContext _context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var brands = _context.Brands.AsQueryable() ; 
            //filter 
            return View(brands.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand , IFormFile ImageFile)
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
                brand.Logo = fileName; 

            }
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c => c.Id == id); 
            if(brand  == null)
            {
                return RedirectToAction("NotFoundPage" , "Home");
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Update(Brand brand , IFormFile ImageFile)
        {
            var brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(b => b.Id == brand.Id);

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
                brand.Logo = fileName;

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", brandInDb.Logo);


                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath); 
                }
            }
            else
            {
                brand.Logo = brandInDb.Logo; 
            }
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(c => c.Id == id);
            if (brand == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\", brand.Logo);


            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
