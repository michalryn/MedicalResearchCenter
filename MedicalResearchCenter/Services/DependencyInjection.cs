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

            services.AddScoped<ILabTestRepository, LabTestRepository>();
            services.AddScoped<LabTestService>();

            services.AddScoped<ILabReferralRepository, LabReferralRepository>();
            services.AddScoped<LabReferralService>();

            services.AddScoped<IPatientTestRepository, PatientTestRepository>();

            return services;
        }
    }
}
