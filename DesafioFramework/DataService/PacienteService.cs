using DataAccess;
using Interface;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class PacienteService : BaseService<Paciente>, IPacienteService
    {
        private readonly ApplicationDBContext _context;

        public PacienteService(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
