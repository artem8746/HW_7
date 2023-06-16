using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebAPI_HW.Models;

namespace ToDoListWebAPI_HW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ToDoListController : ControllerBase
    {
        public static List<Target> targets = new List<Target>() { new Target { Id = 12, TargetValue = "Wake up" } };
        //список с задачами

        [HttpGet]
        public ActionResult GetTargets() 
        {
            if(targets != null) 
            {
                return Ok(targets);
            }

            return StatusCode(404);
        }
        //ендпоинт для получения всех задач

        [HttpPost]
        public ActionResult AddTarget([FromBody] CreateTargetRequest request)
        {
            var target = new Target
            {
                Id = targets[targets.Count() - 1].Id + 1,
                TargetValue = request.TargetValue,
                Description = request.Description,
                IsCompleted = false
            };

            targets.Add(target);

            return Ok(target);
        }
        //ендпоинт для добавления задач

        [HttpPatch("{id}")]
        public ActionResult ChangeTarget([FromRoute] int id, [FromBody] ChangeTargetRequest request)
        {            
            var target = targets.FirstOrDefault(x => x.Id == id);
            if(target != null)
            {
                target.TargetValue = request.NewTargetValue;
                target.IsCompleted = request.IsCompleted;
                target.Description = request.Description;
                
                return Ok(target);
            }    
            else
                return BadRequest(request);
        }
        //ендпоинт для изменения задач по id

        [HttpDelete("{id}")]
        public ActionResult DeleteTarget([FromRoute] int id)
        {
            var target = targets.FirstOrDefault(x => x.Id == id);
            if (target != null)
            {
                Ok(targets.Remove(target));
                return Ok(target);
            }
            else
                return BadRequest();
        }
        //ендпоинт для удаления задач по id

        [HttpGet("{id}")]
        public ActionResult GetTarget([FromRoute] int id)
        {
            var target = targets.FirstOrDefault(x => x.Id == id);

            if(target != null)
                return Ok(target);

            return BadRequest();
        }
        //ендпоинт для получения одной задачи по id
    }
}
