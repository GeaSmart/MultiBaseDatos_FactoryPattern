//using DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace Implementation
{
    public class MySQLDBAdapter:IDBAdapter
    {
        private const string DB_Properties = @"Meta-Inf/MySqlProperties.json";

        public IDbConnection getConnection()
        {
            IDbConnection conexion = null;
            try
            {
                string connectionString = createConnectionString();
                conexion = new MySqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return conexion;
        }

        private string createConnectionString()
        {
            string jsonText;
            using(StreamReader reader=new StreamReader(DB_Properties))
            {
                jsonText = reader.ReadToEnd();
            }

            dynamic configuracion = JsonConvert.DeserializeObject(jsonText);

            StringBuilder builder = new StringBuilder();

            builder.Append("server=");
            builder.Append(configuracion.host);
            builder.Append(";database=");
            builder.Append(configuracion.dbname);
            builder.Append(";uid=");
            builder.Append(configuracion.user);
            builder.Append(";password=");
            builder.Append(configuracion.password);
            builder.Append(";");

            return builder.ToString();
        }

        
    }
}
