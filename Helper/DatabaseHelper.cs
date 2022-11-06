using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Configuration;

namespace TransportManagement.Helper
{
    public class DatabaseHelper
    {
        private readonly IAppConfiguration _configuration;
        private readonly string DBConnectionString;
        public DatabaseHelper(IAppConfiguration _configuration)
        {
            this._configuration = _configuration;
            DBConnectionString = this._configuration.DBConnectionString;
        }

        public List<T> GetData<T>(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(DBConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                //SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                
                List<T> data = new List<T>();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                foreach (DataRow item in dt.Rows)
                {
                    data.AddRange(JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(item.Table), settings));
                    break;
                }

                return data;
            }
        }

        public bool UpdateData(string updateQuery)
        {
            using (SqlConnection connection = new SqlConnection(DBConnectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
