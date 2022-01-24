using mWallet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mWallet.Service.Data
{
    public class BalanceDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        internal WalletModel Balance()
        {
            WalletModel data = new WalletModel();

            string queryString = "SELECT * FROM Wallet";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.current_balance = reader.GetDecimal(0);
                        data.bank_balance = reader.GetDecimal(1);
                    }
                    reader.Close();
                }

                return data;
            }
        }
    }
}