using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domainlayer.Models;
using ApplicationLayer.Service.TareaService;
using Domainlayer.DTO;
using System.Threading.Tasks;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly TaskService _service;

        public TareaController(TaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Response<Tareas>>> GetAllAsync()
            => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<Tareas>>> GetIdTask(int id)
            => await _service.GetIdTask(id);

        [HttpPost]
        public async Task<ActionResult<Response<string>>> AddAsync(Tareas tarea)
            => await _service.AddAsync(tarea);

        [HttpPut]
        public async Task<ActionResult<Response<string>>> UpdateAsync(Tareas tarea)
            => await _service.UpdateAsync(tarea);

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DeleteAsync(int id)
            => await _service.DeleteAsync(id);

        [HttpGet("pendientes")]
        public async Task<ActionResult<Response<Tareas>>> GetPendientes()
        {
            return await _service.GetFilteredAsync(pendientes: true);
        }

        [HttpGet("completadas")]
        public async Task<ActionResult<Response<Tareas>>> GetCompletadas()
        {
            return await _service.GetFilteredAsync(completadas: true);
        }
    }
}
