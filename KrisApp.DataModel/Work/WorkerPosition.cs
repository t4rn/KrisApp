using KrisApp.DataModel.Dictionaries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Work
{
    [Table("WorkerPositions", Schema = "Work")]
    public class WorkerPosition
    {
        public int Id { get; set; }
        [ForeignKey("Worker")]
        public int WorkerId { get; set; }
        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public bool Ghost { get; set; }
        public DateTime AddDate { get; set; }

        public PositionType Position { get; set; }
        public Worker Worker { get; set; }
    }
}
/*
 
 create table Work.WorkerPositions(
Id int identity(1,1) primary key,
WorkerId integer references Work.Worker not null,
PositionId integer references Work.PositionType not null,
Ghost bit default 0,
AddDate datetime2 default getdate()
)

 */
