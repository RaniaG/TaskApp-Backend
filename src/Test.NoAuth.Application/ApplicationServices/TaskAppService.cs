using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.DomainServicesInterfaces;
using Test.NoAuth.DTOs;
using Test.NoAuth.TaskBC;
using System.Linq;
using Abp.ObjectMapping;

namespace Test.NoAuth.ApplicationServices
{
    public class TaskAppService:ApplicationService
    {
        private ITaskManager _taskManager { get; set; }
        public IObjectMapper _objectMapper { get; set; }
        public TaskAppService(ITaskManager taskManager,IObjectMapper objectMapper)
        {
            _taskManager = taskManager;
            _objectMapper = objectMapper;
        }
        public IQueryable<TaskItemDTO> GetAll()
        {
            IQueryable<TaskItem> tasks= _taskManager.GetAllTasks();
            return tasks.Select(t => _objectMapper.Map<TaskItemDTO>(t));
        }
        public TaskItemDTO CreateTask(TaskItemDTO taskDTO)
        {
           return _objectMapper.Map<TaskItemDTO>(_taskManager.CreateTask(_objectMapper.Map<TaskItem>(taskDTO)));
        }
        
    }
}
