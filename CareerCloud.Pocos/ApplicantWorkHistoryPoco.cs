using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
       
        [Column("Company_Name")]
        public String CompanyName { get; set; }
        [Column("Country_Code")] 
        public String  CountryCode { get; set; }
        public String Location { get; set; }
        [Column("Job_Title")]
        public String JobTitle { get; set; }
        [Column("Job_Description")]
        public String JobDescription { get; set; }
         [Column("Start_Month")]
        public short StartMonth{ get; set; }
        [Column("Start_Year")]
        public int StartYear { get; set; }
        [Column("End_Month")]
        public short EndMonth { get; set; }
        [Column("End_Year")]
        public int EndYear { get; set; }      
        [Column("Time_Stamp")]
        public Byte[] TimeStamp { get; set; }
    }
}
