using EFCoreIntro.Models;
using EFCoreIntro.NortwndSample;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCoreIntro.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly NORTHWNDContext db;

    public HomeController(ILogger<HomeController> logger, NORTHWNDContext _db)
    {
      _logger = logger;
      db = _db;
    }

    public IActionResult Index()
    {


      //db deki product tablomdaki tüm verileri listeye ata
      List<Product> plist = db.Products.AsNoTracking().ToList();

      // change tracker mekanizmasını kapatır.
      // nesne update,delete,add işlemleri için ef tarafından izlenmez. nesne detached hale galiyor.
      // change tracker update,remove ve add methodları ile otomatik olarak tekrar açılıyor.

      //db.Products.Add(plist[0]);

      //ürünleri isme göre listele ve listeme ata
      List<Product> plist2 = db.Products.OrderBy(p => p.ProductName).ToList();

      //ürünleri isme göre tersten listele
      List<Product> plist3 = db.Products.OrderByDescending(p => p.ProductName).ToList();

      //ürünleri tersten listele ve ilk 5 ürünü yakalayıp listeme ata
      List<Product> plist4 = db.Products.OrderByDescending(p => p.ProductName).Take(5).ToList();

      //id si 5 olan ürün yakala
      Product prdc = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == 5);

      //id si 3 olan ürünün ADINI ver
      string ad = db.Products.FirstOrDefault(p => p.ProductId == 3).ProductName;

      //Kategori ID si 5 olan ürünlerin listesi
      List<Product> plist5 = db.Products.Where(p => p.CategoryId == 5).ToList();

      //Kategori ID si 3 ve Supplier ID si 2 olan ürünleri isme göre sırala ilk 5 imi al
      List<Product> plist6 = db.Products.Where(p => p.CategoryId == 3 && p.SupplierId == 2).OrderBy(p => p.ProductName).Take(5).ToList();

      //productname a harfi geçen ürünlerin listesi
      List<Product> plist7 = db.Products.Where(p => p.ProductName.Contains("a")).ToList();

      //product name a harfi ile BAŞLAYAN ürünlerin listesi
      List<Product> plist8 = db.Products.Where(p => p.ProductName.StartsWith("a")).ToList();

      //productname a harfi ile BİTEN ürünlerin listesi
      List<Product> plist9 = db.Products.Where(p => p.ProductName.EndsWith("a")).ToList();

      //Kategori Var Mi
      bool VarMi = db.Categories.Any();

      //Kategori IDsi 5 olan kategori var mı_
      bool VarMi2 = db.Categories.Any(c => c.CategoryId == 5);

      //İsminde 'ha' içeren ürün var mı? Küçük veya büyük harf farketmez
      bool VarMi3 = db.Products.Any(p => p.ProductName.Contains("ha"));

      //Ürün dizisine atar
      Product[] pdizi = db.Products.ToArray();


      //ürün sayısı
      int adet = db.Products.Count();

      //product tablomdaki QuantityPerUnit sayısı(tabloda bu kolon eğer null ise toplama eklenmeyecektir)
      int adet2 = db.Products.Count(p => p.QuantityPerUnit != null);



      //product tablomdaki ürün fiyatlarımın toplamı
      decimal? toplamfiyat = db.Products.Sum(p => p.UnitPrice);

      //en pahalı ürünüm
      decimal? pahaliurun = db.Products.Max(a => a.UnitPrice);

      //isme göre sıraladığında ilk 5 ürünü atla kalan ürünleri listele
      List<Product> plist10 = db.Products.OrderBy(p => p.ProductName).Skip(5).ToList();

      //isme göre sıraladığında ilk 5 ürünü atla 10 ürünü listele
      List<Product> plist11 = db.Products.OrderBy(p => p.ProductName).Skip(5).Take(10).ToList();

      //order tablosundaki shipcountry kolonuna distinct uygular(sql karşılığı "select distinct o.ShipCountry //from Orders o")
      List<string> countrylist = db.Orders.Select(a => a.ShipCountry).Distinct().ToList();

      //product tablosundaki primary key alanındaki değere eşdeğer product getirir(5 numaralı ProductID //değerini getirir)
      Product prdct = db.Products.Find(5);


      var data = db.Products.Select(a => new
      {
        name = a.ProductName,
        price = a.UnitPrice
      }).ToList();


      var data3 = db.Products.Include(x => x.Category).Include(x=> x.Supplier).ToList();


      // select sorguları ile bir nesnenin entitynin değeri başka bir nesneye attığımız yönteme dto yöntemi diyoruz
      var data2 = db.Products.Select(a => new ProductViewModel 
      {
        Name = a.ProductName,
        Price = a.UnitPrice
      }).ToList();


      return View();
    }

    public class ProductViewModel
    {
      public string Name { get; set; }
      public decimal? Price { get; set; }

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