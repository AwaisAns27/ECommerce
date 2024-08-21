using ECommerce.DAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.DAL.Services.Implimentation;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        #region Repo Declaration
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IProductRepo _productRepo;
        //private readonly ICategoryRepo _categoryRepo;

        //public ProductsController(IProductRepo productRepo, ICategoryRepo categoryRepo)
        //{
        //    _productRepo = productRepo;
        //    _categoryRepo = categoryRepo;
        //}
        #endregion

        #region Constructor
        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        #endregion

        #region List
        public IActionResult List()
        {
            IEnumerable<Product> products = _unitOfWork.IProductRepo.GetAll("Category");
            return View(products);
        }
        #endregion

        #region Upsert Get

        public IActionResult Upsert(int? id)
        {
            var categories = _unitOfWork.ICategoryRepo.GetAll().Where(c => c.IsActive);
            IEnumerable<SelectListItem> selectLists =
                       categories.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            ViewBag.categorySelectListItems = selectLists;

            if (id == null || id == 0)
            {
                return View("ProductForm", new Product());
            }
            else
            {
                var productInDb = _unitOfWork.IProductRepo.Get(c => c.Id == id);
                return View("ProductForm", productInDb);

            }
        }
        #endregion

        #region Upsert Post
        [HttpPost]
        public IActionResult Upsert(Product product, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string newFileName = "image" + Guid.NewGuid().ToString().Substring(0, 5) + fileExtension;
                    string webRootPath = _webHostEnvironment.WebRootPath;

                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        string oldImagePath = webRootPath + product.ImageUrl;

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    string finalDestination = webRootPath + @"\images\products";
                    // string finalDestination1 = Path.Combine(webRootPath, @"images\products");

                    using (FileStream fileStream = new FileStream(Path.Combine(finalDestination, newFileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    product.ImageUrl = @"\images\products\" + newFileName;

                }
                if (product.Id == 0 || product.Id == null)
                {
                    _unitOfWork.IProductRepo.Add(product);
                    _unitOfWork.Save();
                    product.CreatedOn = DateTime.Now;
                    TempData["Success"] = $"{product.Name} is Created";
                }
                else
                {
                    _unitOfWork.IProductRepo.Update(product);
                    _unitOfWork.Save();
                    product.UpdatedOn = DateTime.Now;

                    TempData["Success"] = $"{product.Name} is Updated";

                }


                return RedirectToAction("List");
            }
            return View("ProductForm", product);
        }
        #endregion

        #region Delete Get

        public IActionResult Delete(int id)
        {
            var productToBeDeleted = _unitOfWork.IProductRepo.Get(id);
            return View("Delete", productToBeDeleted);
        }
        #endregion

        #region Delete Post

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var productToBeDeleted = _productRepo.Get(id);
            //if (productToBeDeleted == null)
            //{
            //    return BadRequest();
            //}
            //else
            //    _productRepo.Delete(productToBeDeleted);
            //_productRepo.Save();

            //return RedirectToAction("List");

            var product = _unitOfWork.IProductRepo.Get(id);
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var webRootPath = _webHostEnvironment.ContentRootPath;
                string oldImagePath = webRootPath + product.ImageUrl;

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.IProductRepo.Remove(id);

            _unitOfWork.Save();
            product.DeletedOn = DateTime.Now;
            TempData["Success"] = $"{product.Name} is Deleted";

            return RedirectToAction("List");
        }
        #endregion

        #region Details
        public IActionResult Details(int id)
        {
            var products = _unitOfWork.IProductRepo.IncludeCat().
                            SingleOrDefault(c => c.Id == id);
            return View(products);

        }
        #endregion

    }
}
