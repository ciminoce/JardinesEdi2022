using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JardinesEdi2022.Datos
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ViveroSqlDbContext _context;

        public UnitOfWork(ViveroSqlDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
