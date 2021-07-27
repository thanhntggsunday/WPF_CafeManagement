using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Documents;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class AccountService : ServiceBase
    {
        private static AccountService _instance;

        private AccountService()
        {
        }

        public static AccountService Instance
        {
            get
            {
                if (_instance == null) _instance = new AccountService();
                return _instance;
            }
            private set { _instance = value; }
        }

        public bool Login(string userName, string passWord)
        {
            byte[] temp = Encoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            //var list = hasData.ToString();
            //list.Reverse();

            string query = "USP_Login @userName , @passWord";

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();

                    DataTable result = DataProvider.Instance.ExecuteQuery(sqlConnection, query,
                        new object[] {userName, hasPass /*list*/});

                    return result.Rows.Count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return false;
        }

        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            string query = "exec USP_UpdateAccount @userName , @displayName , @password , @newPassword";

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                sqlConnection.Open();
                SqlTransaction sqlTransaction = BeginTransaction(sqlConnection);

                try
                {
                    int result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, query,
                        new object[] {userName, displayName, pass, newPass}, sqlTransaction);

                    Commit(sqlConnection, sqlTransaction);
                    return result > 0;
                }
                catch (Exception ex)
                {
                    RollBack(sqlConnection, sqlTransaction);
                    Console.WriteLine(ex.Message);
                }
            }

            return false;
        }

        public List<Account> GetListAccount()
        {
            var accountsList = new List<Account>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();

                    var result = DataProvider.Instance.ExecuteQuery(sqlConnection,
                        "SELECT * FROM dbo.Account");

                    if (result != null)
                    {
                        for (int i = 0; i < result.Rows.Count; i++)
                        {
                            var row = result.Rows[i];
                            var acc = new Account(row);
                            accountsList.Add(acc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return accountsList;
        }

        public Account GetAccountByUserName(string userName)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();

                    DataTable data =
                        DataProvider.Instance.ExecuteQuery(sqlConnection,
                            "Select * from account where userName = '" + userName + "'");

                    foreach (DataRow item in data.Rows)
                    {
                        return new Account(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return null;
        }

        public bool InsertAccount(string name, string displayName, int type)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        string.Format(
                            "INSERT dbo.Account ( UserName, DisplayName, Type, password )VALUES  ( N'{0}', N'{1}', {2}, N'{3}')",
                            name, displayName, type, "1962026656160185351301320480154111117132155");
                    int result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, query);

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return false;
        }

        public bool UpdateAccount(string name, string displayName, int type)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        string.Format(
                            "UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name,
                            displayName, type);
                    int result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, query);

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return false;
        }

        public bool DeleteAccount(string name)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query = string.Format("Delete Account where UserName = N'{0}'", name);
                    int result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, query);

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return false;
        }

        public bool ResetPassword(string name)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        string.Format(
                            "update account set password = N'1962026656160185351301320480154111117132155' where UserName = N'{0}'",
                            name);
                    int result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, query);

                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return false;
        }
    }
}