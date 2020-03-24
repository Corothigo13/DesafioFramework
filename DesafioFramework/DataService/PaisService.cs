using DataAccess;
using Interface;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class PaisService : BaseService<Pais>, IPaisService
    {
        private readonly ApplicationDBContext _context;

        public PaisService(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }


    }
}
