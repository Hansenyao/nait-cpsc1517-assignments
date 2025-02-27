using ProgramSystemLibrary.DAL;
using ProgramSystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSystemLibrary.BLL
{
    public class SchoolServices
    {
        private StarTEDContext _context;

        public SchoolServices(StarTEDContext context) 
        {
            _context = context;
        }

        public List<School> GetAllSchools()
        {
            return _context.Schools.OrderBy(x => x.SchoolName).ToList();
        }
    }
}
