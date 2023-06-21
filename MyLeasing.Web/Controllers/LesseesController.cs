using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {
        private readonly DataContext _context;

        public LesseesController(DataContext context)
        {
            _context = context;
        }

        // GET: Lessees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lessees.ToListAsync());
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel lesseeViewModel)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (lesseeViewModel.ImageFile != null && lesseeViewModel.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\lessees", file);

                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        await lesseeViewModel.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/lessees/{file}";
                }

                var lessee = this.ToLessee(lesseeViewModel, path);

                    _context.Add(lessee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lesseeViewModel);
        }

        private Lessee ToLessee(LesseeViewModel lesseeViewModel, string path)
        {
            return new Lessee
            {
                Id = lesseeViewModel.Id,
                FirstName = lesseeViewModel.FirstName,
                LastName = lesseeViewModel.LastName,
                FixedPhone = lesseeViewModel.FixedPhone,
                CellPhone = lesseeViewModel.CellPhone,
                Address = lesseeViewModel.Address,
                Photo = path,
                User = lesseeViewModel.User,
                Document = lesseeViewModel.Document,
            };
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessees.FindAsync(id);
            if (lessee == null)
            {
                return NotFound();
            }

            var lesseeViewModel = this.ToLesseeViewModel(lessee);
            return View(lesseeViewModel);
        }

        private LesseeViewModel ToLesseeViewModel(Lessee lessee)
        {
            return new LesseeViewModel
            {
                Id = lessee.Id,
                FirstName = lessee.FirstName,
                LastName = lessee.LastName,
                FixedPhone = lessee.FixedPhone,
                CellPhone = lessee.CellPhone,
                Address = lessee.Address,
                Photo = lessee.Photo,
                User = lessee.User,
                Document = lessee.Document,
            };
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel lesseeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = lesseeViewModel.Photo;

                    if(lesseeViewModel.ImageFile != null && lesseeViewModel.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\lessees", file);

                        using(var stream = new FileStream(path, FileMode.Create))
                        {
                            await lesseeViewModel.ImageFile.CopyToAsync(stream);    
                        }

                        path = $"~/images/lessees/{file}";
                    }

                    var lessee = this.ToLessee(lesseeViewModel, path); 

                    _context.Update(lessee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LesseeExists(lesseeViewModel.Id))
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
            return View(lesseeViewModel);
        }

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _context.Lessees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _context.Lessees.FindAsync(id);
            _context.Lessees.Remove(lessee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LesseeExists(int id)
        {
            return _context.Lessees.Any(e => e.Id == id);
        }
    }
}
