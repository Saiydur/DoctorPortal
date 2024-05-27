using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public ApplicationDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    b => b.MigrationsAssembly(_migrationAssemblyName)
                );
            }

            base.OnConfiguring(optionsBuilder);
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Doctor Appointment Relation
            modelBuilder.Entity<Appointment>()
                .HasOne(c=>c.Doctor)
                .WithMany(d=>d.Appointments)
                .HasForeignKey(c=>c.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //User Appointment Relation
            modelBuilder.Entity<Appointment>()
                .HasOne(c => c.User)
                .WithMany(d => d.Appointments)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Appointment Prescription Relation
            modelBuilder.Entity<Prescription>()
                .HasOne(c => c.Appointment)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(c => c.AppointmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Doctor Speciality Relation
            modelBuilder.Entity<Doctor>()
                .HasOne(c => c.Speciality)
                .WithMany(d => d.Doctors)
                .HasForeignKey(c => c.DoctorSpecialityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Rating Doctor Relation
            modelBuilder.Entity<Rating>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.PatientRating)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            //Rating User Relation
            modelBuilder.Entity<Rating>()
                .HasOne(c => c.User)
                .WithMany(d => d.PatientRating)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //PaymentHistory Appointment Relation
            modelBuilder.Entity<PaymentHistory>()
                .HasOne(c => c.Appointment)
                .WithMany(d => d.PaymentHistories)
                .HasForeignKey(c => c.AppointmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //PaymentHistory User Relation
            modelBuilder.Entity<PaymentHistory>()
                .HasOne(c => c.User)
                .WithMany(d => d.PaymentHistories)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //PaymentHistory Doctor Relation
            modelBuilder.Entity<PaymentHistory>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.PaymentHistories)
                .HasForeignKey(c => c.DoctorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Consultation Doctor Relation
            modelBuilder.Entity<Consultation>()
                .HasMany(c => c.Doctors)
                .WithOne(d => d.Consultation)
                .HasForeignKey(d=> d.ConsultationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Consultation Category Relation
            modelBuilder.Entity<Consultation>()
                .HasOne(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        //DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> DoctorSpecialities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
