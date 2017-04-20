using System;

namespace KrisApp.DataModel.Dictionaries
{
    public abstract class DictionaryItem
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool Ghost { get; set; }

        public DateTime AddDate { get; set; }
    }
}