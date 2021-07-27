using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WPF_CafeManagement.Common;

namespace WPF_CafeManagement.SQL
{
    public class DataProvider
    {
        public const string END_OF_LINE = "\r\n";
        public const string COMMA = ",";
        public const string ROUND_START = "(";
        public const string ROUND_END = ")";

        public const string CONNECTION_STR = "Data Source=.\\sqlexpress;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        private static DataProvider _instance; // Ctrl + R + E
        
        private DataProvider()
        {
        }

        public static DataProvider Instance
        {
            get { return _instance ?? (_instance = new DataProvider()); }
        }

        public DataTable ExecuteQuery(SqlConnection sqlConnection, string query, object[] parameter = null, SqlTransaction sqlTransaction = null)
        {
            var data = new DataTable();
            var connection = sqlConnection;
          
            using (var cmd = new SqlCommand(query, connection))
            {
                if (sqlTransaction != null)
                {
                    cmd.Transaction = sqlTransaction;
                }
              
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    GetParametersArrayForCommand(cmd, listPara, parameter);
                }

                using (var sdr = cmd.ExecuteReader())
                {
                    //Load DataReader into the DataTable.
                    data.Load(sdr);
                }
               
            }

            return data;
        }

        public int ExecuteNonQuery(SqlConnection sqlConnection, string query, object[] parameter = null, SqlTransaction sqlTransaction = null)
        {
            var data = 0;
            var connection = sqlConnection;
         
            using (var command = new SqlCommand(query, connection))
            {
                if (sqlTransaction != null)
                {
                    command.Transaction = sqlTransaction;
                }
                if (parameter != null)
                {
                    var listPara = query.Split(' ');
                    GetParametersArrayForCommand(command, listPara, parameter);
                }

                data = command.ExecuteNonQuery();
            }
           
            return data;
        }

        public object ExecuteScalar(SqlConnection sqlConnection, string query, object[] parameter = null, SqlTransaction sqlTransaction = null)
        {
            object data = 0;
            var connection = sqlConnection;
         
            using (var command = new SqlCommand(query, connection))
            {
                if (sqlTransaction != null)
                {
                    command.Transaction = sqlTransaction;
                }
                if (parameter != null)
                {
                    var listPara = query.Split(' ');
                    GetParametersArrayForCommand(command, listPara, parameter);
                }

                data = command.ExecuteScalar();
            }

            return data;
        }

        public DataTable ExecuteQueryBySqlParamArray(SqlConnection sqlConnection, string query, SqlParameter[] parameter = null, SqlTransaction sqlTransaction = null)
        {
            var data = new DataTable();
            var connection = sqlConnection;
          
            using (var cmd = new SqlCommand(query, connection))
            {
                if (sqlTransaction != null)
                {
                    cmd.Transaction = sqlTransaction;
                }

                if (parameter != null)
                {
                    cmd.Parameters.AddRange(parameter);
                }

                using (var sdr = cmd.ExecuteReader())
                {
                    //Load DataReader into the DataTable.
                    data.Load(sdr);
                }
                
            }

            return data;
        }

        private void GetParametersArrayForCommand(SqlCommand command, string[] arrParamsName, 
            object[] arrParamValue)
        {
            int valueIndex = 0;

            for (int i = 0; i < arrParamsName.Count(); i++)
            {
                var item = arrParamsName[i];

                if (arrParamsName[i].Contains('@'))
                {
                    item = item.Replace(END_OF_LINE, string.Empty);
                    item = item.Replace(COMMA, string.Empty);
                    item = item.Replace(ROUND_START, string.Empty);
                    item = item.Replace(ROUND_END, string.Empty);

                    arrParamValue[valueIndex] = arrParamValue[valueIndex] ?? DBNull.Value;
                    command.Parameters.AddWithValue(item, arrParamValue[valueIndex]);
                    valueIndex++;
                }
            }
        }
    }
}