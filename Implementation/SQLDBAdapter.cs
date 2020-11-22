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

namespace Implementation
{
    public class SQLDBAdapter:IDBAdapter
    {
        private const string DB_Properties = @"Meta-Inf/SqlProperties.json";

        public IDbConnection getConnection()
        {
            IDbConnection conexion = null;
            try
            {
                string connectionString = createConnectionString();
                conexion = new SqlConnection(connectionString);
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
            builder.Append(";Initial catalog=");
            builder.Append(configuracion.dbname);
            builder.Append(";user=");
            builder.Append(configuracion.user);
            builder.Append(";password=");
            builder.Append(configuracion.password);

            return builder.ToString();
        }

        
    }
}
