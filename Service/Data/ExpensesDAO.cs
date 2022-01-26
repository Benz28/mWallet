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

        #region expenses function
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
                string queryString = "INSERT INTO [dbo].[Expenses] ([Date], [Description], [Amount]) VALUES (@date, @desc, @amount)";
                //string queryString = "EXEC InsertExpenses @date = '" + temp.ToString("MM-dd-yyyy") + "', @desc = '"+ data.desc + "', @amount = " + data.amount + ", @type = '" + data.type.ToString().Substring(0,1) + "'";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@date", System.Data.SqlDbType.VarChar, 50).Value = temp.ToString("MM-dd-yyyy HH:mm:ss");
                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 50).Value = data.desc.ToUpper();
                command.Parameters.Add("@amount", System.Data.SqlDbType.Decimal).Value = data.amount;

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    if (UpdateBalance(data.amount, data.type.ToString(), "Sub"))
                    {
                        success = AuditExpenses(data.desc, data.amount, data.type.ToString(), "Insert");
                    }
                }

                return success;
            }
        }

        internal bool RemoveExpenses(string desc, decimal amt)
        {
            bool success = false;
            string type = GetExpensesType(desc, amt);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "DELETE FROM Expenses WHERE Description = @desc AND Amount = @amt";

                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 100).Value = desc;
                command.Parameters.Add("@amt", System.Data.SqlDbType.Decimal).Value = amt;

                connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    if (AuditExpenses(desc, amt, type, "Delete"))
                    {
                        success = UpdateBalance(amt, type, "Add");
                    }
                }
                return success;
            }
        }

        internal string GetExpensesType(string desc, decimal amt)
        {
            string type = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT Type FROM Expenses_AT WHERE Description = @desc AND Amount = @amt AND Action = 'Insert' ORDER BY Date DESC";

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

        #region basic expenses function
        internal bool AuditExpenses(string desc, decimal amt, string type, string action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "INSERT INTO [dbo].[Expenses_AT] ([Date], [Description], [Amount], [Type], [Action]) VALUES (@date, @desc, @amount, @type, @action)";

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
    }
    #endregion
}