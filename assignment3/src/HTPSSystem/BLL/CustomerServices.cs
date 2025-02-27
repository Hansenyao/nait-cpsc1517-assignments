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
	public class CustomerServices
	{
		#region setup of the context connection variable and class constructor

		private readonly HTPSContext _context;

		internal CustomerServices(HTPSContext syscontext)
		{
			_context = syscontext;
		}
		#endregion

		#region Queries
		public List<Customer> Customers_Get()
		{
			return _context.Customers
				.OrderBy(x => x.LastName)
				.ThenBy(x => x.FirstName)
				.ToList();
		}
		#endregion
	}
}
