﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using TransportManagement.Models;
using Microsoft.Data.SqlClient;
using TransportManagement.Configuration;
using System.Data;
using Newtonsoft.Json;

namespace TransportManagement.Helper
{
    public class ServiceContext
    {
        private readonly IAppConfiguration _configuration;
        private readonly DatabaseHelper dbHelper;

        public ServiceContext(IAppConfiguration appConfiguration)
        {
            _configuration = appConfiguration;
            dbHelper = new DatabaseHelper(appConfiguration);
        }

        //public static readonly string ConnectionString = new ConfigurationManager().GetConnectionString("Database");
        public static readonly string ConnectionString = @"Data Source=DESKTOP-K17OPLG\SQLEXPRESS;Initial Catalog=TransportManagement;Integrated Security=True;";

        public void CreateInvoice(Dictionary<string, string> data)
        {
            string queryString = "Insert into TransportOrderInformation(";

            foreach (var item in data.Keys)
            {
                queryString += $"{item},";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);

            queryString += ") values(";

            foreach (var item in data.Values)
            {
                queryString += $"'{item}',";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);

            queryString += ")";

            dbHelper.UpdateData(queryString);

        }

        public List<TransportOrderModel> GetInvoicesByDate(string fromDate, string toDate)
        {
            string queryString = "select * from TransportOrderInformation where InvoiceDate >= '" + fromDate + "' and InvoiceDate <= '"+toDate+"'";

            List<TransportOrderModel> invoices = dbHelper.GetData<TransportOrderModel>(queryString);

            return invoices;
        }

        public TransportOrderModel GetInvoice(string key, string value)
        {
            
                string queryString = $"select * from TransportOrderInformation where {key} = '{value}'" ;
                //string queryString = "select * from TransportOrderInformation " ;

                TransportOrderModel transportOrderModel = dbHelper.GetData<TransportOrderModel>(queryString)?.FirstOrDefault();

                //SqlCommand command = new SqlCommand(queryString, connection);

                //connection.Open();

                //SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                ////SqlDataReader reader = command.ExecuteReader();
                //DataTable dt = new DataTable();
                //dataAdapter.Fill(dt);

                //foreach (DataRow item in dt.Rows)
                //{
                //    string test = JsonConvert.SerializeObject(item.Table);
                //    TransportOrderModel transportOrderModel1 = JsonConvert.DeserializeObject<List<TransportOrderModel>>(JsonConvert.SerializeObject(item.Table)).FirstOrDefault();
                //}

                //TransportOrderModel transportOrderModel = new TransportOrderModel();

                return transportOrderModel;
        }

        public void UpdateInvoiceById(string key, string value, Dictionary<string,string> data)
        {
            string updateQuery = "Update TransportOrderInformation set ";

            foreach (KeyValuePair<string,string> item in data)
            {
                updateQuery += $"{item.Key}='{item.Value}',";
            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 1);

            updateQuery += $"where {key}='{value}'";

            dbHelper.UpdateData(updateQuery);

        }
    }
}