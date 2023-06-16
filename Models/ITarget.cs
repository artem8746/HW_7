namespace ToDoListWebAPI_HW.Models
{
    public interface ITarget
    {
        string Description { get; set; }
        int Id { get; set; }
        bool IsCompleted { get; set; }
        string TargetValue { get; set; }
    }
}