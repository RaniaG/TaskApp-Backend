using System;
using System.Collections.Generic;
using System.Text;

namespace Test.NoAuth.DTOs
{
    public class TaskItemGetAllOutputDTO: TaskItemDTO
    {
        public bool IsDeleted { get; set; }

    }
}
