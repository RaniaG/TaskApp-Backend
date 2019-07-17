using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.NoAuth.DomainServicesInterfaces;
using Test.NoAuth.Enums;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DomainServices
{
    public class TaskManager: DomainService,ITaskManager
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskManager(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public TaskItem CreateTask(TaskItem task)
        {
           return  _taskRepository.Insert(task);
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

        public IQueryable<TaskItem> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }
        public IQueryable<TaskItem> GetInProgressTasks()
        {
            return _taskRepository.GetAll().Where(t => t.Status == TaskStatusEnum.InProgress && t.IsDeleted == false);
        }
        public IQueryable<TaskItem> GetDoneTasks()
        {
            return _taskRepository.GetAll().Where(t => t.Status == TaskStatusEnum.Done&&t.IsDeleted==false);
        }
        public IQueryable<TaskItem> GetDeletedTasks()
        {
            return _taskRepository.GetAll().Where(t => t.IsDeleted);
        }
    }
}
