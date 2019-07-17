using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.DomainServicesInterfaces;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DomainServices
{
    class TaskManager: DomainService,ITaskManager
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskManager(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public void CreateTask(TaskItem task)
        {
            _taskRepository.Insert(task);
        }
        public bool DeleteTask(int TaskId)
        {
            TaskItem task = _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return false;
            _taskRepository.Delete(TaskId);
            return true;
        }
        public TaskItem RestoreTask(int TaskId)
        {
            TaskItem task= _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return null;
            task.IsDeleted = false;
            _taskRepository.Update(task);
            return task;
        }

        public TaskItem MarkAsDone(int TaskId)
        {
            TaskItem task = _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return null;
            task.RestoreTask();
            _taskRepository.Update(task);
            return task;
        }

        public TaskItem MarkAsInProgress(int TaskId)
        {
            TaskItem task = _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return null;
            task.TaskInProgress();
            _taskRepository.Update(task);
            return task;
        }

        
    }
}
