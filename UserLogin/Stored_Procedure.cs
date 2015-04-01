using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace UserLogin
{
    public class Stored_Procedure
    {
        public static void INSERT (Model_Table newuser)
        {
            //NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;");
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=postgres;Password=astronauta;Database=Assignment5;");
            conn.Open();
            
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO authentication (username,password_user) values (:username, :password)",conn);

            cmd.Parameters.Add(new NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text));
            cmd.Parameters.Add(new NpgsqlParameter("password", NpgsqlTypes.NpgsqlDbType.Text));
            cmd.Prepare();

            cmd.Parameters[0].Value = newuser.username;
            cmd.Parameters[1].Value = newuser.password;

            cmd.ExecuteNonQuery();

        }
    }
}