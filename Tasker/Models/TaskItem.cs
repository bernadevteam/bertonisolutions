using System;
using SQLite;

namespace Tasker.Models
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

    }
}
