using KrisApp.DataModel.Dictionaries;
using System;

namespace KrisApp.Models.Work
{
    public class WorkerSkillModel
    {
        //public int Id { get; set; }
        public int WorkerId { get; set; }
        public DictionaryItemModel Skill { get; set; }
        public byte Rating { get; set; }
        //public bool Ghost { get; set; }
        //public DateTime AddDate { get; set; }
    }
}