using ECommerce.DAL.Services.Interfaces;
using ECommerce.Model;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICategoryRepo _catogeryRepo;
        //public CategoryController(ICategoryRepo catogeryRepo)
        //{
        //    this._catogeryRepo = catogeryRepo;
        //}
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult List()
        {
            var catogeries = _unitOfWork.ICategoryRepo.GetAll().Where(c => c.IsActive == true);
            return View(catogeries);
        }

        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                return View("CategoryForm", new Category());
            }
            else
            {
                var categoryinDb = _unitOfWork.ICategoryRepo.Get(c => c.Id == id);
                return View("CategoryForm", categoryinDb);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0 || category.Id == null)
                {
                    _unitOfWork.ICategoryRepo.Add(category);
                    _unitOfWork.Save();
                    TempData["Success"] = $"{category.Name} Category Added";
                }
                else
                {
                    _unitOfWork.ICategoryRepo.Update(category);
                    _unitOfWork.Save();
                    TempData["Success"] = $"{category.Name} Category Updated";
                }

                return RedirectToAction("List");

            }

            return View("CategoryForm", category);
        }
        public IActionResult Delete(int id)
        {
            return View(_unitOfWork.ICategoryRepo.Get(id));
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var deletedCategory = _unitOfWork.ICategoryRepo.Get(id);

            _unitOfWork.ICategoryRepo.Remove(deletedCategory.Id);

            _unitOfWork.Save();
            TempData["Success"] = $"{deletedCategory.Name} Category is Deleted";

            return RedirectToAction("List");
        }
        public IActionResult Details(int id)
        {
            var details = _unitOfWork.ICategoryRepo.GetAll().SingleOrDefault(x => x.Id == id);
            return View(details);
        }
    }
}
