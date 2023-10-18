using System.Text.Json.Serialization;
namespace TestTask.Models

{

    public class ToDoTask
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Completed { get; set; }
    }
}
