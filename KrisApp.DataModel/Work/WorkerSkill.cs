using KrisApp.DataModel.Dictionaries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Work
{
    [Table("WorkerSkills", Schema = "Work")]
    public class WorkerSkill
    {
        public int Id { get; set; }
        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        [ForeignKey("Skill")]
        public int SkillId { get; set; }
        public byte Rating { get; set; }
        public bool Ghost { get; set; }
        public DateTime AddDate { get; set; }

        public SkillType Skill { get; set; }
        public Worker Worker { get; set; }
    }
}
/*
 * 
 * create table Work.WorkerSkills(
Id int identity(1,1) primary key,
WorkerId integer references Work.Worker not null,
SkillId integer references Work.SkillType not null,
Rating tinyint CHECK (Rating > 0 AND Rating < 6) not null,
Ghost bit default 0,
AddDate datetime2 default getdate()
)
*/