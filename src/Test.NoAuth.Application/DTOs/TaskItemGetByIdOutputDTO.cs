using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DTOs
{
    [AutoMapTo(typeof(TaskItem))]
    [AutoMapFrom(typeof(TaskItem))]
    public class TaskItemGetByIdOutputDTO:TaskItemDTO
    {
        public bool IsDeleted { get; set; }
    }
}
