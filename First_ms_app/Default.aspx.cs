using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Configuration;


namespace First_ms_app
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection SqlConnection = new SqlConnection(ConnectionString);

            SqlCommand ReadCompaniesCmd = new SqlCommand();            
            SqlDataReader CompaniesDataReader;
            

            ReadCompaniesCmd.CommandText = @"SELECT * FROM Companies INNER JOIN Addresses 
                                ON Companies.Address = Addresses.Address_ID INNER JOIN Employees 
                                ON Companies.Agent = Employees.Employee_ID";
            ReadCompaniesCmd.Connection = SqlConnection;
            
            SqlConnection.Open();
            CompaniesDataReader = ReadCompaniesCmd.ExecuteReader();          

            DataTable CompaniesDataTable = new DataTable();
            CompaniesDataTable.Columns.Add("ID", Type.GetType("System.String"));
            CompaniesDataTable.Columns.Add("Name", Type.GetType("System.String"));
            CompaniesDataTable.Columns.Add("Address", Type.GetType("System.String"));
            CompaniesDataTable.Columns.Add("Agent", Type.GetType("System.String"));
            CompaniesDataTable.Columns.Add("Employees", Type.GetType("System.String"));

            while (CompaniesDataReader.Read())
            {
                CompaniesDataTable.Rows.Add();
                int RowNumber = CompaniesDataTable.Rows.Count - 1;
                string CompanyID = CompaniesDataReader["Company_ID"].ToString();
                string CompanyName = CompaniesDataReader["Name"].ToString();

                CompaniesDataTable.Rows[RowNumber]["ID"] = CompanyID;
                CompaniesDataTable.Rows[RowNumber]["Name"] = CompanyName;

                string Address = CompaniesDataReader["Address_1"].ToString() + ", "
                                    + CompaniesDataReader["Address_2"].ToString();
                CompaniesDataTable.Rows[RowNumber]["Address"] = Address;

                string AgentFirstName = CompaniesDataReader["First_name"].ToString();
                string AgentLastName = CompaniesDataReader["Last_name"].ToString();
                string AgentPhoneNumber = CompaniesDataReader["Phone_Number"].ToString();
                string AgentSpecialization = CompaniesDataReader["Specialization"].ToString();
                string Agent = AgentFirstName + " " 
                        + AgentLastName + ", "
                        + AgentPhoneNumber + ", " 
                        + AgentSpecialization + ", ";
                CompaniesDataTable.Rows[RowNumber]["Agent"] = Agent;
            }
            SqlConnection.Close();

            // Read Employeers
            SqlCommand ReadEmployeesCmd = new SqlCommand();
            SqlDataReader EmployeesDataReader;
            ReadEmployeesCmd.CommandText = @"SELECT * FROM Companies 
                                JOIN Companies_Employees ON Company_ID=Companies_Employees.Company
                                JOIN Employees ON Companies_Employees.Employee=Employee_ID";
            ReadEmployeesCmd.Connection = SqlConnection;
            SqlConnection.Open();
            EmployeesDataReader = ReadEmployeesCmd.ExecuteReader();
            while (EmployeesDataReader.Read())
            {
                string EmployeeCompanyID = EmployeesDataReader["Company"].ToString();
                string EmployeeFirstName = EmployeesDataReader["First_name"].ToString();
                string EmployeeLastName = EmployeesDataReader["Last_name"].ToString();
                string EmployeePhoneNumber = EmployeesDataReader["Phone_number"].ToString();
                string EmaployeeSpecialization = EmployeesDataReader["Specialization"].ToString();
                foreach (DataRow Row in CompaniesDataTable.Rows)
                {
                    if (EmployeeCompanyID.ToString() == Row["ID"].ToString())
                    {
                        Row["Employees"] += EmaployeeSpecialization + ", "
                                            + EmployeeFirstName + " "
                                            + EmployeeLastName + ", "
                                            + EmployeePhoneNumber + " | ";
                    }
                }
            }
            SqlConnection.Close();

            CompaniesGridView.DataSource = CompaniesDataTable;
            CompaniesGridView.DataBind();
        }
    }
}