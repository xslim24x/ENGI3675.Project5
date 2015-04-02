//-----------------------------------------------------------------------
// <copyright file="ServerConn.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UserLogin.App_Code.ServerConn
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using Npgsql;

    /// <summary>
    /// Class for server connection methods
    /// </summary>
    public static class ServerConn
    {
        /// <summary>
        /// Function to execute incoming SQL queries
        /// </summary>
        /// <param name="sql">Takes an SQL query as a parameter to execute on the database</param>
        /// <returns>Returns data read from executed query</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities",
            Justification = "Risky to change manually")]
        public static DataTable MyQuery(string sql)
        {
            DataTable dt = new DataTable();
            NpgsqlDataReader dr = null;

            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                conn.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return dt;
        }


        /// <summary>
        /// Function utilizing a prepared query to insert data into database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void SafeAdd(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO public.\"systemusers\"(username,hashedpass) VALUES (:username,:hashedpass);";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.Add(
                        new NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                    MD5 crypter = MD5.Create();
                    
                    byte[] hashed = crypter.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password));

                    cmd.Parameters.Add(
                        new NpgsqlParameter("hashedpass", NpgsqlTypes.NpgsqlDbType.Bytea) { Value = hashed });

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    //// conn.Close();    //code analysis error that object gets disposed of automatically
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        public static bool Authenticated(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM systemusers WHERE username=:user";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.Add(
                        new NpgsqlParameter("user", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });
                    
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    string shouldbethis = BitConverter.ToString((byte[])dt.Rows[0]["hashedpass"]).Replace("-","");
                    
                    if (shouldbethis.Equals(password.ToUpper()))
                        return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            return false;
        }
    }
}