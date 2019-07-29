using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
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
        private IUnitOfWorkManager _unitOfWorkManager { get; set; }

        public TaskManager(IUnitOfWorkManager unitOfWorkManager,IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public TaskItem CreateTask(TaskItem task)
        {
           int Id= _taskRepository.InsertAndGetId(task);
           return _taskRepository.Get(Id);
        }
        public TaskItem UpdateTask(TaskItem task)
        {
            return _taskRepository.Update(task);
        }
        
        public TaskItem RestoreTask(int TaskId)
        {
            TaskItem task= _taskRepository.Get(TaskId);
            if (task == null)
                return null;
            task.IsDeleted = false;
            _taskRepository.Update(task);
            return task;
        }

        public TaskItem ChangeStatus(int TaskId,TaskStatusEnum status)
        {
            TaskItem task = _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return null;
            task.Status=status;
            _taskRepository.Update(task);
            return task;
        }

        
        public TaskItem ChangeBody(int TaskId, string newBody)
        {
            return _taskRepository.Update(TaskId, t => t.Body = newBody);
        }
        public TaskItem GetById(int Id)
        {
            return _taskRepository.Get(Id);
        }

        public IQueryable<TaskItem> GetAllUndeleted()
        {
            return _taskRepository.GetAll();   
        }
        public List<TaskItem> GetAll()
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                return _taskRepository.GetAllList();
            }
        }
        public void MarkAsOverdue(int TaskId)
        {
            TaskItem task=_taskRepository.Get(TaskId);
            task.Overdue = true;
            _taskRepository.Update(task);
        }
        public bool DeleteTask(int TaskId)
        {
            TaskItem task = _taskRepository.FirstOrDefault(TaskId);
            if (task == null)
                return false;
            task.DeletedAt = DateTime.Now;
            task.IsDeleted = true;
            _taskRepository.Update(task);
            //cant use _taskRepository.Delete bec deletedAt doesnt get set in DB
            return true;
        }

        public bool HardDeleteTask(TaskItem task)
        {
            _taskRepository.HardDelete(task);
            return true;
        }
    }
}
