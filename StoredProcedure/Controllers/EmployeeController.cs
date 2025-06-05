using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcedure.Data;
using StoredProcedure.Models;
using System.Text;

namespace StoredProcedure.Controllers
{
    public class EmployeeController : Controller
    {
        public StoredProcDbContext _context;
        public IConfiguration _confiq { get; }

        public EmployeeController
            (
                StoredProcDbContext context,
                IConfiguration confiq
            )
        {
            _context = context;
            _confiq = confiq;
        }


        public IActionResult Index()
        {
            return View();
        }

        //teha sp andmebaasi, mis annab andmed t;;tajatest
        public IEnumerable<Employee> SearchResult()
        {
            var result = _context.Employees
                .FromSqlRaw<Employee>("spSearchEmployees")
                .ToList();

            return result;
        }

        [HttpGet]
        public IActionResult DynamicSQL()
        {
            string connectionStr = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchEmployees";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (sdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult DynamicSql(string firstName, string lastName, string gender, int salary)
        {
            string connectionStr = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                StringBuilder sbCommand = new StringBuilder("select * from Employees where 1 = 1");

                if (firstName != null)
                {
                    sbCommand.Append(" AND FirstName=@FirstName");
                    SqlParameter param = new
                    SqlParameter("@FirstName", firstName);
                    cmd.Parameters.Add(param);
                }
                if (lastName != null)
                {
                    sbCommand.Append(" AND LastName=@LastName");
                    SqlParameter param = new
                    SqlParameter("@LastName", lastName);
                    cmd.Parameters.Add(param);
                }
                if (gender != null)
                {
                    sbCommand.Append(" AND Gender=@Gender");
                    SqlParameter param = new
                    SqlParameter("@Gender", gender);
                    cmd.Parameters.Add(param);
                }
                if (salary != 0)
                {
                    sbCommand.Append(" AND Salary=@Salary");
                    SqlParameter param = new
                    SqlParameter("@Salary", salary);
                    cmd.Parameters.Add(param);
                }
                cmd.CommandText = sbCommand.ToString();
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (sdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult SearchWithDynamics()
        {
            string connectionStr = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchEmployees";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (sdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult SearchWithDynamics(string firstName, string lastName, string gender, int salary)
        {
            string connectionStr = _confiq.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                StringBuilder sbCommand = new StringBuilder("select * from Employees where 1 = 1");

                if (firstName != null)
                {
                    sbCommand.Append(" AND FirstName=@FirstName");
                    SqlParameter param = new
                    SqlParameter("@FirstName", firstName);
                    cmd.Parameters.Add(param);
                }
                if (lastName != null)
                {
                    sbCommand.Append(" AND LastName=@LastName");
                    SqlParameter param = new
                    SqlParameter("@LastName", lastName);
                    cmd.Parameters.Add(param);
                }
                if (gender != null)
                {
                    sbCommand.Append(" AND Gender=@Gender");
                    SqlParameter param = new
                    SqlParameter("@Gender", gender);
                    cmd.Parameters.Add(param);
                }
                if (salary != 0)
                {
                    sbCommand.Append(" AND Salary=@Salary");
                    SqlParameter param = new
                    SqlParameter("@Salary", salary);
                    cmd.Parameters.Add(param);
                }
                cmd.CommandText = sbCommand.ToString();
                cmd.CommandType = System.Data.CommandType.Text;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (sdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = sdr["FirstName"].ToString();
                    details.LastName = sdr["LastName"].ToString();
                    details.Gender = sdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(sdr["Salary"]);
                    model.Add(details);
                }

                return View(model);
            }
        }
    }
}
