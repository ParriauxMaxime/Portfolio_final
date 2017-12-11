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
        
        public static List<int> GetPostsByTag(string tag)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);

                cmd.Parameters["@1"].Value = tag;

                cmd.CommandText = "call getPostsByTag(@1)";

                var reader = cmd.ExecuteReader();

                var result = new List<int>();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
                return result;
            }
        }
        
        public static List<int> SearchHistoryForAccount(string user, int limitNumber)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);

                cmd.Parameters["@1"].Value = user;
                cmd.Parameters["@2"].Value = limitNumber;

                cmd.CommandText = "call searchHistoryForAccount(@1, @2)";

                var reader = cmd.ExecuteReader();

                var result = new List<int>();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
                return result;
            }
        }
        
        public static List<int> SearchQueryHistoryForAccount(string user, int limitNumber)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.String);
                cmd.Parameters.Add("@2", DbType.Int32);

                cmd.Parameters["@1"].Value = user;
                cmd.Parameters["@2"].Value = limitNumber;

                cmd.CommandText = "call searchQueryHistoryForAccount(@1, @2)";

                var reader = cmd.ExecuteReader();

                var result = new List<int>();
                while (reader.Read())
                {
                    result.Add(reader.GetInt32(0));
                }
                return result;
            }
        }

          public static List<Post> getQuestions(uint page, uint pageSize)
        {
            using (var db = new DatabaseContext())
            {
                var conn = (MySqlConnection) db.Database.GetDbConnection();
                conn.Open();
                var cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.Add("@1", DbType.UInt32);
                cmd.Parameters.Add("@2", DbType.UInt32);

                cmd.Parameters["@1"].Value = page;
                cmd.Parameters["@2"].Value = pageSize;

                cmd.CommandText = "call getQuestion(@1, @2)";

                var reader = cmd.ExecuteReader();

                var result = new List<Post>();
                while (reader.Read())
                {
                    var post = new Post();
                    post.Id = reader.GetInt32(0);
                    try {post.parentId = reader.GetInt32(1);}
                    catch (Exception) {post.parentId = null;}
                    try {post.acceptedAnswerId = reader.GetInt32(2);}
                    catch (Exception) {post.acceptedAnswerId = null;}
                    post.creationDate = reader.GetDateTime(3);
                    try {post.closedDate = reader.GetDateTime(4);}
                    catch (Exception) {post.closedDate = null;}
                    try {post.title = reader.GetString(5);}
                    catch (Exception) {post.title = null;}
                    post.score = reader.GetInt32(6);
                    post.postTypeId = reader.GetInt32(7);
                    post.body = reader.GetString(8);
                    result.Add(post);
                }
                return result;
            }
        }
    }
}