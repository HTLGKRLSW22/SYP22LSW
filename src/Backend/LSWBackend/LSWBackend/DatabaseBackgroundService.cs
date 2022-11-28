using System.Globalization;

namespace LSWBackend;

public class DatabaseBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseBackgroundService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;


    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LSWContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Console.WriteLine("Database created...");
        if (!db.Teachers.Any()) {
            InitializeDb(db);
            Console.WriteLine("Database initialized...");
        }
        return Task.Run(() => { }, stoppingToken);
    }


    private static void InitializeDb(LSWContext db) {
        db.Teachers.AddRange(LoadTeachers());
        db.SaveChanges();
        db.Clazzes.AddRange(LoadClazzes());
        db.SaveChanges();
        db.Students.AddRange(LoadStudents());
        db.SaveChanges();
        db.Offers.AddRange(LoadOffers());
        db.SaveChanges();
        db.AvailableDates.AddRange(LoadAvailableDates());
        db.SaveChanges();
        db.OfferTeachers.AddRange(LoadOfferTeachers());
        db.SaveChanges();
        db.ClassOffers.AddRange(LoadClassOffers());
        db.SaveChanges();
        db.OfferDates.AddRange(LoadOfferDates());
        db.SaveChanges();
        db.StudentOffers.AddRange(LoadStudentOffers());
        db.SaveChanges();
    }


    private static List<Teacher> LoadTeachers() {
        return File.ReadAllLines("InitData/1_Teachers.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new Teacher {
                FirstName = x[0],
                LastName = x[1],
                IsAdmin = int.Parse(x[2]),
                Username = x[3],
            })
            .ToList();
    }


    private static List<Clazz> LoadClazzes() {
        return File.ReadAllLines("InitData/2_Clazzes.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new Clazz {
                ClazzName = x[0],
                TeacherId = int.Parse(x[1]),
            })
            .ToList();
    }


    private static List<Student> LoadStudents() {
        return File.ReadAllLines("InitData/3_Students.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new Student {
                FirstName = x[0],
                LastName = x[1],
                Username = x[2],
                ClazzId = int.Parse(x[3]),
            })
            .ToList();
    }


    private static List<Offer> LoadOffers() {
        return File.ReadAllLines("InitData/4_Offers.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new Offer {
                Description = x[0],
                Costs = decimal.Parse(x[1], CultureInfo.InvariantCulture),
                Location = x[2],
                MaxStudents = int.Parse(x[3]),
                MeetingPoint = x[4],
                MinStudents = int.Parse(x[5]),
                Title = x[6],
                TeacherId = int.Parse(x[7]),
                VisibleForStudents = int.Parse(x[8]),
            })
            .ToList();
    }


    private static List<AvailableDate> LoadAvailableDates() {
        return File.ReadAllLines("InitData/5_AvailableDates.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new AvailableDate {
                Date = DateTime.Parse(x[0]),
            })
            .ToList();
    }


    private static List<OfferTeacher> LoadOfferTeachers() {
        return File.ReadAllLines("InitData/6_OfferTeachers.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new OfferTeacher {
                OfferId = int.Parse(x[0]),
                TeacherId = int.Parse(x[1]),
            })
            .ToList();
    }


    private static List<ClassOffer> LoadClassOffers() {
        return File.ReadAllLines("InitData/7_ClassOffers.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new ClassOffer {
                ClazzId = int.Parse(x[0]),
                OfferId = int.Parse(x[1]),
            })
            .ToList();
    }


    private static List<OfferDate> LoadOfferDates() {
        return File.ReadAllLines("InitData/8_OfferDates.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new OfferDate {
                StartDate = DateTime.Parse(x[0]),
                EndDate = DateTime.Parse(x[1]),
                OfferId = int.Parse(x[2]),
            })
            .ToList();
    }


    private static List<StudentOffer> LoadStudentOffers() {
        return File.ReadAllLines("InitData/9_StudentOffers.csv")
            .Skip(1)
            .Select(x => x.Split(","))
            .Select(x => new StudentOffer {
                OfferId = int.Parse(x[0]),
                StudentId = int.Parse(x[1]),
            })
            .ToList();
    }
}
