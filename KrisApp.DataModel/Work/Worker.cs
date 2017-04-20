using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Work
{
    [Table("Worker", Schema = "Work")]
    public class Worker
    {
        public int ID { get; set; }
        public string Nick { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
        public bool Ghost { get; set; }
        public List<WorkerSkill> Skills { get; set; }
        public List<WorkerPosition> Positions { get; set; }
    }
}