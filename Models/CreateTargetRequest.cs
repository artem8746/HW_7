namespace ToDoListWebAPI_HW.Models
{
    //класс для создания объекта запроса на создание задачи
    public class CreateTargetRequest
    {
        public string TargetValue { get; set; }

        public string Description { get; set; }
    }
}
