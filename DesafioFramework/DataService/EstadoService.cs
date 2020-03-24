using DataAccess;
using Interface;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class EstadoService : BaseService<Estado>, IEstadoService
    {
        private readonly ApplicationDBContext _context;

        public EstadoService(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
