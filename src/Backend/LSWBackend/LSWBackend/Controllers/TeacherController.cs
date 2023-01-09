using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LSWBackend.Controllers;

[Route("api/[controller]")]
public class TeacherController : Controller
{
    private readonly TeacherService _teacherService;

    public TeacherController(TeacherService teacherService) => _teacherService = teacherService;

    // GET: api/values
    [HttpGet]
    public Dictionary<string, int> Get(int dateAsInt) {
        return _teacherService.AmountHoursFromTeachers(dateAsInt);
    }
}

