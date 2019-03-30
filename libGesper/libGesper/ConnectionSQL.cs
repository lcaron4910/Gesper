using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace libGesper
{
     static class ConnectionSQL
    {
        static private MySqlConnection cnx;
        static private string sConnection;

        static ConnectionSQL()
        {
            sConnection = "host=localhost; database=gesper; user=root; password=siojjr";
            cnx = new MySqlConnection(sConnection);
        }
         static public MySqlConnection GetConnection()
        {
            return cnx;
        }
    }
}
