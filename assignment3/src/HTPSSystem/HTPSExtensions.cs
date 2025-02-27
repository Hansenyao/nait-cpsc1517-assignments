using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HTPSSystem.DAL;
using HTPSSystem.BLL;
#endregion


namespace HTPSSystem
{
	public static class HTPSExtensions
	{
		public static void HTPSDependencies(this IServiceCollection services,
		  Action<DbContextOptionsBuilder> options)
		{

			services.AddDbContext<HTPSContext>(options);

			services.AddTransient<ProductServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetService<HTPSContext>();
				return new ProductServices(context);
			});

			services.AddTransient<CustomerServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetService<HTPSContext>();
				return new CustomerServices(context);
			});

			services.AddTransient<RegistrationServices>((serviceProvider) =>
			{
				var context = serviceProvider.GetService<HTPSContext>();
				return new RegistrationServices(context);
			});
		}
	}
}
