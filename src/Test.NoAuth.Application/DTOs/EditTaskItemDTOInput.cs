using System;
using System.Collections.Generic;
using System.Text;
using Test.NoAuth.Enums;

namespace Test.NoAuth.DTOs
{
    public class EditTaskItemDTOInput
    {

        public string Body { get; set; }
        public TaskStatusEnum? Status { get; set; }
    }
}
