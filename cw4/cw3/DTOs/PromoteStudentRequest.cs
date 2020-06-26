using System;
using System.ComponentModel.DataAnnotations;

namespace cw5.DTOs
{
    public class PromoteStudentRequest
    {
        [Required]
        public int Semester { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
