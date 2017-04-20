using System;
using System.Collections.Generic;

namespace KrisApp.Models.Work
{
    public class WorkerModel
    {
        //public int ID { get; set; }
        public string Nick { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
        //public bool Ghost { get; set; }
        public List<WorkerSkillModel> Skills { get; set; }
        public List<WorkerPositionModel> Positions { get; set; }
    }
}