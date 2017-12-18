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
        public Procedures()
        {
        }

        public List<int> SearchInPosts(string query)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var result = new List<int>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
                    cmd.Parameters.Add("@1", DbType.String);
                    cmd.Parameters["@1"].Value = query;
                    cmd.CommandText = "CALL getPostsWithWeighting(@1)";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32(0));
                            // skip rank and post body
                        }
                    }
                    cmd.Connection.Close();
                }
                return result;
            }
        }

        public List<string> getTagsForPost(int postId)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                var result = new List<string>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
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
                    cmd.Connection.Close();
                }
                return result;
            }
        }

        public List<int> GetPostsByUser(string user)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                var result = new List<int>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
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
                    cmd.Connection.Close();
                }
                return result;
            }
        }

        public List<int> GetPostsByTag(string tag)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                var result = new List<int>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
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
                    cmd.Connection.Close();
                }
                return result;
            }
        }

        public List<int> SearchHistoryForAccount(string user, int limitNumber)
        {
            using (DatabaseContext db = new DatabaseContext())
            {

                var result = new List<int>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
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
                    cmd.Connection.Close();
                }
                return result;
            }
        }

        public List<int> SearchQueryHistoryForAccount(string user, int limitNumber)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var result = new List<int>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
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
                    cmd.Connection.Close();
                }
                return result;
            }
        }
        
        public string TermNetwork(string query)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                string result = "";
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
                    cmd.Parameters.Add("@1", DbType.String);
                    cmd.Parameters["@1"].Value = query;
                    cmd.CommandText = "CALL term_network(@1)";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result += reader.GetString(0);
                        }
                    }
                    cmd.Connection.Close();
                }
                return result;
            }
        }
        
        public class WeightedList
        {
            public string word { get;  }
            public double rank { get; }
            
            public WeightedList(string word, double rank)
            {
                this.word = word;
                this.rank = rank;
            }
        }
        
        public List<WeightedList> WeightedListTFIDF(string query)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var result = new List<WeightedList>();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = (MySqlConnection)db.Database.GetDbConnection();
                    cmd.Connection.Open();
                    cmd.Parameters.Add("@1", DbType.String);
                    cmd.Parameters["@1"].Value = query;
                    cmd.CommandText = "CALL getWeightedListTFIDF(@1)";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new WeightedList(reader.GetString(0), reader.GetDouble(1)));
                        }
                    }
                    cmd.Connection.Close();
                }
                return result;
            }
        }
    }
}