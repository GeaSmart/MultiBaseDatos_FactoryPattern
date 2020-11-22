using Imlpementation;
using Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace DAO
{
    public class ProductoDAO
    {
        private IDBAdapter dbAdapter;

        public ProductoDAO()
        {
            dbAdapter = DBFactory.GetDefaultDBAdapter();
        }

        public List<Producto> findAllProducts()
        {            
            IDbConnection conexion = dbAdapter.getConnection();
            List<Producto> listado = new List<Producto>();
            
            try
            {
                IDbCommand statement = conexion.CreateCommand();
                statement.CommandText = "select IdProducto,Nombre,Precio from producto";
                statement.CommandType = CommandType.Text;

                IDataReader reader = statement.ExecuteReader();

                while (reader.Read())
                {
                    listado.Add(new Producto { IdProducto = (int)reader["IdProducto"], Nombre = reader["Nombre"].ToString(), Precio = (decimal)reader["Precio"] });
                }

                reader.Close();
                conexion.Close();

                return listado;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
