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
        internal DatabaseContext db = new DatabaseContext();
        internal MySqlConnection conn;
        public Procedures()
        {
            this.conn = conn = (MySqlConnection)db.Database.GetDbConnection();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        public List<int> SearchInPosts(string query, int questionOnly, int numberLimit)
        {
            var result = new List<int>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);
                cmd.Parameters.Add("@3", DbType.Int32);
                cmd.Parameters["@1"].Value = query;
                cmd.Parameters["@2"].Value = questionOnly;
                cmd.Parameters["@3"].Value = numberLimit;
                cmd.CommandText = "CALL searchInPosts(@1, @2, @3)";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                }
            }
            return result;
        }

        public List<string> getTagsForPost(int postId)
        {
            var result = new List<string>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.Int32);
                cmd.Parameters["@1"].Value = postId;
                cmd.CommandText = "call getTagsForPost(@1)";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
            }
            return result;
        }

        public List<int> GetPostsByUser(string user)
        {
            var result = new List<int>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters["@1"].Value = user;
                cmd.CommandText = "call getPostsByUser(@1)";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                }
            }
            return result;
        }

        public List<int> GetPostsByTag(string tag)
        {
            var result = new List<int>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters["@1"].Value = tag;
                cmd.CommandText = "call getPostsByTag(@1)";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                    reader.Close();
                }
            }
            return result;
        }

        public List<int> SearchHistoryForAccount(string user, int limitNumber)
        {
            var result = new List<int>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);
                cmd.Parameters["@1"].Value = user;
                cmd.Parameters["@2"].Value = limitNumber;
                cmd.CommandText = "call searchHistoryForAccount(@1, @2)";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                }
            }
            return result;
        }

        public List<int> SearchQueryHistoryForAccount(string user, int limitNumber)
        {
            var result = new List<int>();
            using (var cmd = new MySqlCommand())
            {
                cmd.Connection = conn;
                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);
                cmd.Parameters["@1"].Value = user;
                cmd.Parameters["@2"].Value = limitNumber;
                cmd.CommandText = "call searchQueryHistoryForAccount(@1, @2)";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetInt32(0));
                    }
                }
            }
            return result;
        }
    }
}