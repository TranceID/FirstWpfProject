using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows;

namespace BudgetLib
{
    public static class SqlDataAccess
    {
        // NOTE: Dapper.Contrib has extension methods to simplify a lot of things

        #region User-related SQL Methods
        public static void CreateNewAccount(string userName, string pass)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"INSERT into Users (username, password) VALUES (@userName, @pass)", new { userName, pass });
                /*
                try
                {
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\nCreate New Account Error");
                }*/
            }
        }

        // Loads account after you verify that it exists.
        public static User LoadUserAccount(string userName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.QuerySingle<User>($"SELECT * FROM Users WHERE username=@userName", new { userName });
                    return output;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return null; // <-- bad, i think
                }
            }
        }

        // Checks if an account exists with username given.
        public static bool CheckIfUserExists(string userName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    User output = cnn.QuerySingle<User>($"SELECT username FROM Users WHERE username=@userName", new { userName });
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        // Checking if password is correct.
        public static bool VerifyPassword(string userName, string passwordInput)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QuerySingle<string>($"SELECT password FROM Users WHERE username=@userName", new { userName });
                if (passwordInput == output)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Updates database of changes to account information (password atm)
        public static void UpdateUserPassword(string newPassword)
        {
            string userName = StaticObjects.user.UserName;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE Users set password=@newPassword WHERE username=@userName", new { newPassword, userName });
            }
        }

        #endregion

        #region Bill-related SQL Methods

        // Adds a new bill to the database
        public static void AddUserBill(Bill newBill)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"INSERT into Bills(newBill)", newBill); // Todo - This may crash due to SQL logic
            }
        }

        // Updates user's bill in the database
        public static void UpdateUserBill(IBill bill)
        {
            DateTime newDate; // Advance depending on payment intervals ( could add a list of Dates as a property, most companies give you all your due dates ahead of time ) 

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bill WHERE BillID = @bill.BillID", bill);
            }
        }

        // Method for moving a paid off bill into a different archived DB and removing old data
        public static void ArchiveBill(IBill bill)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"DELETE Bill where BillID = @bill.BillID", bill);
            }
        }

        #endregion

        #region Connection String
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        #endregion
    }
}
