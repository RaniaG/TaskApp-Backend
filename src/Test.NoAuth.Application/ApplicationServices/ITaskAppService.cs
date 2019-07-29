using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.NoAuth.DTOs;
using Test.NoAuth.Enums;

namespace Test.NoAuth.ApplicationServices
{
    public interface ITaskAppService
    {
        IQueryable<TaskItemDTO> GetAllUndeleted();
        IEnumerable<TaskItemGetAllOutputDTO> GetAll();
        TaskItemGetByIdOutputDTO GetById(int Id);
        TaskItemDTO CreateTask(CreateTaskItemDTOInput taskDTO);
        TaskItemDTO ChangeTaskStatus(int TaskId, TaskStatusEnum newstatus);

        TaskItemDTO ChangeTaskBody(int TaskId, string Body);
        bool DeleteTask(int TaskId);
        void MarkTaskAsOverdue(int TaskId);
        void HardDeleteTasks();
        TaskItemDTO UpdateTask(int id,EditTaskItemDTOInput taskDTO);

    }
}
