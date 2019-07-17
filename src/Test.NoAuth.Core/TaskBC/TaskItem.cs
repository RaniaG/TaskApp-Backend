using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test.NoAuth.Enums;

namespace Test.NoAuth.TaskBC
{
    public class TaskItem:Entity, ICreationAudited,ISoftDelete
    {
        [Required]
        public string Body { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskStatusEnum Status { get; set; }
        public bool IsDeleted { get; set; }

        public TaskItem()
        {
            CreationTime = DateTime.Now;
            Status = TaskStatusEnum.InProgress;
        }
        public void DeleteTask()
        {
            IsDeleted = true;
        }
        public void TaskDone()
        {
            Status = TaskStatusEnum.Done;
        }
        public void TaskInProgress()
        {
            Status = TaskStatusEnum.InProgress;
        }
        public void RestoreTask()
        {
            IsDeleted = false;
        }

    }
}
