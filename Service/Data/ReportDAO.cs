using mWallet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mWallet.Service.Data
{
    public class ReportDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region Summary Report Function
        internal List<ReportModel> SummaryByMonth()
        {
            List<ReportModel> data = new List<ReportModel>();

            string queryString = "SELECT YEAR(Date), MONTH(Date), SUM(Amount) from Expenses GROUP BY MONTH(Date), YEAR(Date) ORDER BY YEAR(Date), MONTH(Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var dt = new DateTime(2022, reader.GetInt32(1), 1);
                        ReportModel temp = new ReportModel();
                        temp.year = reader.GetInt32(0);
                        temp.month = dt.ToString("MMM");
                        temp.amount = reader.GetDecimal(2);
                        data.Add(temp);
                    }
                    reader.Close();
                }
                return data;
            }
        }
        internal List<ReportModel> SummaryByYear()
        {
            List<ReportModel> data = new List<ReportModel>();

            string queryString = "SELECT YEAR(Date), SUM(Amount) FROM Expenses GROUP BY YEAR(Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReportModel temp = new ReportModel();
                        temp.year = reader.GetInt32(0);
                        temp.amount = reader.GetDecimal(1);

                        data.Add(temp);
                    }
                    reader.Close();
                }
                return data;
            }
        }
        #endregion

        #region Annual Report Function
        internal List<ReportModel> ExpensesByYear(int? year)
        {
            year = year == null ? DateTime.Now.Year : year;
            List<ReportModel> data = new List<ReportModel>();

            string queryString = "SELECT MONTH(Date), SUM(Amount) FROM Expenses WHERE YEAR(Date) = @year GROUP BY MONTH(Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = year;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReportModel temp = new ReportModel();
                        temp.month = reader.GetInt32(0).ToString();
                        temp.amount = reader.GetDecimal(1);

                        data.Add(temp);
                    }
                    reader.Close();
                }
                return data;
            }

        }

        internal List<ReportModel> IncomeByYear(int? year)
        {
            year = year == null ? DateTime.Now.Year : year;
            List<ReportModel> data = new List<ReportModel>();

            string queryString = "SELECT MONTH(Date), SUM(Amount) FROM Income WHERE YEAR(Date) = @year GROUP BY MONTH(Date)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@year", System.Data.SqlDbType.Int).Value = year;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReportModel temp = new ReportModel();
                        temp.month = reader.GetInt32(0).ToString();
                        temp.amount = reader.GetDecimal(1);

                        data.Add(temp);
                    }
                    reader.Close();
                }
                return data;
            }
        }

        internal List<string> TotalYear()
        {
            List<string> data = new List<string>();

            string queryString = "SELECT DISTINCT YEAR(Date) FROM Expenses";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetInt32(0).ToString());
                    }

                    reader.Close();
                }
            }
            return data;
        }
        #endregion
    }
}