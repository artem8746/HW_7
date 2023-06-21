namespace ToDoListWebAPI_HW.Models
{
    public class Target : ITarget
    {
        public int Id { get; set; }

        public string TargetValue { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
    //класс задач
}
