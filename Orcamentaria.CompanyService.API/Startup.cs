using Microsoft.Extensions.DependencyInjection.Extensions;
using Orcamentaria.CompanyService.Application.Validators;
using Orcamentaria.CompanyService.Domain.Mappers;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.CompanyService.Infrastructure.Contexts;
using Orcamentaria.CompanyService.Infrastructure.Repositories;
using Orcamentaria.Lib.Domain.Validators;
using Orcamentaria.Lib.Infrastructure;

namespace Orcamentaria.CompanyService.API
{
    public class Startup
    {
        private readonly string _serviceName = "Orcamentaria.CompanyService";
        private readonly string _apiVersion = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration = CommonDI.ResolveConfigs(_serviceName, services, Configuration);

            services.Replace(ServiceDescriptor.Singleton(Configuration));

            CommonDI.AddServiceRegistryHosted(services, Configuration);
            
            CommonDI.ResolveCommonServicesWithMySql<MySqlContext>
                (_serviceName, _apiVersion, services, Configuration, () =>
            {
                //Validators
                services.AddScoped<IValidatorEntity<Company>, CompanyValidator>();

                //Mappers
                services.AddAutoMapper(typeof(CompanyMapper));

                //Services
                services.AddScoped<ICompanyService, Application.Services.CompanyService>();

                //Repositories
                services.AddScoped<ICompanyRepository, CompanyRepository>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			=> CommonDI.ConfigureCommon(_serviceName, _apiVersion, app, env);
    }
}
