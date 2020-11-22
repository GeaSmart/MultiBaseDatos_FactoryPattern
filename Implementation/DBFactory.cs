using Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imlpementation
{
    public class DBFactory
    {
        private const string APP_settings = @"appsettings.json";

        public static IDBAdapter GetDBAdapter(DBType engine)//Cuando uno quiere indicarle directamente el db engine y no obtenerla de appsettings.json
        {
            switch (engine)
            {
                case DBType.SQLServer:
                    return new SQLDBAdapter();                    
                case DBType.MySQL:
                    return new MySQLDBAdapter();
                default:
                    throw new Exception("Oh crash");
            }
        }

        public static IDBAdapter GetDefaultDBAdapter()
        {
            string jsonText;
            using (StreamReader reader = new StreamReader(APP_settings))
            {
                jsonText = reader.ReadToEnd();
            }

            dynamic configuracion = JsonConvert.DeserializeObject(jsonText);            
            string BDClassName = string.Format("{0}.{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, configuracion.DefaultDBClass);

            return (IDBAdapter)Activator.CreateInstance(Type.GetType(BDClassName));            
        }
    }
}
