using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        //DbSets
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
