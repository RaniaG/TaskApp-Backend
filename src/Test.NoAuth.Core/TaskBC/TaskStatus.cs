using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Test.NoAuth.Enums;

namespace Test.NoAuth.TaskBC
{
    public class TaskStatus : ValueObject
    {
        public TaskStatusEnum Status { get; set; }
        
        [ForeignKey("Task")]
        [Key]
        public int TaskId { get; set; }
        public TaskItem Task { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Status;
        }
        public TaskStatus(int TaskId)
        {
            this.TaskId = TaskId;
            Status = TaskStatusEnum.InProgress;
        }
        public void Delete()
        {
            //Status = TaskStatusEnum.Deleted;
        }
        public void InProgress()
        {
            Status = TaskStatusEnum.InProgress;
        }
        public void Done()
        {
            Status = TaskStatusEnum.Done;
        }
    }
}
