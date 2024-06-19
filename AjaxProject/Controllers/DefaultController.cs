using AjaxProject.DAL;
using AjaxProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AjaxProject.Controllers
{
	public class DefaultController : Controller
	{
		private readonly Context _context;

		public DefaultController(Context context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ProductList()
		{
			var jsonValues = JsonConvert.SerializeObject(_context.Products.ToList());
			return Json(jsonValues);
		}
		[HttpPost]
		public IActionResult CreateProduct(Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Products.Add(product);
				_context.SaveChanges();
				JsonConvert.SerializeObject(product);
				return NoContent();
			}
			else
			{
				var errors = ModelState.Values.SelectMany(x => x.Errors)
					.Select(y => y.ErrorMessage)
					.ToList();
				return BadRequest(new { errors });
			}

		}
		public IActionResult DeleteProduct(int id)
		{
			if (id <= 0)
			{
				return BadRequest(new { error = "Alanlar boş bırakılamaz." });
			}
			var value = _context.Products.FirstOrDefault(x => x.ProductId == id);
			if (value != null)
			{
				_context.Products.Remove(value);
				_context.SaveChanges();
				return NoContent();
			}
			else
			{
				return BadRequest(new { error = "Bu ID'ye ait veri bulunamadı." });
			}
		}
		public IActionResult GetProduct(int id)
		{
			if (id <= 0)
			{
				return BadRequest(new { error = "Alanlar boş bırakılamaz." });
			}
			else
			{
				var value = _context.Products.FirstOrDefault(x => x.ProductId == id);
				if (value == null)
				{
					return BadRequest(new { error = "Bu ID'ye ait listelenecek veri bulunamadı" });
				}
				return Json(JsonConvert.SerializeObject(value));
			}
		}
		[HttpPost]
		public IActionResult UpdateProduct(Product product)
		{
			if (!ModelState.IsValid)
			{
				//var errors = ModelState.Values.SelectMany(x => x.Errors)
				//   .Select(y => y.ErrorMessage)
				//   .ToList();
				return BadRequest(new { errors="Alanlar Boş Bırakılamaz" });
			}
			var value = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
			if (value == null)
			{
				return BadRequest(new { errors = "Girilen ID'ye ait ürün bulunamadı." });
			}
			value.Name = product.Name;
			value.Price = product.Price;
			value.Stock = product.Stock;
			value.ImageUrl = product.ImageUrl;
			_context.SaveChanges();
			return NoContent();
		}

	}
}
