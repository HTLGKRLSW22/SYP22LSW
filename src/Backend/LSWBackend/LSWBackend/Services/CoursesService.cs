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

            courseToEdit.Title = offerPutDto.Title;
            courseToEdit.Description = offerPutDto.Description;
            courseToEdit.Costs = (decimal)offerPutDto.Price;
            courseToEdit.MaxStudents = offerPutDto.MaxCount;
            courseToEdit.MinStudents = offerPutDto.MinCount;

            if (offerPutDto.StartDates.Count > 0 && offerPutDto.StartDates.Count == offerPutDto.EndDates.Count) {
                courseToEdit.OfferDates.Clear();
                List<OfferDate> offerDates = new();
                for (var i = 0; i < offerPutDto.StartDates.Count; i++) {
                    // avoid api crash using tryparse
                    if (DateTime.TryParse(offerPutDto.StartDates[i], out var startDate) && DateTime.TryParse(offerPutDto.EndDates[i], out var endDate)) {
                        offerDates.Add(new OfferDate {
                            StartDate = startDate,
                            EndDate = endDate
                        });
                    }
                }
                courseToEdit.OfferDates = offerDates;
            }

            // set classes
            if (offerPutDto.Clazzes.Count == 0) {
                // add all classes
                courseToEdit.ClassOffers.Clear();
                courseToEdit.ClassOffers = _db.Clazzes.Select(x => new ClassOffer {
                    Clazz = x,
                    OfferId = courseToEdit.OfferId,
                }).ToList();
            }
            else {
                courseToEdit.ClassOffers.Clear();
                List<ClassOffer> classOffers = new();
                foreach (var clazzName in offerPutDto.Clazzes) {
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
                Title = courseToEdit.Title,
                Description = courseToEdit.Description,
                Price = (double)courseToEdit.Costs,
                MaxCount = courseToEdit.MaxStudents,
                MinCount = courseToEdit.MinStudents,
                StartDates = courseToEdit.OfferDates.Select(x => $"{x.StartDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                EndDates = courseToEdit.OfferDates.Select(x => $"{x.EndDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                Teachers = courseToEdit.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToList(),
                CurrentCount = courseToEdit.StudentOffers.Count
            }.CopyPropertiesFrom(courseToEdit);
        }
        catch (InvalidOperationException) {
            return new OfferDto { Description = "-1" };
        }
        catch (DbUpdateException) {
            return new OfferDto { Description = "-2" };
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
                    Title = x.Title,
                    Description = x.Description,
                    Price = (double)x.Costs,
                    MaxCount = x.MaxStudents,
                    MinCount = x.MinStudents,
                    StartDates = x.OfferDates.Select(x => $"{x.StartDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                    EndDates = x.OfferDates.Select(x => $"{x.EndDate:dd.MM.yyyy HH:mm:ss}").ToList(),
                    Teachers = x.OfferTeachers.Select(y => $"{y.Teacher.FirstName} {y.Teacher.LastName}").ToList(),
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
