using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models.DatabaseContext;
using DataService;
using Interface;

namespace DesafioFramework.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;
        private readonly IPaisService _paisService;
        private readonly IEstadoService _estadoService;
        private readonly ICidadeService _cidadeService;

        public PacientesController(IPacienteService pacienteService, IPaisService paisService, IEstadoService estadoService,
            ICidadeService cidadeService)
        {
            _pacienteService = pacienteService;
            _paisService = paisService;
            _estadoService = estadoService;
            _cidadeService = cidadeService;
        }

        // GET: Pais
        public IActionResult Index()
        {
            return View(_pacienteService.GetAll());
        }

        // GET: Pais/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = _pacienteService.GetById(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            ViewBag.Paises = _paisService.GetAll();
            ViewBag.Estados = _estadoService.GetAll();
            ViewBag.Cidades = _cidadeService.GetAll();
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome,CPF,PaisId,EstadoId,CidadeId,Id")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.Pais = _paisService.GetById(paciente.PaisId).Descricao;
                paciente.Estado = _estadoService.GetById(paciente.EstadoId).Descricao;
                paciente.Cidade = _cidadeService.GetById(paciente.CidadeId).Descricao;
                paciente.DateCreate = DateTime.Now;
                _pacienteService.Add(paciente);
                _pacienteService.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pais/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = _pacienteService.GetById(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewBag.Paises = _paisService.GetAll();
            ViewBag.Estados = _estadoService.GetAll();
            ViewBag.Cidades = _cidadeService.GetAll();
            return View(paciente);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Nome,CPF,PaisId,EstadoId,CidadeId,DateCreate,Id")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    paciente.Pais = _paisService.GetById(paciente.PaisId).Descricao;
                    paciente.Estado = _estadoService.GetById(paciente.EstadoId).Descricao;
                    paciente.Cidade = _cidadeService.GetById(paciente.CidadeId).Descricao;
                    paciente.DateUpdate = DateTime.Now;
                    _pacienteService.Update(paciente);
                    _pacienteService.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pais/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = _pacienteService.GetById(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _pacienteService.Delete(id);
            _pacienteService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            bool exists = false;
            var paciente = _pacienteService.GetById(id);

            if (paciente != null)
                exists = true;

            return exists;
        }
    }
}
