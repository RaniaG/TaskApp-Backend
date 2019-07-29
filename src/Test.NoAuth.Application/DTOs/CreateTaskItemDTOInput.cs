using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test.NoAuth.CustomAttributes;

namespace Test.NoAuth.DTOs
{
    public class CreateTaskItemDTOInput
    {
        [Required]
        public string Body { get; set; }

        [FutureDate]
        public DateTime? DeadLine { get; set; }
    }
}
