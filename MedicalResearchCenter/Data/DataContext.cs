﻿using MedicalResearchCenter.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace MedicalResearchCenter.Data
{
    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        #endregion

        #region Properties
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ResearchProject> ResearchProjects { get; set; }
        public DbSet<LabReferral> LabReferrals { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<PatientTest> PatientTests { get; set; }

        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientTest>().HasKey(l => new
            {
                l.LabReferralId,
                l.LabTestId
            });
        }

        public void AttachEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Attach(entity);
        }

        public async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class, new()
        {
            await Set<TEntity>().AddAsync(entity);
        }

        public async Task AddEntityAndSaveChangesAsync<TEntity>(TEntity entity) where TEntity : class, new()
        {
            await AddEntityAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddEntitiesRangeAsync<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            await Set<TEntity>().AddRangeAsync(entity);
        }

        public async Task AddEntitiesRangeAndSaveChangesAsync<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            await AddEntitiesRangeAsync(entity);
            await SaveChangesAsync();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateEntityAndSaveChangesAsync<TEntity>(TEntity entity) where TEntity : class, new()
        {
            UpdateEntity(entity);
            await SaveChangesAsync();
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public async Task UpdateEntitiesRangeAndSaveChangesAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            await SaveChangesAsync();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public async Task RemoveEntitiesRangeAndSaveChangesAsync<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            await SaveChangesAsync();
        }
        #endregion
    }
}
