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
            SqlCommand Cmd = new SqlCommand();
            SqlDataReader DataReader;

            Cmd.CommandText = @"SELECT * FROM Companies INNER JOIN Addresses 
                                ON Companies.Address = Addresses.Address_ID INNER JOIN Employees 
                                ON Companies.Agent = Employees.Employee_ID";
            Cmd.Connection = SqlConnection;
            SqlConnection.Open();
            DataReader = Cmd.ExecuteReader();

            DataTable CompaniesDataTable = new DataTable();
            CompaniesDataTable.Columns.Add("ID", Type.GetType("System.String"));
            CompaniesDataTable.Columns.Add("Name", Type.GetType("System.String"));

            while (DataReader.Read())
            {
                CompaniesDataTable.Rows.Add();
                int RowNumber = CompaniesDataTable.Rows.Count - 1;
                CompaniesDataTable.Rows[RowNumber]["ID"] = DataReader["Company_ID"].ToString();
                CompaniesDataTable.Rows[RowNumber]["Name"] = DataReader["Name"].ToString();
            }

            SqlConnection.Close();

            CompaniesGridView.DataSource = CompaniesDataTable;
            CompaniesGridView.DataBind();

        }
    }
}