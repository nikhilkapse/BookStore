using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {

        // Now we have created ICategoryRepository and in that class we are accessing ApplicationDbContext so no need to access ApplicationDbContext here
        // Instead we need to access ICategoryRepository only
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        { 
            if(ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["Success"] = "Category added succssfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            } 

            Category? categoryFromDb = _categoryRepo.Get(U=> U.Id ==id); // find method  only works with the primary key
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); // this method will work even if the id not primary, we can do any modifications with this method
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); // Where can be used when there is a situation where we need to do some filtering or some calculations and then get firstOrDefault


            if (categoryFromDb == null)
            {
                return NotFound(); 
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj); 
                _categoryRepo.Save();
                TempData["Success"] = "Category edited succssfully";
                return RedirectToAction("Index"); 
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _categoryRepo.Get(U => U.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Category? obj = _categoryRepo.Get(U => U.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["Success"] = "Category deleted succssfully";
            return RedirectToAction("Index");

        }

    }
}
