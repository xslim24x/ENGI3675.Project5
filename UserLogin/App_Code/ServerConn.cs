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
        /// Use the stored procedure to add users to the system
        /// the database will handle encrypting the password
        /// </summary>
        /// <param name="name">plaintext username</param>
        /// <param name="password">plaintext password but will be stored as a md5 hash</param>
        public static void SpAdd(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                            "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT addUser(:username,:plaintext);";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.Add(
                        new NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                    cmd.Parameters.Add(
                        new NpgsqlParameter("plaintext", NpgsqlTypes.NpgsqlDbType.Text) { Value = password });

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        // Not Used anymore but this is how to do it safely:
        /*
        public static void SafeSpAdd(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                            "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT addUser(:username,:hashedpass);";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

                    cmd.Parameters.Add(
                        new NpgsqlParameter("username", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                    MD5 crypter = MD5.Create();

                    byte[] hashed = crypter.ComputeHash(System.Text.Encoding.Default.GetBytes(password));

                    cmd.Parameters.Add(
                        new NpgsqlParameter("hashedpass", NpgsqlTypes.NpgsqlDbType.Bytea) { Value = hashed });

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
         * */

        /// <summary>
        /// Stored procedure is called to search for user and verify password
        /// </summary>
        /// <param name="name">username of login attempt</param>
        /// <param name="password">md5 hash of password as a hex string</param>
        /// <returns>POSTGRESQL will return a boolean informing if user/hash combo is correct</returns>
        public static bool SpAuth(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM Authenticate(:user,:hashedpass);";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.Add(
                        new NpgsqlParameter("user", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });

                    byte[] hashed = new byte[16];
                    for (int i = 0; i < 32; i += 2)
                    {
                        hashed[i / 2] = Convert.ToByte(password.Substring(i, 2), 16);                    
                    }

                    cmd.Parameters.Add(
                       new NpgsqlParameter("hashedpass", NpgsqlTypes.NpgsqlDbType.Bytea) { Value = hashed });
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    return (bool)dt.Rows[0][0];
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return false;
        }

        /// <summary>
        /// Old user addition using parameters instead of stored procedure
        /// </summary>
        /// <param name="name">  Creating Username </param>
        /// <param name="password"> Creating password </param>
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
                    
                    byte[] hashed = crypter.ComputeHash(System.Text.Encoding.Default.GetBytes(password));

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

        /// <summary>
        /// Old Authentication method . 
        /// </summary>
        /// <param name="name"> Authenticating Username</param>
        /// <param name="password"> Authenticating Password</param>
        /// <returns> Boolean if Name and password combo is valid</returns>
        public static bool Authenticated(string name, string password)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(
                "Server=127.0.0.1; Port=5432; Database=Assignment5; Integrated Security=true;"))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM systemusers WHERE username=:user;";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.Add(
                        new NpgsqlParameter("user", NpgsqlTypes.NpgsqlDbType.Text) { Value = name });
                    
                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    string shouldbethis = BitConverter.ToString((byte[])dt.Rows[0]["hashedpass"]).Replace("-", string.Empty);

                    if (shouldbethis.Equals(password.ToUpper()))
                    {
                        return true;
                    }
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