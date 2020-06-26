using System;
using System.Collections.Generic;
using cw4.Models;
using cw4.DAL;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using cw5.DTOs;

namespace cw4.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        public void EnrollStudent(EnrollStudentRequest request)
        {

            //var st = new Student(Int32.Parse(request.IndexNumber), request.FirstName, request.LastName, request.id);

            using (var con = new MySqlConnection("SERVER=localhost;DATABASE=owndb;UID=root;PASSWORD=my_password"))
            using (var com = new MySqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //1. Czy studia istnieja?
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //return BadRequest("Studia nie istnieja");
                        //...
                    }
                    int idstudies = (int)dr["IdStudies"];

                    //x. Dodanie studenta
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("index", request.FirstName);
                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

        }

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

        public void PromoteStudent(PromoteStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public interface IDbService
    {
        public IEnumerable<Student> GetStudents(int studentId);
        void EnrollStudent(EnrollStudentRequest request);
        void PromoteStudent(PromoteStudentRequest request);
    }
}
