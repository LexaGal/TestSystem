using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace TestDatabase.Entities
{
    public class Test
    {
        [Key]
        public int Id { get; private set; }

        public int TaskId { get; private set; }
        public string Input { get; private set; }
        public string Output { get; private set; }

        public Test()
        {
        }

        public Test(int taskId, string input, string output)
        {
            TaskId = taskId;
            Input = input;
            Output = output;
        }
    }
}