﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
   public class CompanyJobSkillPoco : IPoco
    {

        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public String Skill { get; set; }
        [Column("Skill_Level")]
        public String SkillLevel { get; set; }
        public int Importance { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }
      //  public CompanyJobPoco CompanyJob { get; set; }
        public virtual CompanyJobPoco CompanyJob { get; set; }

    }
}
