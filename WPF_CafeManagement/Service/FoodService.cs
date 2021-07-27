using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class FoodService : ServiceBase
    {
        private static FoodService _instance;

        private FoodService()
        {
        }

        public static FoodService Instance
        {
            get
            {
                if (_instance == null) _instance = new FoodService();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<Food> GetFoodByCategoryId(int id)
        {
            var list = new List<Food>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "select * from Food where idCategory = " + id;

                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query);

                    list.AddRange(from DataRow item in data.Rows select new Food(item));

                    return list;
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

            return list;
        }

        public List<Food> GetListFood()
        {
            var list = new List<Food>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query = "select * from Food";

                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query);

                    list.AddRange(from DataRow item in data.Rows select new Food(item));

                    return list;
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

            return list;
        }

        public List<Food> SearchFoodByName(string name)
        {
            var list = new List<Food>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    
                    name = "%" + name + "%";
                    const string query = "select * from Food where name like @name";
                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query, new Object[] {name});

                    list.AddRange(from DataRow item in data.Rows select new Food(item));

                    return list;
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

            return list;
        }

        public bool InsertFood(string name, int id, float price)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        string.Format("INSERT dbo.Food ( name, idCategory, price )VALUES  ( N'{0}', {1}, {2})", name,
                            id, price);
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

        public bool UpdateFood(int idFood, string name, int id, float price)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        string.Format(
                            "UPDATE dbo.Food SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name,
                            id, price, idFood);
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

        public bool DeleteFood(int idFood)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    BillInfoService.Instance.DeleteBillInfoByFoodId(idFood);

                    string query = string.Format("Delete Food where id = {0}", idFood);
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