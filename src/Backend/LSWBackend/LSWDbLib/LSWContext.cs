using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib
{
    public class LSWContext : DbContext
    {
        public LSWContext() {
        }

        public LSWContext(DbContextOptions<LSWContext> options) : base(options) {
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferDate> OfferDates { get; set; }
        public DbSet<StudentOffer> StudentOffers { get; set; }
        public DbSet<WaitingList> WaitingLists { get; set; }
        public DbSet<OfferTeacher> OfferTeachers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportImage> ReportImages { get; set; }
        public DbSet<Clazz> ClassRooms { get; set; }
        public DbSet<ClassOffer> ClassRoomOffers { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<TeacherLesson> TeacherLessons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"server=(LocalDB)\mssqllocaldb;attachdbfilename=D:\Temp\Persons.mdf;database=LSWDb;integrated security=True;MultipleActiveResultSets=True;");
            }
        }
    }
}
