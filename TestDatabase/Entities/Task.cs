using System.ComponentModel.DataAnnotations;

namespace TestDatabase.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }
        public double Memory { get; set; }
        public double Time { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public int Price { get; set; }

        public Task()
        {
        }

        public Task(double memory, double time, string description, string result, int price)
        {
            Memory = memory;
            Time = time;
            Description = description;
            Result = result;
            Price = price;
        }
    }
}