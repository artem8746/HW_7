using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebAPI_HW.Models;
using ToDoListWebAPI_HW.Services;

namespace ToDoListWebAPI_HW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ToDoListController : ControllerBase
    {
        private readonly IDataRegisterService dataRegister;

        ToDoListController(IDataRegisterService dataRegister) 
        {
            this.dataRegister = dataRegister;
            dataRegister.Add(new Target { Id = 1, TargetValue = "Wake up" });
        }
        //список с задачами

        [HttpGet]
        public ActionResult GetTargets() 
        {
            if(dataRegister.GetTargets() != null) 
            {
                return Ok(dataRegister.GetTargets());
            }

            return StatusCode(404);
        }
        //ендпоинт для получения всех задач

        [HttpPost]
        public ActionResult AddTarget([FromBody] CreateTargetRequest request)
        {
            var target = new Target
            {
                Id = dataRegister.GetLastId() + 1,
                TargetValue = request.TargetValue,
                Description = request.Description,
                IsCompleted = false
            };

            dataRegister.Add(target);

            return Ok(target);
        }
        //ендпоинт для добавления задач

        [HttpPatch("{id}")]
        public ActionResult ChangeTarget([FromRoute] int id, [FromBody] ChangeTargetRequest request)
        {
            var target = dataRegister.ChangeTarget(request, id);

            if(target is not null)
            {
                return Ok(target);
            }    
            else
                return BadRequest(request);
        }
        //ендпоинт для изменения задач по id

        [HttpDelete("{id}")]
        public ActionResult DeleteTarget([FromRoute] int id)
        {
            var target = dataRegister.DeleteTarget(id);
            if (target is not null)
            {
                return Ok(target);
            }
            
            return BadRequest();
        }
        //ендпоинт для удаления задач по id

        [HttpGet("{id}")]
        public ActionResult GetTarget([FromRoute] int id)
        {
            var target = dataRegister.GetTarget(id);

            if(target is not null)
                return Ok(target);

            return BadRequest();
        }   
        //ендпоинт для получения одной задачи по id
    }
}
