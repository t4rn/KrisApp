using KrisApp.DataModel.Dictionaries;
using System;

namespace KrisApp.Models.Work
{
    public class WorkerPositionModel
    {
        //public int Id { get; set; }
        public int WorkerId { get; set; }
        public DictionaryItemModel Position { get; set; }
        //public bool Ghost { get; set; }
        //public DateTime AddDate { get; set; }
    }
}