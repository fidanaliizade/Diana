//using Diana.DAL;
//using Microsoft.AspNetCore.Mvc;

//namespace Diana.Controllers
//{
//	public class ShopSingleController : Controller
//	{
//		AppDbContext _db;

//		public ShopSingleController(AppDbContext db)
//		{
//			_db = db;
//		}
//		public IActionResult Detail(int? id)
//		{
//			Product product = _db.Products
//				  .Where(p => p.IsDeleted == false)
//				  .Include(p => p.Category)
//				  .Include(p => p.ProductImages)
//				  .Include(p => p.ProductMaterials)
//				  .Include(p => p.ProductColors)
//				  .Include(p => p.ProductSizes).ThenInclude(ps => ps.s)
//				  .FirstOrDefault(product => product.Id == id);
//			if (product == null)
//			{
//				return NotFound();
//			}
//			DetailVM detailVM = new DetailVM()
//			{
//				Product = product,
//				Products = _db.Products.Include(p => p.ProductImages).Include(p => p.Category).Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id).ToList()
//			};

//			return View(detailVM);
//		}
//	}
//}
