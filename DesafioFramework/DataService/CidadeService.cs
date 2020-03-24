using DataAccess;
using Interface;
using Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class CidadeService : BaseService<Cidade>, ICidadeService
    {
        private readonly ApplicationDBContext _context;

        public CidadeService(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
