using MedicalResearchCenter.Data.DTOs.ResearchProject;
using MedicalResearchCenter.Data.DTOs.Response;
using MedicalResearchCenter.Data.Entities;
using MedicalResearchCenter.Data.IRepositories;

namespace MedicalResearchCenter.Services
{
    public class ResearchProjectService : BaseService
    {
        #region Properties

        private readonly IResearchProjectRepository _researchProjectRepo;

        #endregion

        #region Constructors

        public ResearchProjectService(IResearchProjectRepository researchProjectRepo)
        {
            _researchProjectRepo = researchProjectRepo;
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

        #endregion
    }
}
