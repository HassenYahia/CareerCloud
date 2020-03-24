using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
   public class CareerCloudContext : DbContext
    {
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options) : base(options)
        {

        }
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes {get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
          string  _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

       //     optionsBuilder.UseLazyLoadingproxie();
        optionsBuilder.UseSqlServer(_connStr);
      //      optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>
                (entity =>
                {
                    entity.HasOne(AE => AE.ApplicantProfile)
                    .WithMany(AP => AP.ApplicantEducations)
                    .HasForeignKey(AE => AE.Applicant);
                });

            modelBuilder.Entity<ApplicantJobApplicationPoco>
               (entity =>
               {
                   entity.HasOne(AJA => AJA.ApplicantProfile)
                   .WithMany(AJA => AJA.ApplicantJobApplications)
                   .HasForeignKey(AE => AE.Applicant);

                   entity.HasOne(AJA => AJA.CompanyJob)
                   .WithMany(CJ => CJ.ApplicantJobApplictions)
                   .HasForeignKey(AJA => AJA.Job);
               });
            modelBuilder.Entity<ApplicantProfilePoco>
               (entity =>
               {
                   entity.HasOne(AP => AP.SecurityLogin)
                   .WithMany(SL => SL.ApplicantProfiles)
                   .HasForeignKey(AP => AP.Login);

                   entity.HasOne(AP => AP.SystemCountryCode)
                   .WithMany(SCC => SCC.ApplicantProfiles)
                   .HasForeignKey(AP => AP.Country);
               });

            modelBuilder.Entity<ApplicantResumePoco>
               (entity =>
               {
                   entity.HasOne(AR => AR.ApplicantProfile)
                   .WithMany(AP => AP.ApplicantResumes)
                   .HasForeignKey(AR => AR.Applicant);
               });
            modelBuilder.Entity<ApplicantSkillPoco>
               (entity =>
               {
                   entity.HasOne(AS => AS.ApplicantProfile)
                   .WithMany(AP => AP.ApplicantSkills)
                   .HasForeignKey(AE => AE.Applicant);
               });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>
           (entity =>
           {
               entity.HasOne(AWH => AWH.ApplicantProfile)
               .WithMany(AP => AP.ApplicantWorkHistories)
               .HasForeignKey(AWH => AWH.Applicant);

               entity.HasOne(AWH => AWH.SystemCountryCode)
                   .WithMany(SCC => SCC.ApplicantWorkHistories)
                   .HasForeignKey(AWH => AWH.CountryCode);
           });

            modelBuilder.Entity<CompanyDescriptionPoco>
           (entity =>
           {
               entity.HasOne(CD => CD.CompanyProfile)
               .WithMany(CP => CP.CompanyDescriptions)
               .HasForeignKey(CD => CD.Company);

               entity.HasOne(CD => CD.SystemLanguageCode)
                   .WithMany(SLC => SLC.CompanyDescriptions)
                   .HasForeignKey(CD => CD.LanguageId);
           });

            modelBuilder.Entity<CompanyJobEducationPoco>
              (entity =>
              {
                  entity.HasOne(CJ => CJ.CompanyJob)
                  .WithMany(CJE => CJE.CompanyJobEducations)
                  .HasForeignKey(CJ => CJ.Job);
              });

            modelBuilder.Entity<CompanyJobSkillPoco>
              (entity =>
              {
                  entity.HasOne(CJS => CJS.CompanyJob)
                  .WithMany(CJ => CJ.CompanyJobSkills)
                  .HasForeignKey(CJS => CJS.Job);
              });

            modelBuilder.Entity<CompanyJobPoco>
              (entity =>
              {
                  entity.HasOne(CJ => CJ.CompanyProfile)
                  .WithMany(CP => CP.CompanyJobs)
                  .HasForeignKey(CJ => CJ.Company);
              });

            modelBuilder.Entity<CompanyJobDescriptionPoco>
              (entity =>
              {
                  entity.HasOne(CJD => CJD.CompanyJob)
                  .WithMany(CJE => CJE.CompanyJobDescriptions)
                  .HasForeignKey(CJD => CJD.Job);
              });

            modelBuilder.Entity<CompanyLocationPoco>
              (entity =>
              {
                  entity.HasOne(CJ => CJ.CompanyProfile)
                  .WithMany(CJE => CJE.CompanyLocations)
                  .HasForeignKey(CJ => CJ.Company);
              });

            modelBuilder.Entity<SecurityLoginsLogPoco>
              (entity =>
              {
                  entity.HasOne(CJ => CJ.SecurityLogin)
                  .WithMany(CJE => CJE.SecurityLoginsLogs)
                  .HasForeignKey(CJ => CJ.Login);
              });

            modelBuilder.Entity<SecurityLoginsRolePoco>
          (entity =>
          {
              entity.HasOne(CD => CD.SecurityLogin)
              .WithMany(CP => CP.SecurityLoginsRoles)
              .HasForeignKey(CD => CD.Login);

              entity.HasOne(CD => CD.SecurityRole)
                  .WithMany(SLC => SLC.SecurityLoginsRoles)
                  .HasForeignKey(CD => CD.Role);
          });

            base.OnModelCreating(modelBuilder);
        }
    }
}
