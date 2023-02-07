using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataLibrary
{
    //CRUD Operation
    public class EmpDataStore
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public EmpDataStore(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        //Method witch return all employees
        public List<Emp> GetEmps()
        {
            try
            {
                string sqlConnection = "select empno, ename, hiredate, sal from emp";
                command = new SqlCommand(sqlConnection, connection);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                List<Emp> empList = new List<Emp>();
                reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Emp emp = new Emp();
                    emp.EmpNo = (int)reader["empno"];
                    emp.EmpName = reader["ename"].ToString();
                    //emp.HireDate = (DateTime?)(reader.IsDBNull(2) ? null : reader["hiredate"]);
                    //emp.Salary = (decimal?)(reader.IsDBNull(3) ? null : reader["sal"]);
                    emp.HireDate = reader["hiredate"] as DateTime?;
                    emp.Salary = reader["sal"] as decimal?;

                    empList.Add(emp);
                }

                return empList;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public Emp GetEmpByNo(int empID)
        {
            try
            {
                string sqlConnection = $"select empno, ename, hiredate, sal from emp where empno = @empID";
                command = new SqlCommand(sqlConnection, connection);
                command.Parameters.AddWithValue("@empID", empID);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Emp emp = new Emp();
                    emp.EmpNo = (int)reader["empno"];
                    emp.EmpName = reader["ename"].ToString();
                    //emp.HireDate = (DateTime?)(reader.IsDBNull(2) ? null : reader["hiredate"]);
                    //emp.Salary = (decimal?)(reader.IsDBNull(3) ? null : reader["sal"]);
                    emp.HireDate = reader["hiredate"] as DateTime?;
                    emp.Salary = reader["sal"] as decimal?;

                    return emp;
                }

                return null; ;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int InsertNewEmployee(Emp emp)
        {
            try
            {
                string insertNewEmployee = $"insert into EMP (EMPNO, ENAME, HIREDATE, SAL) values (@empID, @empName, @HireDate, @Sal)";
                command = new SqlCommand(insertNewEmployee, connection);
                command.Parameters.AddWithValue("@empID", emp.EmpNo);
                command.Parameters.AddWithValue("@empName", emp.EmpName);
                command.Parameters.AddWithValue("@HireDate", emp.HireDate);
                command.Parameters.AddWithValue("@Sal", emp.Salary);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int count = command.ExecuteNonQuery();

                return count; 
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int DeleteEmployee(int empID)
        {
            try
            {
                string deleteEmployee = $"delete from EMP where EMPNO = @empID";
                command = new SqlCommand(deleteEmployee, connection);
                command.Parameters.AddWithValue("@empID", empID);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int count = command.ExecuteNonQuery();

                return count; ;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int UpdateEmployee(Emp emp)
        {
            try
            {
                string updateEmployee = $"UPDATE EMP SET ENAME= @empName, HIREDATE = @HireDate, SAL = @Sal WHERE EMPNO = @empID";
                command = new SqlCommand(updateEmployee, connection);
                command.Parameters.AddWithValue("@empID", emp.EmpNo);
                command.Parameters.AddWithValue("@empName", emp.EmpName);
                command.Parameters.AddWithValue("@HireDate", emp.HireDate);
                command.Parameters.AddWithValue("@Sal", emp.Salary);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                int count = command.ExecuteNonQuery();

                return count;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
