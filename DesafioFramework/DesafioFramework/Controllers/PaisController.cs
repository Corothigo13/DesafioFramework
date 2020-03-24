using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models.DatabaseContext;
using Interface;

namespace DesafioFramework.Controllers
{
    public class PaisController : Controller
    {
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        // GET: Pais
        public IActionResult Index()
        {
            return View(_paisService.GetAll());
        }

        // GET: Pais/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = _paisService.GetById(id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Descricao,Id")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                pais.DateCreate = DateTime.Now;
                _paisService.Add(pais);
                _paisService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Pais/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = _paisService.GetById(id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Descricao,DateCreate,Id")] Pais pais)
        {
            if (id != pais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pais.DateUpdate = DateTime.Now;
                    _paisService.Update(pais);
                    _paisService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(pais.Id))
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
            return View(pais);
        }

        // GET: Pais/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = _paisService.GetById(id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _paisService.Delete(id);
            _paisService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisExists(int id)
        {
            bool exists = false;
            var pais = _paisService.GetById(id);

            if (pais != null)
                exists = true;

            return exists;
        }
    }
}
