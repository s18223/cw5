using System;
using System.Collections.Generic;
using cw4.Models;
using cw4.DAL;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace cw4.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        public IEnumerable<Student> GetStudents(int studentId)
        {
            var students = new List<Student>();
            using (var con = new MySqlConnection("SERVER=localhost;DATABASE=owndb;UID=root;PASSWORD=my_password"))
            {
                con.Open();
                var cmd = new MySqlCommand("select *, Enrollment.Semester from Student, Enrollment where Student.IdEnrollment = Enrollment.IdEnrollment AND Student.IndexNumber = @id", con);
                cmd.Parameters.AddWithValue("id", studentId);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var student = new Student(
                        int.Parse(dr["IndexNumber"].ToString()),
                        dr["FirstName"].ToString(),
                        dr["LastName"].ToString(),
                        int.Parse(dr["IdEnrollment"].ToString())
                        ); ;

                    students.Add(student);
                }
                con.Close();
            }

            return students;
        }
    }

    public interface IDbService
    {
        public IEnumerable<Student> GetStudents(int studentId);
    }
}
