using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.DomainServicesInterfaces;
using Test.NoAuth.DTOs;
using Test.NoAuth.TaskBC;
using System.Linq;
using Abp.ObjectMapping;
using Test.NoAuth.Enums;

namespace Test.NoAuth.ApplicationServices
{
    public class TaskAppService:ApplicationService, ITaskAppService
    {
        private ITaskManager _taskManager { get; set; }
        public IObjectMapper _objectMapper { get; set; }
        public TaskAppService(ITaskManager taskManager,IObjectMapper objectMapper)
        {
            _taskManager = taskManager;
            _objectMapper = objectMapper;
        }
        public IQueryable<TaskItemDTO> GetAllUndeleted()
        {
            IQueryable<TaskItem> tasks= _taskManager.GetAllUndeleted();
            return tasks.Select(t => _objectMapper.Map<TaskItemDTO>(t));
        }
        public IEnumerable<TaskItemGetAllOutputDTO> GetAll()
        {
            List<TaskItem> tasks = _taskManager.GetAll();
            return tasks.Select(t => _objectMapper.Map<TaskItemGetAllOutputDTO>(t));
        }
        public TaskItemGetByIdOutputDTO GetById(int Id)
        {
            return _objectMapper.Map<TaskItemGetByIdOutputDTO>(_taskManager.GetById(Id));
        }
        public TaskItemDTO CreateTask(CreateTaskItemDTOInput taskDTO)
        {
           return _objectMapper.Map<TaskItemDTO>(_taskManager.CreateTask(_objectMapper.Map<TaskItem>(taskDTO)));
        }
        public TaskItemDTO ChangeTaskStatus(int TaskId, TaskStatusEnum newstatus)
        {
            return _objectMapper.Map<TaskItemDTO>(_taskManager.ChangeStatus(TaskId, newstatus));
        }
        public TaskItemDTO ChangeTaskBody (int TaskId, string Body)
        {
            return _objectMapper.Map<TaskItemDTO>(_taskManager.ChangeBody(TaskId, Body));
        }
        public bool DeleteTask(int TaskId)
        {
            return _taskManager.DeleteTask(TaskId);
        }
        public void MarkTaskAsOverdue(int TaskId)
        {
            _taskManager.MarkAsOverdue(TaskId);
        }
       
        public void HardDeleteTasks()
        {
            //to delete tasks which have been marked as deleted for more than 30 days
            IEnumerable<TaskItem> tasks = _taskManager.GetAll().Where(x => x.IsDeleted);
            foreach (TaskItem item in tasks)
            {
                 //if ((DateTime.Now - (DateTime)item.DeletedAt).Minutes >= 1)
                if((DateTime.Now-(DateTime)item.DeletedAt).TotalDays>=30)
                        _taskManager.HardDeleteTask(item);
            }
        }

    }
}
