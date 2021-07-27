using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class MenuService : ServiceBase
    {
        private static MenuService _instance;

        private MenuService()
        {
        }

        public static MenuService Instance
        {
            get { return _instance ?? (_instance = new MenuService()); }
            private set { _instance = value; }
        }

        public List<Menu> GetListMenuByTable(int id)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    string query =
                        "SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = " +
                        id;
                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection, query);

                    return (from DataRow item in data.Rows select new Menu(item)).ToList();
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

            return new List<Menu>();
        }
    }
}