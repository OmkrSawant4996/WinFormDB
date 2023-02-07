using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using DataLibrary;

namespace WinFormDB
{
    public partial class Form1 : Form
    {
        EmpDataStore empDataStore;
        public Form1()
        {
            InitializeComponent();
            empDataStore = new EmpDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Emp> empList = empDataStore.GetEmps();
            empDataGrid.DataSource = empList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int num = int.Parse(txtEmpNo.Text);
            Emp emp = empDataStore.GetEmpByNo(num);

            if(emp != null)
            {
                txtEmpNo.Text = emp.EmpNo.ToString();
                txtEmpName.Text = emp.EmpName.ToString();
                txtHireDate.Text = emp.HireDate.ToString();
                txtSalary.Text = emp.Salary.ToString();
            }
            else
            {
                clear();
                MessageBox.Show($"Employee {num} Not Found");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txtEmpNo.Clear();
            txtEmpName.Clear();
            txtHireDate.Clear();
            txtSalary.Clear();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Emp emp = new Emp();
            emp.EmpNo = Convert.ToInt32(txtEmpNo.Text);
            emp.EmpName = txtEmpName.Text;
            emp.HireDate = Convert.ToDateTime(txtHireDate.Text);
            emp.Salary = Convert.ToDecimal(txtSalary.Text);

            int count = 0;
            try
            {
                count = empDataStore.InsertNewEmployee(emp);
                if(count == 1)
                {
                    MessageBox.Show("Record Inserted");
                    clear();
                    empDataGrid.DataSource = empDataStore.GetEmps();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int num = int.Parse(txtEmpNo.Text);
            int count = 0;
            try
            {
                count = empDataStore.DeleteEmployee(num);
                if (count == 1)
                {
                    MessageBox.Show("Record Inserted");
                    clear();
                    empDataGrid.DataSource = empDataStore.GetEmps();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Emp emp = new Emp();
            emp.EmpNo = Convert.ToInt32(txtEmpNo.Text);
            emp.EmpName = txtEmpName.Text;
            emp.HireDate = Convert.ToDateTime(txtHireDate.Text);
            emp.Salary = Convert.ToDecimal(txtSalary.Text);
            try
            {
                int count = 0;
                count = empDataStore.UpdateEmployee(emp);
                if (count == 1 && txtEmpNo.Text.Equals(emp.EmpNo.ToString()))
                {
                    MessageBox.Show("Record Updated Succesfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    empDataGrid.DataSource = empDataStore.GetEmps();
                }
                else
                {
                    MessageBox.Show("You Cannot Update Employee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
