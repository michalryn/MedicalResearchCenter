﻿using MedicalResearchCenter.Data.Entities;

namespace MedicalResearchCenter.Data.IRepositories
{
    public interface ILabTestRepository
    {
        Task<LabTest> AddLabTestAsync(LabTest labTest);
        Task<LabTest> GetLabTestAsync(int id);
        Task UpdateLabTestAsync(LabTest labTest);
        Task<bool> ExistsAsync(string name);
    }
}