using EFCoreIntro.Data.Contexts;
using EFCoreIntro.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreIntro.Controllers
{
  public class CategoryController : Controller
  {
    private AppDbContext _dbContext;

    public CategoryController(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public IActionResult Create()
    {
      var entity = new Category
      {
        Name = "Category1"
      };

      _dbContext.Categories.Add(entity);
       int result = _dbContext.SaveChanges();

      ViewBag.Result = result > 0 ? "Eklendi" : "Başarısız";


      return View();
    }

    public IActionResult Update()
    {

      var entity = _dbContext.Categories.Find(1);
      entity.Name = "Category11";

      int result = _dbContext.SaveChanges();

      ViewBag.Result = result > 0 ? "Güncellendi" : "Başarısız";

      return View();
    }

    public IActionResult Delete()
    {

      var entity = _dbContext.Categories.Find(1);
     

      _dbContext.Categories.Remove(entity);
       int result = _dbContext.SaveChanges();

      ViewBag.Result = result > 0 ? "Silindi" : "Başarısız";

      return View();
    }
  }
}
