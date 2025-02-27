using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgramSystemLibrary.BLL;
using ProgramSystemLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramSystemLibrary
{
    public static class StarTEDExtensions
    {
        // Register database context and services
        public static void StarTEDExtensionServices(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<StarTEDContext>(options);

            services.AddScoped<ProgramServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<StarTEDContext>();
                return new ProgramServices(context);
            });

            services.AddScoped<ProgramCourseServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<StarTEDContext>();
                return new ProgramCourseServices(context);
            });

            services.AddScoped<SchoolServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<StarTEDContext>();
                return new SchoolServices(context);
            });
        }
    }
}
