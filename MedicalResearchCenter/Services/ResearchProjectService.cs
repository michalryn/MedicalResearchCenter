using MedicalResearchCenter.Data.DTOs.ResearchProject;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace MedicalResearchCenter.Services
{
    public class ResearchProjectService : BaseService
    {
        #region Properties

        private readonly IResearchProjectRepository _researchProjectRepo;
        private readonly IPatientRepository _patientRepo;

        #endregion

        #region Constructors

        public ResearchProjectService(IResearchProjectRepository researchProjectRepo, IPatientRepository patientRepo)
        {
            _researchProjectRepo = researchProjectRepo;
            _patientRepo = patientRepo;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponseDTO> AddResearchProjectAsync(CreateRPDTO dto)
        {
            try
            {
                ResearchProject project = new ResearchProject()
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    Manager = dto.Manager,
                    StartDate = dto.StartDate
                };

                project = await _researchProjectRepo.AddResearchProjectAsync(project);

                ReadRPDTO newProject = new ReadRPDTO()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Manager = project.Manager,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate
                };

                return CreateSuccessResponse(201, "Research project created successfully", newProject);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while creating the research project");
            }
        }

        public async Task<ServiceResponseDTO> GetResearchProjectAsync(int id)
        {
            try
            {
                ResearchProject project = await _researchProjectRepo.GetResearchProjectAsync(id);
                
                if(project == null)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                ReadRPDTO dto = new ReadRPDTO()
                {
                    Id=project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    Manager = project.Manager,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate
                };

                return CreateSuccessResponse(200, "Research project retrieved successfully", dto);
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the research project");
            }
        }

        public async Task<ServiceResponseDTO> DeleteResearchProjectAsync(int id)
        {
            try
            {
                ResearchProject project = await _researchProjectRepo.GetResearchProjectAsync(id);

                if(project == null)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                await _researchProjectRepo.DeleteResearchProjectAsync(project);

                return CreateSuccessResponse(204, "");
            }
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while deleting the research project");
            }
        }

        public async Task<ServiceResponseDTO> UpdateResearchProjectAsync(UpdateRPDTO dto)
        {
            try
            {
                ResearchProject project = await _researchProjectRepo.GetResearchProjectAsync(dto.Id);

                if(project == null)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                project.Title = dto.Title;
                project.Description = dto.Description;
                project.Manager = dto.Manager;
                project.StartDate = dto.StartDate;
                project.EndDate = dto.EndDate;

                await _researchProjectRepo.UpdateResearchProjectAsync(project);

                return CreateSuccessResponse(204, "");
            }
            catch (Exception ex)
            {
                return CreateFailureResponse(500, "Error while updating the research project");
            }
        }

        public async Task<ServiceResponseDTO> AssignPatientAsync(int projectId, int patientId)
        {
            try
            {
                ResearchProject project = await _researchProjectRepo.GetResearchProjectWithPatientsAsync(projectId);
                Patient patient = await _patientRepo.GetPatientAsync(patientId);

                if(project == null)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                if(patient == null)
                {
                    return CreateFailureResponse(404, "Patient with such id was not found");
                }

                project.Patients?.Add(patient);

                await _researchProjectRepo.UpdateResearchProjectAsync(project);

                return CreateSuccessResponse(200, "Patient was successfully assigned to the research project");
            }
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while assigning the patient to the research project");
            }
        }

        public async Task<ServiceResponseDTO> RemovePatientAsync(int projectId, int patientId)
        {
            try
            {
                ResearchProject project = await _researchProjectRepo.GetResearchProjectWithPatientsAsync(projectId);
                Patient patient = await _patientRepo.GetPatientAsync(patientId);

                if (project == null)
                {
                    return CreateFailureResponse(404, "Research project with such id was not found");
                }

                if (patient == null)
                {
                    return CreateFailureResponse(404, "Patient with such id was not found");
                }

                project.Patients?.Remove(patient);

                await _researchProjectRepo.UpdateResearchProjectAsync(project);

                return CreateSuccessResponse(200, "Patient was successfully removed from the research project");
            }
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while removing the patient from the research project");
            }
        }

        public async Task<ServiceResponseDTO> GetResearchProjectsAsync(GetRPsDTO dto)
        {
            try
            {
                IQueryable<ResearchProject> projects = _researchProjectRepo.GetResearchProjects();

                projects = projects.OrderBy(p => p.Title);

                GetRPsResponseDTO response = new GetRPsResponseDTO();
                response.TotalCount = projects.Count();
                response.PageNumber = dto.PageNumber;
                response.PageSize = dto.PageSize;

                response.ResearchProjects = await projects
                    .Select(p => new ReadRPDTO()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Manager = p.Manager,
                        StartDate = p.StartDate,
                        EndDate = p.EndDate
                    }).ToPagedListAsync(dto.PageNumber, dto.PageSize);

                return CreateSuccessResponse(200, "Research projects retrieved successfully", response);
            }
            catch(Exception ex)
            {
                return CreateFailureResponse(500, "Error while retrieving the tutorships");
            }
        }

        #endregion
    }
}
