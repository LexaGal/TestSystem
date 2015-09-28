using System.ComponentModel.DataAnnotations;

namespace TestDatabase.Entities
{
    public class Code
    {
        [Key]
        public int Id { get; private set; }

        public int UserId { get; private set; }
        public int TaskId { get; private set; }
        public string Text { get; private set; }

        public Code()
        {
        }

        public Code(int userId, int taskId, string text)
        {
            UserId = userId;
            TaskId = taskId;
            Text = text;
        }
    }
}