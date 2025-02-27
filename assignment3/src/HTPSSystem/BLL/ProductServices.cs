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
	public class ProductServices
	{
		#region setup of the context connection variable and class constructor

		private readonly HTPSContext _context;

		internal ProductServices(HTPSContext syscontext)
		{
			_context = syscontext;
		}
		#endregion

		#region Queries
		public List<Product> Products_Get()
		{
			return _context.Products
				.OrderBy(x => x.Name)
				.ToList();
		}
		#endregion
	}
}
