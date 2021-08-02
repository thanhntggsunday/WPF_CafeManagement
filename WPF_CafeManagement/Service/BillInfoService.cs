using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class BillInfoService : ServiceBase
    {
        private static BillInfoService _instance;
        private BillInfoService()
        {
        }

        public static BillInfoService Instance
        {
            get
            {
                if (_instance == null) _instance = new BillInfoService();
                return _instance;
            }
            private set { _instance = value; }
        }

        public void DeleteBillInfoByFoodId(int id)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteQuery(sqlConnection, "delete dbo.BillInfo WHERE idFood = " + id);
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

        public List<BillInfo> GetListBillInfo(int id)
        {
            var listBillInfo = new List<BillInfo>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataTable data = DataProvider.Instance.ExecuteQuery(sqlConnection,
                        "SELECT * FROM dbo.BillInfo WHERE idBill = " + id);

                    foreach (DataRow item in data.Rows)
                    {
                        var info = new BillInfo(item);
                        listBillInfo.Add(info);
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

            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteNonQuery(sqlConnection, "USP_InsertBillInfo @idBill , @idFood , @count",
                        new object[] {idBill, idFood, count});
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