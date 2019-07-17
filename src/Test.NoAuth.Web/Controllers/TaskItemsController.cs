using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.NoAuth.ApplicationServices;
using Test.NoAuth.DTOs;
using Test.NoAuth.EntityFrameworkCore;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : NoAuthControllerBase
    {
        private readonly NoAuthDbContext _context;
        //private TaskAppService _taskAppService { get; set; }

        public TaskItemsController(NoAuthDbContext context)
        {
            _context = context;
            //_taskAppService = taskAppService;
        }

        // GET: api/TaskItems
        [HttpGet]
        //[Route("TaskItems")]
        public IEnumerable<TaskItemDTO> GetAll()
        {
            //return _taskAppService.GetAll();
            return null;
        }

        //// GET: api/TaskItems/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        //{
        //    var taskItem = await _context.Tasks.FindAsync(id);

        //    if (taskItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return taskItem;
        //}

        //// PUT: api/TaskItems/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        //{
        //    if (id != taskItem.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(taskItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TaskItemExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/TaskItems
        //[HttpPost]
        //public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        //{
        //    _context.Tasks.Add(taskItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTaskItem", new { id = taskItem.Id }, taskItem);
        //}

        //// DELETE: api/TaskItems/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<TaskItem>> DeleteTaskItem(int id)
        //{
        //    var taskItem = await _context.Tasks.FindAsync(id);
        //    if (taskItem == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tasks.Remove(taskItem);
        //    await _context.SaveChangesAsync();

        //    return taskItem;
        //}

        //private bool TaskItemExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.Id == id);
        //}
    }
}
