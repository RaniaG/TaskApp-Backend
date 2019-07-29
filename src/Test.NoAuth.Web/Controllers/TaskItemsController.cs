using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.NoAuth.ApplicationServices;
using Test.NoAuth.DTOs;
using Test.NoAuth.EntityFrameworkCore;
using Test.NoAuth.Enums;
using Test.NoAuth.TaskBC;
using Hangfire;
using Microsoft.AspNetCore.JsonPatch;
using Abp.ObjectMapping;

namespace Test.NoAuth.Web.Controllers
{
    [ApiController]
    public class TaskItemsController : NoAuthControllerBase
    {
        private readonly NoAuthDbContext _context;
        private TaskAppService _taskAppService { get; set; }
        public IObjectMapper _objectMapper { get; set; }

        public TaskItemsController(NoAuthDbContext context, TaskAppService taskAppService
            , IObjectMapper objectMapper)
        {
            _context = context;
            _taskAppService = taskAppService;
            _objectMapper = objectMapper;
        }

        [HttpGet("api/TaskItems")]
        public ActionResult<IEnumerable<TaskItemGetAllOutputDTO>> GetAll()
        {
            return _taskAppService.GetAll().ToList();
        }
        [HttpGet("api/TaskItems/UnDeleted")]
        public ActionResult<IEnumerable<TaskItemDTO>> GetAllUndelted()
        {
            return _taskAppService.GetAllUndeleted().ToList();
        }
        
        [HttpPost("api/TaskItems")]
        public ActionResult<TaskItemDTO> PostTaskItem(CreateTaskItemDTOInput task)
        {

            TimeSpan ts = (DateTime)task.DeadLine-DateTime.Now;
            if (task.DeadLine != null && ts.Days < 0)
                return BadRequest();
            TaskItemDTO TaskDTO = _taskAppService.CreateTask(task);
            //must wait for task to get id
            if (TaskDTO != null&&task.DeadLine!=null)
            {
                BackgroundJob.Schedule<ITaskAppService>((x) => x.MarkTaskAsOverdue(TaskDTO.Id), TimeSpan.FromDays(ts.TotalDays));
                //BackgroundJob.Enqueue<TaskAppService>((x) => x.MarkTaskAsOverdue(TaskDTO.Id));
            }
            return TaskDTO;
        }

        [HttpPatch("api/TaskItemsold/{id}")]
        public ActionResult<TaskItemDTO> PatchTaskItem(int id, EditTaskItemDTOInput taskInput)
        {

            try {
                TaskItemDTO task = _taskAppService.GetById(id);
            //if (task == null || ((TaskItemGetByIdOutputDTO)task).IsDeleted)
                if (taskInput.Status != null)
                    task= _taskAppService.ChangeTaskStatus(id,(TaskStatusEnum)taskInput.Status);
                if(taskInput.Body!=null)
                   task= _taskAppService.ChangeTaskBody(id,taskInput.Body);
                return task;
            } catch (Exception e)
            {
                return NotFound();
            }
        }
        [HttpDelete("api/TaskItems/{id}")]
        public ActionResult DeleteTaskItem(int id)
        {
            try
            {
                if (_taskAppService.DeleteTask(id))
                    return Ok();
                else return NotFound();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        [HttpPatch("api/TaskItems/{id}")]
        public ActionResult PatchTask(int id, [FromBody]JsonPatchDocument<EditTaskItemDTOInput> taskInput)
        {
            if (taskInput == null)
                return BadRequest();
            TaskItemDTO task = _taskAppService.GetById(id);
            if (task == null)
                return BadRequest();
            EditTaskItemDTOInput TaskToPatch = new EditTaskItemDTOInput()
            {
                Body = task.Body,
                Status = task.Status
            };
            
            //validation for operation types
            taskInput.ApplyTo(TaskToPatch);

            task =_taskAppService.UpdateTask(id,TaskToPatch);
            return Ok(task);
        }
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
