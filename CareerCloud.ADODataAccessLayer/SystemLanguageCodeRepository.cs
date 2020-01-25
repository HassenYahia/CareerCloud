﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connStr;
        public SystemLanguageCodeRepository()
        {
            var Config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            Config.AddJsonFile(path, false);
            var root = Config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Connection;
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                               ([LanguageID]
                                               ,[Name]
                                               ,[Native_Name])
                                         VALUES
                                               (@LanguageID
                                               ,@Name
                                               ,@Native_Name)";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);
                   
                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT [LanguageID]
                                        ,[Name]
                                        ,[Native_Name]
                                    FROM [dbo].[System_Language_Codes]";

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);

                    pocos[index] = poco;
                    index++;
                }
                Connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM System_Language_Codes
                                   WHERE [LanguageID] =@LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                           SET 
                                              [Name] = @Name
                                              ,[Native_Name] = @Native_Name
                                                                              
                                        WHERE [LanguageID] =@LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
