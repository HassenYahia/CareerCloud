using CareerCloud.DataAccessLayer;
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
  public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string _connStr;
        public ApplicantSkillRepository()
        {
            var Config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            Config.AddJsonFile(path, false);
            var root = Config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Connection;
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                            ([Id]
                                            ,[Applicant]
                                            ,[Skill]
                                            ,[Skill_Level]
                                            ,[Start_Month]
                                            ,[Start_Year]
                                            ,[End_Month]
                                            ,[End_Year])
                                        VALUES
                                            (@Id 
                                            ,@Applicant
                                            ,@Skill
                                            ,@Skill_Level
                                            ,@Start_Month
                                            ,@Start_Year
                                            ,@End_Month
                                            ,@End_Year)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);


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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandText = @"SELECT [Id]
                                      ,[Applicant]
                                      ,[Skill]
                                      ,[Skill_Level]
                                      ,[Start_Month]
                                      ,[Start_Year]
                                      ,[End_Month]
                                      ,[End_Year]
                                      ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Skills]";

                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);
                    poco.TimeStamp = (byte[])reader[8];

                    pocos[index] = poco;
                    index++;
                }
                Connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Skills
                                    WHERE ID = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection Connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                foreach (ApplicantSkillPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                           SET 
                                              [Applicant] = @Applicant
                                              ,[Skill] = @Skill
                                              ,[Skill_Level] = @Skill_Level
                                              ,[Start_Month] = @Start_Month
                                              ,[Start_Year] = @Start_Year
                                              ,[End_Month] = @End_Month
                                              ,[End_Year] = @End_Year
                                   WHERE[Id] =@Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    Connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    Connection.Close();
                }
            }
        }
    }
}
