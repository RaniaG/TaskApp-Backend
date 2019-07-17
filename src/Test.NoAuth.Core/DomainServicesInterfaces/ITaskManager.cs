using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DomainServicesInterfaces
{
    public interface ITaskManager: IDomainService
    {
        TaskItem CreateTask(TaskItem task);
        bool DeleteTask(int TaskId);
        TaskItem RestoreTask(int TaskId);
        TaskItem MarkAsDone(int TaskId);
        TaskItem MarkAsInProgress(int TaskId);
        IQueryable<TaskItem> GetAllTasks();
        IQueryable<TaskItem> GetInProgressTasks();
        IQueryable<TaskItem> GetDoneTasks();
        IQueryable<TaskItem> GetDeletedTasks();
    }
}
