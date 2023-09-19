using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _repository.GetById(id);
                return Ok(item);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            if (IsModelStateIsNotValid())
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TodoItem item)
        {
            if (IsModelStateIsNotValid())
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _repository.Update(item);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.Remove(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool IsModelStateIsNotValid()
        {
            return !ModelState.IsValid;
        }
    }

}
