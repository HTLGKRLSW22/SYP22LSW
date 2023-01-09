namespace LSWBackend.Services;

public class CoursesService
{
    private readonly LSWContext _db;

    public CoursesService(LSWContext db) => _db = db;

    public OfferDto EditCourse(int id, CreateOfferDto offerPutDto) {
        try {
            var courseToEdit = _db.Offers
                .Include(x => x.StudentOffers)
                .Include(x => x.OfferDates)
                .Include(x => x.OfferTeachers)
                .ThenInclude(x => x.Teacher)
                .Include(x => x.ClassOffers)
                .ThenInclude(x => x.Clazz)
                .Single(x => x.OfferId == id);

            courseToEdit.Title = offerPutDto.Titel;
            courseToEdit.Description = offerPutDto.Beschreibung;
            courseToEdit.Costs = (decimal)offerPutDto.Preis;
            courseToEdit.MaxStudents = offerPutDto.MaxCount;
            courseToEdit.MinStudents = offerPutDto.MinCount;

            if (offerPutDto.Startdatum != null && offerPutDto.Enddatum != null && offerPutDto.Startdatum.Count == offerPutDto.Enddatum.Count) {
                courseToEdit.OfferDates.Clear();
                List<OfferDate> offerDates = new();
                for (var i = 0; i < offerPutDto.Startdatum.Count; i++) {
                    DateTime startDate;
                    DateTime endDate;
                    // avoid api crash using tryparse
                    if (DateTime.TryParse(offerPutDto.Startdatum[i], out startDate) && DateTime.TryParse(offerPutDto.Enddatum[i], out endDate)) {
                        offerDates.Add(new OfferDate {
                            StartDate = startDate,
                            EndDate = endDate
                        });
                    }
                }
                courseToEdit.OfferDates = offerDates;
            }

            // set classes
            if (offerPutDto.Klassen != null) {
                courseToEdit.ClassOffers.Clear();
                List<ClassOffer> classOffers = new();
                foreach (var clazzName in offerPutDto.Klassen) {
                    var clazz = _db.Clazzes.SingleOrDefault(x => x.ClazzName == clazzName);
                    if (clazz != null) {
                        classOffers.Add(new ClassOffer { Clazz = clazz, OfferId = courseToEdit.OfferId });
                    }
                }
                courseToEdit.ClassOffers = classOffers;
            }
            courseToEdit.CopyPropertiesFrom(offerPutDto);
            _db.SaveChanges();
            return new OfferDto() {
                Titel = courseToEdit.Title,
                Beschreibung = courseToEdit.Description,
                Preis = (double)courseToEdit.Costs,
                MaxCount = courseToEdit.MaxStudents,
                MinCount = courseToEdit.MinStudents,
                Startdatum = courseToEdit.OfferDates.Select(x => $"{x.StartDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                Enddatum = courseToEdit.OfferDates.Select(x => $"{x.EndDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                Lehrer = courseToEdit.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToList(),
                CurrentCount = courseToEdit.StudentOffers.Count
            }.CopyPropertiesFrom(courseToEdit);
        }
        catch (InvalidOperationException) {
            return new OfferDto { Beschreibung = "-1" };
        }
        catch (DbUpdateException) {
            return new OfferDto { Beschreibung = "-2" };
        }
    }

    public IEnumerable<OfferDto> GetTeacherCourses(int teacherId) {
        try {
            return _db.Offers
                .Include(x => x.StudentOffers)
                .Include(x => x.OfferDates)
                .Include(x => x.OfferTeachers)
                .ThenInclude(x => x.Teacher)
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new OfferDto {
                    Titel = x.Title,
                    Beschreibung = x.Description,
                    Preis = (double)x.Costs,
                    MaxCount = x.MaxStudents,
                    MinCount = x.MinStudents,
                    Startdatum = x.OfferDates.Select(x => $"{x.StartDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                    Enddatum = x.OfferDates.Select(x => $"{x.EndDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                    Lehrer = x.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToList(),
                    CurrentCount = x.StudentOffers.Count
                }.CopyPropertiesFrom(x))
                .ToList();
        }
        catch (Exception ex) {
            return new List<OfferDto>();
        }
    }

    // helper for indicating if teacher who wants to edit the specified course is actually the owner
    public bool IsTeachersCourse(int courseId, int teacherId) {
        try {
            var course = _db.Offers.Single(x => x.OfferId == courseId);
            return course.TeacherId == teacherId;
        }
        catch (Exception) {
            return false;
        }
    }

}
