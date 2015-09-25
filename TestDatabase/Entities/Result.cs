using System.ComponentModel.DataAnnotations;

namespace TestDatabase.Entities
{
    public class Result
    {
        [Key]
        public int Id { get; private set; }

        public int CodeId { get; private set; }
        public string ResultState { get; private set; }

        public Result()
        {
        }

        public Result(int codeId, string resultState)
        {
            CodeId = codeId;
            ResultState = resultState;
        }
    }
}