using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProgramSystemLibrary.DAL;
using ProgramSystemLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSystemLibrary.BLL
{
    public class ProgramServices
    {
        private StarTEDContext _context;
        public ProgramServices(StarTEDContext context) 
        { 
            _context = context;
        }
        #region Queries 
        public List<Program> GetAllPrograms()
        {
            return _context.Programs.Include(p => p.SchoolCodeNavigation).ToList();
        }

        public Program GetPrograms_ByID(int id)
        {
            return _context.Programs.Where(x => x.ProgramID == id).Include(p => p.SchoolCodeNavigation).FirstOrDefault();
        }

        public List<Program> GetPrograms_ByName(string name)
        {
            return _context.Programs.Where(x => x.ProgramName.ToLower().Contains(name.ToLower())).Include(p => p.SchoolCodeNavigation).ToList();
        }
        #endregion

        #region CRUD
        public int Programs_Add(Program program)
        {
            // Check inputs
            if (program == null)
            {
                throw new ArgumentNullException("You must supply the new program information.");
            }

            // Business Rule: The same program can only belong to one school
            // (no duplicate programs in the same school)
            bool exists = _context.Programs.Any(x => x.ProgramName == program.ProgramName
                                                && x.SchoolCode == program.SchoolCode);
            if (exists)
            {
                throw new ArgumentException("Program already exists, cannot add.");
            }

            // Add program to local
            _context.Programs.Add(program);

            // Commit adding to database
            _context.SaveChanges();

            return program.ProgramID;
        }

        public int Programs_Update(Program program)
        {
            // Check inputs
            if (program == null)
            {
                throw new ArgumentNullException("You must supply the new program information.");
            }

            // Need to check that the data exists in the database
            bool exists = _context.Programs.Any(p => p.ProgramID == program.ProgramID);
            if (!exists)
            {
                throw new ArgumentException($"Program {program.ProgramName} (ID: {program.ProgramID}) is no longer in the database.");
            }

            // Business Rule: The same program can only belong to one school
            // (no duplicate programs in the same school)
            exists = _context.Programs.Any(x => (x.ProgramID != program.ProgramID) 
                                             && ((x.ProgramName == program.ProgramName) && (x.SchoolCode == program.SchoolCode)));
            if (exists)
            {
                throw new ArgumentException($"Program {program.ProgramName} (ID: {program.ProgramID}) already exists for the indicated school.");
            }

            // update data in local
            EntityEntry<Program> updating = _context.Entry(program);
            updating.State = EntityState.Modified;

            // Commit updating to database, and return how many rows were updated in database.
            return _context.SaveChanges();
        }

        public int Programs_PhysicalDelete(Program program)
        {
            // Check inputs
            if (program == null)
            {
                throw new ArgumentNullException("You must supply the new program information.");
            }

            // Check if the program exists in database
            Program? exists = _context.Programs.Include(x => x.ProgramCourses).FirstOrDefault(p => p.ProgramID == program.ProgramID);
            if (exists == null)
            {
                throw new ArgumentException($"Program {program.ProgramName} (ID: {program.ProgramID}) is no longer in the database.");
            }

            // Check if the program exists in program courses table
            if (exists.ProgramCourses.Count > 0)
            {
                throw new ArgumentException($"Program {program.ProgramName} (ID: {program.ProgramID}) has related information in the database. Unable to delete.");
            }

            // update data in local
            EntityEntry<Program> deleting = _context.Entry(program);
            deleting.State = EntityState.Deleted;

            // Commit deleting to database, and return how many rows were deleted physically in database.
            return _context.SaveChanges();
        }
        #endregion
    }
}
