using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.Enums;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.DTOs
{
    //[AutoMapTo(typeof(TaskItem))]
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskStatusEnum Status { get; set; }
    }
}
