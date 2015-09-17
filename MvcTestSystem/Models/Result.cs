using System;
using System.ComponentModel.DataAnnotations;

namespace MvcTestSystem.Models
{
    public class Result
    {
        [Key]
        public int Id { get; private set; }
        public int CodeId { get; private set; }
        public bool ResultState { get; private set; }

        public Result(int codeId, bool resultState)
        {
            Id = (new Random()).Next(1, 1000);
            CodeId = codeId;
            ResultState = resultState;
        }
    }
}