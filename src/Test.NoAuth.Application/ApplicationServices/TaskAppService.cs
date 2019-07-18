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
    public class TaskAppService:ApplicationService
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
        
    }
}
