using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DHSHOP.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using logintest.Areas.Identity.Data;
using logintest.Models;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DHSHOP.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ImageController(ImageDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Image
        [HttpGet]
        public async Task<IActionResult> Index(
            string GPU, string Brand, string Submit,
            string search,
            string priceascending, string pricedescending, string nameascending, string namedescending, string Sortby, string df,
            string Category,
            int pg = 1,           
            string SearchText = ""
           )
        {
            ViewData["SearchForDetails"] = search;
            ViewData["PriceAscend"] = string.IsNullOrEmpty(priceascending) ? "PriceAsc" : "";
            ViewData["PriceDescend"] = string.IsNullOrEmpty(pricedescending) ? "PriceDes" : "";
            ViewData["NameAscend"] = string.IsNullOrEmpty(nameascending) ? "NameAsc" : "";
            ViewData["NameDescend"] = string.IsNullOrEmpty(namedescending) ? "NameDes" : "";
            ViewData["Default"] = string.IsNullOrEmpty(df) ? "DF" : "";


            var query = from x in _context.Images select x;

            switch (Sortby)
            {
                case "PriceAsc":
                    query = query.OrderByDescending(x => x.ProductPrice);
                    return View(await query.AsNoTracking().ToListAsync());
                   
                case "PriceDes":
                    query = query.OrderBy(x => x.ProductPrice);
                    return View(await query.AsNoTracking().ToListAsync());
                    
                case "NameAsc":
                    query = query.OrderByDescending(x => x.ProductName);
                    return View(await query.AsNoTracking().ToListAsync());
                    
                case "NameDes":
                    query = query.OrderBy(x => x.ProductName);
                    return View(await query.AsNoTracking().ToListAsync());
                    
                case "DF":
                    query = query.OrderBy(x => x.ProductId);
                    return View(await query.AsNoTracking().ToListAsync());
                    
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.ProductName.Contains(search));
                return View(await query.AsNoTracking().ToListAsync());
            }

            if (!string.IsNullOrEmpty(Category))
            {
                query = query.Where(x => x.Series.Contains(Category));
                return View(await query.AsNoTracking().ToListAsync());
            }

            if (!string.IsNullOrEmpty(Submit) && !string.IsNullOrEmpty(GPU) && !string.IsNullOrEmpty(Brand))
            {
                query = query.Where(x => x.Series.Contains(GPU));
                if (!string.IsNullOrEmpty(Brand))
                {
                    query = query.Where(x => x.ProductName.Contains(Brand));
                }
                return View(await query.AsNoTracking().ToListAsync());
            }
            else if (!string.IsNullOrEmpty(Submit) && !string.IsNullOrEmpty(GPU) && string.IsNullOrEmpty(Brand))
            {
                query = query.Where(x => x.Series.Contains(GPU));
                return View(await query.AsNoTracking().ToListAsync());
            }
            else if (!string.IsNullOrEmpty(Submit) && string.IsNullOrEmpty(GPU) && !string.IsNullOrEmpty(Brand))
            {
                query = query.Where(x => x.ProductName.Contains(Brand));
                return View(await query.AsNoTracking().ToListAsync());
            }



            List<ImageModel> products;

            if (SearchText != "" && SearchText != null)
            {
                products = _context.Images
                    .Where(p => p.ProductName.Contains(SearchText)).ToList();
            }
            else
            
            products = _context.Images.ToList();
            const int pageSize = 9;
            if (pg < 1)
                pg = 1;

            int recsCount = products.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            //this.ViewBag.PageSize = GetPageSizes(pageSize);

            //return View(await _context.Images.ToListAsync());

            return View(data);
        }

        // GET: Image/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDetail,ProductSlot,Series,Description,ProductSize,ProductRam,ProductGPU,ProductPOWER,ImageFile")] ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                //save img to wwwroot/img
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                imageModel.ProductImage = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageModel.ImageFile.CopyToAsync(fileStream);
                }
                //insert record
                _context.Add(imageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductId,ProductName,ProductPrice,ProductDetail,ProductSlot,Series,Description,ProductSize,ProductRam,ProductGPU,ProductPOWER,ImageFile")] ImageModel imageModel)
        {
            if (id != imageModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageModel);
        }

        // GET: Image/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var imageModel = await _context.Images.FindAsync(id);

            //delete image from wwwroot/Image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Image", imageModel.ProductImage);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record

            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageModelExists(string id)
        {
            return _context.Images.Any(e => e.ProductId == id);
        }

        //private List<SelectListItem> GetPageSizes (int selectedPageSize = 10)
        //{
        //    var pagesSizes = new List<SelectListItem>();

        //    if (selectedPageSize == 5)
        //        pagesSizes.Add(new SelectListItem("5", "5", true));
        //    else
        //        pagesSizes.Add(new SelectListItem("5", "5"));

        //    for (int lp =10; lp <=10; lp +=10)
        //    {
        //        if(lp == selectedPageSize)
        //        { pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString(), true)); }
        //        else
        //            pagesSizes.Add(new SelectListItem(lp.ToString(), lp.ToString()));
        //    }

        //    return pagesSizes;

        //}

        //public async Task<IActionResult> Index(string Category)
        //{
        //    var query = from x in _context.Images select x;

        //    if (!string.IsNullOrEmpty(Category))
        //    {
        //        query = query.Where(x => x.ProductName.Contains(Category) || x.ProductDetail.Contains(Category));
        //    }

        //    return View(await query.AsNoTracking().ToListAsync());
        //}
        
    }
}
