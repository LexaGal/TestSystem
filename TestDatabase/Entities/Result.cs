using System.ComponentModel.DataAnnotations;

namespace TestDatabase.Entities
{
    public class Result
    {
        [Key]
        public int Id { get; private set; }

        public int CodeId { get; private set; }
        public string ResultState { get; private set; }
        public int TaskId { get; private set; }
        public int UserId { get; private set; }

        public Result()
        {
        }

        public Result(int taskId, int userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public Result(int codeId, string resultState, int taskId, int userId)
        {
            CodeId = codeId;
            ResultState = resultState;
            TaskId = taskId;
            UserId = userId;
        }
    }
}