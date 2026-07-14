using Microsoft.Extensions.DependencyInjection.Extensions;
using Orcamentaria.CompanyService.Application.Validators;
using Orcamentaria.CompanyService.Domain.Mappers;
using Orcamentaria.CompanyService.Domain.Models;
using Orcamentaria.CompanyService.Domain.Repositories;
using Orcamentaria.CompanyService.Domain.Services;
using Orcamentaria.CompanyService.Infrastructure.Contexts;
using Orcamentaria.CompanyService.Infrastructure.Repositories;
using Orcamentaria.Lib.Domain.Validators;
using Orcamentaria.Lib.Infrastructure.Configures;

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
            Configuration = services.ResolveConfigs(Configuration, _serviceName);

            services.Replace(ServiceDescriptor.Singleton(Configuration));

            services.AddServiceRegistryHosted(Configuration);

            services.ResolveCommonServicesWithMySql<MySqlContext>(
                configuration: Configuration,
                serviceName: _serviceName,
                apiVersion: _apiVersion,
                customServices: () =>
                {
                //Validators
                services.AddScoped<IValidatorEntity<Company>, CompanyValidator>();

                //Mappers
                services.AddAutoMapper(_ => { },
                    typeof(CompanyMapper));

                //Services
                services.AddScoped<ICompanyService, Application.Services.CompanyService>();

                //Repositories
                services.AddScoped<ICompanyRepository<Company>, CompanyRepository>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.ConfigureCommon(env, _serviceName, _apiVersion);
    }
}
