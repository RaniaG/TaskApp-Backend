using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.Enums;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DTOs
{
    [AutoMapTo(typeof(TaskItem))]
    [AutoMapFrom(typeof(TaskItem))]
    public class EditTaskItemDTOInput
    {

        public string Body { get; set; }
        public TaskStatusEnum? Status { get; set; }
    }
}
