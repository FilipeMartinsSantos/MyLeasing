using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(IOwnerRepository ownerRepository, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll());
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Owners");
                }

                var owner = _converterHelper.TOOwner(model, path, true);

                //TODO: Modificar para o user que tiver logado
                owner.User = await _userHelper.GetUserByEmailAsync("santos.filipe.m@hotmail.com");
                await _ownerRepository.CreateAsync(owner);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Owner ToOwner(OwnerViewModel model, string path)
        {
            return new Owner
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FixedPhone = model.FixedPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                ImageUrl = path,
                User = model.User,
                Document = model.Document,
            };
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToOwnerViewModel(owner);
            return View(model);
        }

        private OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixedPhone = owner.FixedPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                ImageUrl = owner.ImageUrl,
                User = owner.User,
                Document = owner.Document,
            };
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Owners");
                    }

                    var owner = _converterHelper.TOOwner(model, path, false);

                    //TODO: Modificar para o user que tiver logado
                    owner.User = await _userHelper.GetUserByEmailAsync("santos.filipe.m@hotmail.com");
                    await _ownerRepository.UpdateAsync(owner);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(model.Id))
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
            return View(model);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            await _ownerRepository.DeleteAsync(owner);
            return RedirectToAction(nameof(Index));
        }
    }
}
