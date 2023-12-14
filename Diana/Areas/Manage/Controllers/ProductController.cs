using Diana.Areas.Manage.ViewModels;
using Diana.DAL;
using Diana.Helpers;
using Diana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Diana.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;

        AppDbContext _context { get; set; }
        public ProductController(AppDbContext context, IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> product = await _context.Products.Where(p=>p.IsDeleted==false)
                .Include(p=>p.Category)
                .Include(p=>p.ProductMaterials)
                .ThenInclude(p=>p.Material)
                .Include(p=>p.ProductColors)
                .ThenInclude(p=>p.Color)
                .Include(p=>p.ProductSizes)
                .ThenInclude(p=>p.Size)
                .Include(p=>p.ProductImages)               
                .ToListAsync();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();   
            ViewBag.Materials = await _context.Materials.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM createProductVM)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Materials = await _context.Materials.ToListAsync();
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Colors = await _context.Colors.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool resultCategory = await _context.Categories.AnyAsync(c=>c.Id==createProductVM.CategoryId);
            if(!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "No such category exist.");
                return View();
            }
            Product product = new Product()
            {
                Name = createProductVM.Name,
                Price = createProductVM.Price,
                Description = createProductVM.Description,
                CategoryId = createProductVM.CategoryId,
                ProductImages= new List<ProductImage>()
            };

            if(createProductVM.MaterialIds != null)
            {
                foreach (int materialId in createProductVM.MaterialIds)
                {
                    bool resultMaterial = await _context.Materials.AnyAsync(c => c.Id == materialId);
                    if (!resultMaterial)
                    {
                        ModelState.AddModelError("MaterialIds", "No such material exists.");
                        return View();
                    }
                    ProductMaterial productMaterial = new ProductMaterial()
                    {
                        Product = product,
                        MaterialId = materialId
                    };
                    _context.ProductMaterials.Add(productMaterial); 
                }
      
            }

            if (createProductVM.ColorIds != null)
            {
                foreach (int colorId in createProductVM.ColorIds)
                {
                    bool resultColor = await _context.Colors.AnyAsync(c => c.Id == colorId);
                    if (!resultColor)
                    {
                        ModelState.AddModelError("ColorIds", "No such color exists.");
                        return View();
                    }
                    ProductColor productColor = new ProductColor()
                    {
                        Product = product,
                        ColorId = colorId
                    };
                    _context.ProductColors.Add(productColor);
                }

            }

            if (createProductVM.SizeIds != null)
            {
                foreach (int sizeId in createProductVM.SizeIds)
                {
                    bool resultSize = await _context.Sizes.AnyAsync(c => c.Id == sizeId);
                    if (!resultSize)
                    {
                        ModelState.AddModelError("SizeIds", "No such size exists.");
                        return View();
                    }
                    ProductSize productSize = new ProductSize()
                    {
                        Product = product,
                        SizeId = sizeId
                    };
                    _context.ProductSizes.Add(productSize);
                }

            }



            if (createProductVM.Photos != null)
            {
                foreach (var item in createProductVM.Photos)
                {
                    if (!item.CheckType("image/"))
                    {
                        TempData["Error"] += $"{item.FileName} Type is not correct. \t ";

                        continue;
                    }
                    if (!item.CheckLength(3000))
                    {
                        TempData["Error"] += $"{item.FileName} Image is more than 3mb. \t";
                        continue;
                    }
                    ProductImage newPhoto = new ProductImage()
                    {
                        
                        ImgUrl = item.Upload(_env.WebRootPath, @"\Upload\Product\"),
                        Product = product,
                    };
                    product.ProductImages.Add(newPhoto);
                }
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update( )
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return View("Error");
            };
            product.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

