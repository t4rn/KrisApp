using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Logs
{
    [Table("Logs", Schema = "WWW")]
    public class AppLog
    {
        public enum LogType
        {
            DEBUG, ERROR
        }

        public long ID { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Ip { get; set; }
        public DateTime? AddDate { get; set; }
    }
}