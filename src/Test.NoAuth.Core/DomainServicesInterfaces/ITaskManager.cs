using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DomainServicesInterfaces
{
    interface ITaskManager: IDomainService
    {
        void CreateTask(TaskItem task);
        bool DeleteTask(int TaskId);
        TaskItem RestoreTask(int TaskId);
        TaskItem MarkAsDone(int TaskId);
        TaskItem MarkAsInProgress(int TaskId);
    }
}
