using System.ComponentModel.DataAnnotations;

namespace AjaxProject.DAL
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="Ürün Adı Boş Bırakılamaz.")]
        public string? Name { get; set; }
		[Required(ErrorMessage = "Ürün Stoğu Boş Bırakılamaz.")]
		public int? Stock { get; set; }
		[Required(ErrorMessage = "Ürün Fiyatı Boş Bırakılamaz.")]
		public decimal? Price { get; set; }
		[Required(ErrorMessage = "Ürün Görseli Boş Bırakılamaz.")]
		public string? ImageUrl { get; set; }
	}
}
