using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Models;

namespace DataAccessLayer
{
    public class Procedures
    {
        public static List<int> SearchInPosts(string query, int questionOnly, int numberLimit)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);
                cmd.Parameters.Add("@3", DbType.Int32);
                cmd.Parameters["@1"].Value = query;
                cmd.Parameters["@2"].Value = questionOnly;
                cmd.Parameters["@3"].Value = numberLimit;

                cmd.CommandText = "CALL searchInPosts(@1, @2, @3)";

                var reader = cmd.ExecuteReader();

                var result = new List<int>();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
                return result;
            }
        }
        
        public static List<int> GetPostsByUser(string user)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);

                cmd.Parameters["@1"].Value = user;

                cmd.CommandText = "call getPostsByUser(@1)";

                var reader = cmd.ExecuteReader();

                var result = new List<int>();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
                return result;
            }
        }
    }
}