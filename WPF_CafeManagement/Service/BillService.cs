using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using WPF_CafeManagement.DTO;
using WPF_CafeManagement.SQL;

namespace WPF_CafeManagement.Service
{
    public class BillService : ServiceBase
    {
        private static BillService _instance;

        private BillService()
        {
        }

        public static BillService Instance
        {
            get
            {
                if (_instance == null) _instance = new BillService();
                return _instance;
            }
            private set { _instance = value; }
        }

        /// <summary>
        ///     Thành công: bill ID
        ///     thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckBillIdByTableId(int id)
        {
            var data = new DataTable();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    data =
                        DataProvider.Instance.ExecuteQuery(sqlConnection,
                            "SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0");
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

            if (data.Rows.Count > 0)
            {
                var bill = new Bill(data.Rows[0]);
                return bill.Id;
            }

            return -1;
        }

        public void CheckOut(int id, int discount = 0, float totalPrice = 0)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount +
                           ", totalPrice = " + totalPrice + " WHERE id = " + id;

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteNonQuery(sqlConnection, query);
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
               

        public void InsertBill(int id)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    DataProvider.Instance.ExecuteNonQuery(sqlConnection, "exec USP_InsertBill @idTable",
                        new object[] {id});
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

        public List<Bill> GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            List<Bill> bills = new List<Bill>();
            List<Bill> billsTotalPrice = new List<Bill>();

            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    // "exec USP_GetListBillByDate @checkIn , @checkOut"
                    string billTablesQuery = @"SELECT [id]
                                      ,[DateCheckIn]
                                      ,[DateCheckOut]
                                      ,[idTable]
                                      ,[status]
                                      ,[discount]
                                      ,[totalPrice]
                                  FROM [Bill]
                                  WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut";

                    string billTotalPriceQuery = @"SELECT b.id , SUM(f.price) AS totalPrice
                                                    FROM BillInfo AS bi
                                                    LEFT JOIN Bill AS b
                                                    ON bi.idBill = b.id
                                                    LEFT JOIN TableFood AS tf ON tf.id = b.idTable
                                                    LEFT JOIN Food AS f ON f.id = bi.idFood
                                                    WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut
                                                    GROUP BY b.id";

                    sqlConnection.Open();

                    var result = DataProvider.Instance.ExecuteQuery(sqlConnection, billTablesQuery, new Object[] {checkIn, checkOut});
                    var result2 = DataProvider.Instance.ExecuteQuery(sqlConnection, billTotalPriceQuery, new Object[] { checkIn, checkOut });

                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        var item = result.Rows[i];

                        bills.Add(new Bill(item));
                    }

                    for (int i = 0; i < result2.Rows.Count; i++)
                    {
                        var item = result2.Rows[i];

                        billsTotalPrice.Add(Bill.CreateBillTotalPrice(item));
                    }

                    for (int i = 0; i < bills.Count; i++)
                    {
                        for (int j = 0; j < billsTotalPrice.Count; j++)
                        {
                            if (bills[i].Id == billsTotalPrice[j].Id)
                            {
                                bills[i].TotalPrice = billsTotalPrice[j].TotalPrice;
                            }
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
            return bills;
        }

        public DataTable GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    return DataProvider.Instance.ExecuteQuery(sqlConnection,
                        "exec USP_GetListBillByDateAndPage @checkIn , @checkOut , @page",
                        new object[] {checkIn, checkOut, pageNum});
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

            return new DataTable();
        }

        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    var numberBill =
                        (int)
                            DataProvider.Instance.ExecuteScalar(sqlConnection, "exec USP_GetNumBillByDate @checkIn , " +
                                                                               "@checkOut",
                                new object[] {checkIn, checkOut});
                    return numberBill;
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
            return -1;
        }

        public int GetMaxIdBill()
        {
            using (var sqlConnection = new SqlConnection(DataProvider.CONNECTION_STR))
            {
                try
                {
                    sqlConnection.Open();
                    return (int) DataProvider.Instance.ExecuteScalar(sqlConnection, "SELECT MAX(id) FROM dbo.Bill");
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

            return -1;
        }
    }
}