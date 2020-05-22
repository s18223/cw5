using System;
using cw4.DAL;
using cw4.Models;
using Microsoft.AspNetCore.Mvc;

namespace cw4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string studentId)
        {
            return Ok(_dbService.GetStudents(int.Parse(studentId)));
        }
    }
}
