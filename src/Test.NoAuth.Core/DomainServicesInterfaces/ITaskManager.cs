using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.NoAuth.Enums;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DomainServicesInterfaces
{
    public interface ITaskManager: IDomainService
    {
        TaskItem CreateTask(TaskItem task);
        bool DeleteTask(int TaskId);
        bool HardDeleteTask(TaskItem Task);
        TaskItem RestoreTask(int TaskId);
        TaskItem ChangeStatus(int TaskId, TaskStatusEnum status);
        TaskItem ChangeBody(int TaskId, string newBody);
        IQueryable<TaskItem> GetAllUndeleted();
        List<TaskItem> GetAll();
        TaskItem GetById(int Id);
        void MarkAsOverdue(int TaskId);
        TaskItem UpdateTask(TaskItem task);


    }
}
