﻿using Diana.DAL;
using Diana.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Diana.Controllers
{
	public class HomeController : Controller
	{
		AppDbContext _db;
		public HomeController(AppDbContext db)
		{
			_db = db;
		}
		public async Task<IActionResult> Index()
		{

			HomeVM vm = new HomeVM()
			{
				Products = await _db.Products.Include(p => p.ProductImages).ToListAsync(),

			};
			return View(vm);
		}
	}
}
