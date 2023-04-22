using MedicalResearchCenter.Data.IRepositories;
using MedicalResearchCenter.Data.Repositories;

namespace MedicalResearchCenter.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectService(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<PatientService>();
            
            services.AddScoped<IResearchProjectRepository, ResearchProjectRepository>();
            services.AddScoped<ResearchProjectService>();

            return services;
        }
    }
}
