using System;
using cw4.DAL;
using cw4.Models;
using cw5.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController
    {
        private readonly IDbService _dbService;

        public EnrollmentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            if (request.FirstName == null || request.LastName == null || request.Studies == null)
            {

                return new NotFoundResult();
            }

            _dbService.EnrollStudent(request);
            var response = new EnrollStudentResponse();
            response.LastName = request.LastName;
            response.Semester = 1;
            response.StartDate = DateTime.Now;
            //...

            return new OkObjectResult(response);
        }
        [Route("/promotions")]
        [HttpPost]
        public IActionResult PromoteStudent(PromoteStudentRequest request)
        {
            _dbService.PromoteStudent(request);
            return new OkObjectResult(null);
        }
    }
}
