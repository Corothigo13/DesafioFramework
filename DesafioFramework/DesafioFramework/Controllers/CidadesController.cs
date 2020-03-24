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
    public class CidadesController : Controller
    {
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;

        public CidadesController(ICidadeService cidadeService, IEstadoService estadoService)
        {
            _cidadeService = cidadeService;
            _estadoService = estadoService;
        }

        // GET: Pais
        public IActionResult Index()
        {
            return View(_cidadeService.GetAll());
        }

        // GET: Pais/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = _cidadeService.GetById(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            ViewBag.Cidades = _estadoService.GetAll();
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Descricao,EstadoId,Id")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                cidade.Estado = _estadoService.GetById(cidade.EstadoId).Descricao;
                cidade.DateCreate = DateTime.Now;
                _cidadeService.Add(cidade);
                _cidadeService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cidade);
        }

        // GET: Pais/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cidade = _cidadeService.GetById(id);
            if (cidade == null)
            {
                return NotFound();
            }
            ViewBag.Cidades = _estadoService.GetAll();
            return View(cidade);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Descricao,DateCreate,EstadoId,Id")] Cidade cidade)
        {
            if (id != cidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cidade.Estado = _estadoService.GetById(cidade.EstadoId).Descricao;
                    cidade.DateUpdate = DateTime.Now;
                    _cidadeService.Update(cidade);
                    _cidadeService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidade.Id))
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
            return View(cidade);
        }

        // GET: Pais/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = _cidadeService.GetById(id);
            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _cidadeService.Delete(id);
            _cidadeService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(int id)
        {
            bool exists = false;
            var cidade = _cidadeService.GetById(id);

            if (cidade != null)
                exists = true;

            return exists;
        }
    }
}
