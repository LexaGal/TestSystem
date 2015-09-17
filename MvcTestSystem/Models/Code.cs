using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTestSystem.Models
{
    public class Code
    {
        public Code(int userId, int taskId, string text)
        {
            UserId = userId;
            TaskId = taskId;
            Text = text;
            Id = (new Random()).Next(1, 1000);
        }

        [Key]
        public int Id { get; private set; }

        public int UserId { get; private set; }
        public int TaskId { get; private set; }
        public string Text { get; private set; }
    }
}