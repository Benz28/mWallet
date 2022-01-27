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
        internal List<SummaryReportModel> SummaryByMonth()
        {
            List<SummaryReportModel> data = new List<SummaryReportModel>();

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
                        SummaryReportModel temp = new SummaryReportModel();
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
        internal List<SummaryReportModel> SummaryByYear()
        {
            List<SummaryReportModel> data = new List<SummaryReportModel>();

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
                        SummaryReportModel temp = new SummaryReportModel();
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

    }
}