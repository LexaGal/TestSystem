using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTestSystem.Models
{
    public class Task
    {
        [Key]
        public int Id { get; private set; }

        public int Memory { get; private set; }
        public int Time { get; set; }
        public string Description { get; private set; }
        public string Result1 { get; private set; }

        public Task(int memory, int time, string description, string result1)
        {
            Id = (new Random()).Next(1, 1000);
            Memory = memory;
            Time = time;
            Description = description;
            Result1 = result1;
        }
    }
}