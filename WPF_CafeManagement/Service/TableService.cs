using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using WPF_CafeManagement.Common;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class TableService : ServiceBase
    {
        private static TableService _instance;

        public static int TableWidth = 90;
        public static int TableHeight = 90;

        private TableService()
        {
        }

        public static TableService Instance
        {
            get
            {
                if (_instance == null) _instance = new TableService();
                return _instance;
            }
            private set { _instance = value; }
        }

        public void SwitchTable(int id1, int id2)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteQuery(sqlConnection, "USP_SwitchTabel @idTable1 , @idTabel2",
                        new object[] {id1, id2});
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
        }

        public List<Table> LoadTableList()
        {
            var tableList = new List<Table>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var data = DataProvider.Instance.ExecuteQuery(sqlConnection, "USP_GetTableList");

                    tableList.AddRange(from DataRow item in data.Rows select new Table(item));

                    return tableList;
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
            return tableList;
        }

        public void UpdateTableStatus(int id, bool isFull = true)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    var sql = @"UPDATE TableFood
                                SET status = @status
                                WHERE id = @id";
                    sqlConnection.Open();

                    var status = Constants.TABLE_EMPTY_STATUS;

                    if (isFull)
                    {
                        status = Constants.TABLE_FULL_STATUS;
                    }

                    DataProvider.Instance.ExecuteQuery(sqlConnection, sql, new object[] { status, id });
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
        }

        public void UpdateTable(Table table)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    var sql = @"UPDATE TableFood
                                SET name = @name
                                WHERE id = @id";
                    
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteQuery(sqlConnection, sql, new object[] {table.Name, table.Id });
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
        }


        public void InsertTable(Table table)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    var sql = @"Insert Into TableFood (name, status) values (@name, @status)";

                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteQuery(sqlConnection, sql, new object[] { table.Name, table.Status });
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
        }



        public List<Table> GetManyTableByName(string name)
        {
            var tableList = new List<Table>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();

                    name = "%" + name + "%";
                    const string query = "select * from TableFood where name like @name";
                    var data = DataProvider.Instance.ExecuteQuery(sqlConnection, query, new Object[] {name});

                    tableList.AddRange(from DataRow item in data.Rows select new Table(item));

                    return tableList;
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
            return tableList;
        }

        public void DeleteTable(int tableId)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var sql = @"DELETE FROM TableFood WHERE id = @id";

                    var result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, sql, new Object[] { tableId });
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
        }

    }
}