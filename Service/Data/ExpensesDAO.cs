using mWallet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mWallet.Service.Data
{
    public class ExpensesDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        internal List<ExpensesModel> MonthlyExpenses(int? month)
        {
            List<ExpensesModel> data = new List<ExpensesModel>();

            string queryString = "SELECT DAY(Date), UPPER(Description), Amount FROM Expenses WHERE MONTH(Date) = @month ORDER BY Date";

            if (month == null)
            {
                month = DateTime.Now.Month;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@month", System.Data.SqlDbType.Int).Value = month;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ExpensesModel temp = new ExpensesModel();
                        temp.date = reader.GetInt32(0);
                        temp.desc = reader.GetString(1);
                        temp.amount = reader.GetDecimal(2);

                        data.Add(temp);
                    }
                    reader.Close();
                }

                return data;
            }
        }

        internal bool InsertExpenses(ModifyExpensesModel data)
        {
            bool success = false; // procedure unable to check successful insert
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime temp = DateTime.ParseExact(data.date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string queryString = "EXEC InsertExpenses @date = '" + temp.ToString("MM-dd-yyyy") + "', @desc = '"+ data.desc + "', @amount = " + data.amount + ", @type = '" + data.type.ToString().Substring(0,1) + "'";

                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                command.ExecuteNonQuery();
                return success;
            }
        }
    }
}