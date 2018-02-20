using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskApi.Controllers
{
    // localhost:[port]/api/task
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;

            if (_context.TaskItems.Count() == 0)
            {
                _context.TaskItems.Add(new TaskItem { Name = "TaskItem1" });
                _context.SaveChanges();
            }
        }

        //GET /api/task
        [HttpGet]
        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        //GET /api/task/{id}
        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetById(long id)
        {
            var item = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody] TaskItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TaskItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTask", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TaskItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var taskItem = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.IsComplete = item.IsComplete;
            taskItem.Name = item.Name;

            _context.TaskItems.Update(taskItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var taskItem = _context.TaskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
