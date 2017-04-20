using KrisApp.DataModel.Work;
using System;

namespace KrisApp.DataModel
{
    public class Task
    {
        public int ID { get; set; }
        public int NygusID { get; set; }
        public Worker Nyger { get; set; }
        public string TaskDesc { get; set; }
        public DateTime? TaskDate { get; set; }
        public DateTime AddDate { get; set; }
        public bool Ghost { get; set; }

    }
}