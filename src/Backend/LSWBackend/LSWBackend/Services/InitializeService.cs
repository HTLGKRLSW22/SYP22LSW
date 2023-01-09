namespace LSWBackend.Services;

public class InitializeService
{
    private readonly LSWContext _db;

    public InitializeService(LSWContext db) => _db = db;


    private const string AdminUsername = "doppj";


    public void ResetDatabase() {
        _db.Database.ExecuteSqlRaw("DELETE FROM offerDates;");
        _db.Database.ExecuteSqlRaw("DELETE FROM teacherLessons;");
        _db.Database.ExecuteSqlRaw("DELETE FROM offerTeachers;");
        _db.Database.ExecuteSqlRaw("DELETE FROM classOffers;");
        _db.Database.ExecuteSqlRaw("DELETE FROM studentOffers;");
        _db.Database.ExecuteSqlRaw("DELETE FROM priorities;");
        _db.Database.ExecuteSqlRaw("DELETE FROM waitingLists;");
        _db.Database.ExecuteSqlRaw("DELETE FROM students;");
        _db.Database.ExecuteSqlRaw("DELETE FROM clazzes;");
        _db.Database.ExecuteSqlRaw($"DELETE FROM teachers WHERE LOWER(username) != '{AdminUsername}';");
        _db.Database.ExecuteSqlRaw("DELETE FROM reportImages;");
        _db.Database.ExecuteSqlRaw("DELETE FROM reports;");
        _db.Database.ExecuteSqlRaw("DELETE FROM offers;");
        _db.Database.ExecuteSqlRaw("DELETE FROM phases;");
        _db.Database.ExecuteSqlRaw("DELETE FROM availableDates;");
    }


    public string GetFileName() {
        return _db.Teachers.Count() == 1 ? "teachers.csv" : "students.csv";
    }


    public List<string> InitializeTeachers() {
        List<string> errorMessages = new();
        var teachers = ReadFile("teachers.csv");

        if (teachers[0][0].ToLower() != "id" || teachers[0][1].ToLower() != "benutzername"
            || teachers[0][2].ToLower() != "nachname" || teachers[0][3].ToLower() != "vorname") {
            throw new Exception("Die Datei entspricht nicht dem vorgegebenen Format: Id;Benutzername;Nachname;Vorname");
        }

        foreach (var teacher in teachers.Skip(1)) {
            if (teacher[1].ToLower() == AdminUsername) continue;
            if (teacher.Length != 4) {
                errorMessages.Add($"Ungültiges Format bei Lehrer: {teacher[0]}");
                continue;
            }

            if (string.IsNullOrEmpty(teacher[1]) || string.IsNullOrEmpty(teacher[2]) || string.IsNullOrEmpty(teacher[3])) {
                errorMessages.Add($"Lehrer konnte nicht hinzugefügt werden: {teacher[1] ?? "!k.A.!"} {teacher[2] ?? "!k.A.!"}, {teacher[3] ?? "!k.A.!"}");
            }
            else {
                _db.Teachers.Add(new Teacher {
                    Username = teacher[1],
                    LastName = teacher[2],
                    FirstName = teacher[3],
                });
            }
        }
        _db.SaveChanges();
        return errorMessages;
    }


    public List<string> InitializeStudents() {
        List<string> errorMessages = new();
        var students = ReadFile("students.csv");

        if (students[0][0].ToLower() != "klasse" || students[0][1].ToLower() != "klassenvorstand"
            || students[0][2].ToLower() != "familienname" || students[0][3].ToLower() != "vorname"
            || students[0][4].ToLower() != "email 1 (schüler)") {
            throw new Exception("Die Datei entspricht nicht dem vorgegebenen Format: Klasse;Klassenvorstand;Familienname;Vorname;Email 1 (Schueler)");
        }

        List<Clazz> clazzes = new();

        foreach (var student in students.Skip(1)) {
            var clazz = clazzes.FirstOrDefault(x => x.ClazzName == student[0]);
            if (clazz == null) {
                Teacher? teacher = GetTeacher(student[1]);

                if (teacher == null)
                    errorMessages.Add($"Die Klasse {student[0]} hat keinen Klassenvorstand!");

                clazz = new Clazz {
                    ClazzName = student[0],
                    Teacher = teacher,
                };
                _db.Clazzes.Add(clazz);
                _db.SaveChanges();
                clazzes.Add(clazz);
            }

            string? lastName = student[2];
            string? firstName = student[3];
            string? username = student[4].Split("@")[0];

            if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(username)) {
                errorMessages.Add($"Schüler konnte nicht hinzugefügt werden: {firstName ?? "!k.A.!"} {lastName ?? "!k.A.!"}, {username ?? "!k.A.!"} [{student[0]}]");
            }
            else {
                var studentToAdd = new Student {
                    LastName = lastName,
                    FirstName = firstName,
                    Username = username,
                    Clazz = clazz,
                };

                _db.Students.Add(studentToAdd);
            }
        }

        _db.SaveChanges();
        return errorMessages;
    }


    private static List<string[]> ReadFile(string filename) {
        var lines = File.ReadAllLines($"InitData/{filename}");
        return lines.Length == 0 ? throw new Exception("Die angegebene Datei ist leer") : lines.Select(x => x.Split(";")).ToList();
    }


    private Teacher? GetTeacher(string teacherName) {
        var teacherNameParts = teacherName.Split(" ");
        teacherName = $"{teacherNameParts[teacherNameParts.Length - 2]} {teacherNameParts[teacherNameParts.Length - 1]}".ToLower();
        return teacherNameParts.Length < 2
            ? null
            : _db.Teachers
                .FirstOrDefault(x => x.FirstName.ToLower() + " " + x.LastName.ToLower() == teacherName);
    }

}
