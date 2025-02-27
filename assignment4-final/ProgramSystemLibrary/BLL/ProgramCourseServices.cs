using ProgramSystemLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSystemLibrary.BLL
{
    public class ProgramCourseServices
    {
        private StarTEDContext _context;
        public ProgramCourseServices(StarTEDContext context) 
        {
            _context = context;
        }
    }
}
