using mWallet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mWallet.Service.Data
{
    public class IncomeDAO
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        #region income function
        internal List<IncomeModel> MonthlyIncome(int? month)
        {
            List<IncomeModel> data = new List<IncomeModel>();

            string queryString = "SELECT DAY(Date), UPPER(Description), Amount, Type FROM Income WHERE MONTH(Date) = @month ORDER BY Date";

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
                        IncomeModel temp = new IncomeModel();
                        temp.date = reader.GetInt32(0);
                        temp.desc = reader.GetString(1);
                        temp.amount = reader.GetDecimal(2);
                        temp.type = reader.GetString(3) == "C" ? "Cash" : "Bank";

                        data.Add(temp);
                    }
                    reader.Close();
                }

                return data;
            }
        }

        internal bool InsertIncome(ModifyIncomeModel data)
        {
            bool success = false; // procedure unable to check successful insert
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime temp = DateTime.ParseExact(data.date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                string queryString = "INSERT INTO [dbo].[Income] ([Date], [Description], [Amount], [Type]) VALUES (@date, @desc, @amount, @type)";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = temp.ToString("MM-dd-yyyy HH:mm:ss");
                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 50).Value = data.desc.ToUpper();
                command.Parameters.Add("@amount", System.Data.SqlDbType.Decimal).Value = data.amount;
                command.Parameters.Add("@type", System.Data.SqlDbType.Char).Value = Convert.ToChar(data.type.ToString().Substring(0,1));

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    if (UpdateBalance(data.amount, data.type.ToString(), "Add"))
                    {
                        success = AuditIncome(data.desc, data.amount, data.type.ToString(), "Insert");
                    }
                }
                return success;
            }
        }

        internal bool RemoveIncome(string desc, decimal amt)
        {
            bool success = false;
            string type = GetIncomeType(desc, amt);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "DELETE FROM Income WHERE Description = @desc AND Amount = @amt";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 100).Value = desc;
                command.Parameters.Add("@amt", System.Data.SqlDbType.Decimal).Value = amt;

                connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    if (AuditIncome(desc, amt, type, "Delete"))
                    {
                        success = UpdateBalance(amt, type, "Sub");
                    }
                }
                return success;
            }
        }

        internal string GetIncomeType(string desc, decimal amt)
        {
            string type = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT Type FROM Income_AT WHERE Description = @desc AND Amount = @amt AND Action = 'Insert' ORDER BY Date DESC";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 100).Value = desc;
                command.Parameters.Add("@amt", System.Data.SqlDbType.Decimal).Value = amt;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        type = reader.GetString(0);
                    }
                }
                reader.Close();
                return type;
            }
        }
        #endregion

        #region basic income function
        internal bool AuditIncome(string desc, decimal amt, string type, string action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "INSERT INTO [dbo].[Income_AT] ([Date], [Description], [Amount], [Type], [Action]) VALUES (@date, @desc, @amount, @type, @action)";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 50).Value = desc.ToUpper();
                command.Parameters.Add("@amount", System.Data.SqlDbType.Decimal).Value = amt;
                command.Parameters.Add("@type", System.Data.SqlDbType.Char).Value = Convert.ToChar(type.Substring(0, 1));
                command.Parameters.Add("@action", System.Data.SqlDbType.VarChar, 50).Value = action;

                connection.Open();

                return command.ExecuteNonQuery() > 0;
            }
        }

        internal bool UpdateBalance(decimal amount, string type, string action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                type = type.Substring(0, 1);

                decimal balance = GetBalance(type);
                string queryString = "UPDATE Wallet SET Current_Balance = @balance";

                if (type.Equals("B"))
                {
                    queryString = "UPDATE Wallet SET Bank_Balance = @balance";
                }

                SqlCommand command = new SqlCommand(queryString, connection);

                decimal new_balance = 0;
                if (action.Equals("Sub"))
                {
                    new_balance = balance - amount;
                }
                else if (action.Equals("Add"))
                {
                    new_balance = balance + amount;
                }
                command.Parameters.Add("@balance", System.Data.SqlDbType.Decimal).Value = new_balance;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        internal decimal GetBalance(string type)
        {
            decimal data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Wallet";

                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        data = type == "C" ? reader.GetDecimal(0) : reader.GetDecimal(1);
                    }
                }
                reader.Close();
                return data;
            }
        }
        #endregion
    }
}