
using Microsoft.EntityFrameworkCore;

namespace LSWDbLib;

public class LSWContext : DbContext
{
    public LSWContext() {
    }

    public LSWContext(DbContextOptions<LSWContext> options) : base(options) {
    }


    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;
    public DbSet<OfferDate> OfferDates { get; set; } = null!;
    public DbSet<StudentOffer> StudentOffers { get; set; } = null!;
    public DbSet<WaitingList> WaitingLists { get; set; } = null!;
    public DbSet<OfferTeacher> OfferTeachers { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;
    public DbSet<ReportImage> ReportImages { get; set; } = null!;
    public DbSet<Clazz> Clazzes { get; set; } = null!;
    public DbSet<ClassOffer> ClassOffers { get; set; } = null!;
    public DbSet<Priority> Priorities { get; set; } = null!;
    public DbSet<TeacherLesson> TeacherLessons { get; set; } = null!;
    public DbSet<AvailableDate> AvailableDates { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer(@"server=(LocalDB)\mssqllocaldb;attachdbfilename=D:\Temp\Persons.mdf;database=LSWDb;integrated security=True;MultipleActiveResultSets=True;");
        }
    }
}
