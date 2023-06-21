namespace ToDoListWebAPI_HW.Models
{
    //класс для создания объекта запроса на изменение задачи
    public class ChangeTargetRequest
    {
        public string NewTargetValue { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
