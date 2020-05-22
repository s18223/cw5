using System;
namespace cw4.Models
{
    public class Student
    {

        public Student(int IdStudent, string FirstName, string LastName, int IdEnrollment)
        {
            this.IdStudent = IdStudent;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IdEnrollment = IdEnrollment;
        }

        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public int IdEnrollment { get; set; }
    }
}
