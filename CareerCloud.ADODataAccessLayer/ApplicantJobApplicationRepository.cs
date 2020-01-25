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
  public  class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connStr;
        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Connection;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Job]
                                       ,[Application_Date])
                                 VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Job 
                                       ,@Application_Date)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                   

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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Job]
                                      ,[Application_Date]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Job_Applications]";

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[index] = poco;
                    index++;
                }
                Connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Job_Applications
                                    WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                        SET 
                                            [Applicant] = @Applicant
                                            ,[Job] = @Job
                                            ,[Application_Date] = @Application_Date
                                        WHERE[Id] =@id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    
    }
}
