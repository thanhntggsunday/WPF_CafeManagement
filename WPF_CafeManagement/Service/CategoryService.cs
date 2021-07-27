using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class CategoryService : ServiceBase
    {
        private static CategoryService _instance;

        private CategoryService()
        {
        }

        public static CategoryService Instance
        {
            get
            {
                if (_instance == null) _instance = new CategoryService();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<Category> GetListCategory()
        {
            var list = new List<Category>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    const string query = "select * from FoodCategory";

                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query);

                    foreach (DataRow item in data.Rows)
                    {
                        var category = new Category(item);
                        list.Add(category);
                    }

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

        public Category GetCategoryById(int id)
        {
            Category category = null;

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var query = "select * from FoodCategory where id = " + id;

                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query);

                    foreach (DataRow item in data.Rows)
                    {
                        category = new Category(item);
                        return category;
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

            return category;
        }

        public void UpdateCategory(Category category)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var sql = @"UPDATE FoodCategory
                                SET name = @name
                                WHERE id = @id";

                    var result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, sql, new Object[] { category.Name, category.Id});
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

        public void InsertCategory(Category category)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var sql = @"INSERT INTO FoodCategory (name)
                                VALUES (@name)";

                    var result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, sql, new Object[] { category.Name });
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

        public void DeleteCategory(int cateId)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var sql = @"DELETE FROM FoodCategory WHERE id = @id";

                    var result = DataProvider.Instance.ExecuteNonQuery(sqlConnection, sql, new Object[] { cateId });
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

        public List<Category> GetListCategoryByName(string name)
        {
            var list = new List<Category>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();

                    name = "%" + name + "%";
                    const string query = "select * from FoodCategory where name like @name";

                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query, new object[] {name});

                    foreach (DataRow item in data.Rows)
                    {
                        var category = new Category(item);
                        list.Add(category);
                    }

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
    }
}