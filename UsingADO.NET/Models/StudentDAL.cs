using System.Data.SqlClient;
using UsingADO.NET.Models;

namespace CRUDUsingADO.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public StudentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConection"));
        }


        public List<Student1> GetStudents()
        {
            List<Student1> students = new List<Student1>();
            string qry = "select * from Student1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student1 student = new Student1();
                    student.RollNo = Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.City = dr["city"].ToString();
                    student.Percentage = Convert.ToInt32(dr["percentage"]);


                    students.Add(student);
                }
            }
            con.Close();
            return students;
        }
        public Student1 GetStudentByRollNO(int rollno)
        {
            Student1 student = new Student1();
            string qry = "select * from Student1 where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    student.RollNo = Convert.ToInt32(dr["rollno"]);
                    student.Name = dr["name"].ToString();
                    student.City = dr["city"].ToString();
                    student.Percentage = Convert.ToInt32(dr["percentage"]);
                }
            }
            con.Close();
            return student;
        }
        public int AddStudent(Student1 student)
        {
            int result = 0;
            string qry = "insert into Student1 values(@name,@city,@percentage)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@city", student.City);
            cmd.Parameters.AddWithValue("@percentage", student.Percentage);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int UpdateStudent(Student1 student)
        {
            int result = 0;
            string qry = "update Student1 set name=@name,city=@city,percentage=@percentage where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@city", student.City);
            cmd.Parameters.AddWithValue("@percentage", student.Percentage);
            cmd.Parameters.AddWithValue("@rollno", student.RollNo);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteStudent(int rollno)
        {
            int result = 0;
            string qry = "delete from Student1 where rollno=@rollno";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@rollno", rollno);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


    }
}