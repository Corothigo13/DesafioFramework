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
    public class EstadoesController : Controller
    {
        private readonly IEstadoService _estadoService;
        private readonly IPaisService _paisService;

        public EstadoesController(IEstadoService estadoService, IPaisService paisService)
        {
            _estadoService = estadoService;
            _paisService = paisService;
        }

        // GET: Pais
        public IActionResult Index()
        {
            return View(_estadoService.GetAll());
        }

        // GET: Pais/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = _estadoService.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            ViewBag.Paises = _paisService.GetAll();
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Descricao,PaisId,Id")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                estado.Pais = _paisService.GetById(estado.PaisId).Descricao;
                estado.DateCreate = DateTime.Now;
                _estadoService.Add(estado);
                _estadoService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(estado);
        }

        // GET: Pais/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = _estadoService.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }
            ViewBag.Paises = _paisService.GetAll();
            return View(estado);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Descricao,PaisId,DateCreate,Id")] Estado estado)
        {
            if (id != estado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    estado.Pais = _paisService.GetById(estado.PaisId).Descricao;
                    estado.DateUpdate = DateTime.Now;
                    _estadoService.Update(estado);
                    _estadoService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoExists(estado.Id))
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
            return View(estado);
        }

        // GET: Pais/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estado = _estadoService.GetById(id);
            if (estado == null)
            {
                return NotFound();
            }

            return View(estado);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _estadoService.Delete(id);
            _estadoService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoExists(int id)
        {
            bool exists = false;
            var estado = _estadoService.GetById(id);

            if (estado != null)
                exists = true;

            return exists;
        }
    }
}
