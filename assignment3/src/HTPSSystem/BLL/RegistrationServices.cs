using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using HTPSSystem.DAL;
using HTPSSystem.Entities;
#endregion

namespace HTPSSystem.BLL
{
	public class RegistrationServices
	{
		#region setup of the context connection variable and class constructor

		private readonly HTPSContext _context;

		internal RegistrationServices(HTPSContext syscontext)
		{
			_context = syscontext;
		}
		#endregion

		#region Queries
		public List<Registration> Registrations_Get()
		{
			return _context.Registrations
				.OrderByDescending(x => x.RegistrationID)
				.ToList();
		}
		#endregion

		#region CRUD Activity 1

		public int AddRegistration(Registration item)
		{
            //TODO: 1 Complete the AddRegistration method
            //      Check instance received by method
            //      Check product id with serial number does not already exist on the database
            //      If the data is acceptable; add the registration to the database
            //

            // Show an exception if item is null
            if (item == null)
            {
                throw new ArgumentNullException("You must supply a new registration.");
            }

			// Check the same SN exist or not
			bool exist = _context.Registrations.Any(x => (x.SerialNumber == item.SerialNumber) 
													&& (x.ProductID == item.ProductID));
			if (exist)
			{
                throw new ArgumentException("Registration already exists, cannot add.");
            }

			// Add the new registration to database
			_context.Registrations.Add(item);
			_context.SaveChanges();

            // Return the record's id
            return item.RegistrationID;
		}

		#endregion

	}
}
