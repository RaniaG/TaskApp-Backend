using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Test.NoAuth.DTOs
{
    public class CreateTaskItemDTOInput
    {
        [Required]
        public string Body { get; set; }
    }
}
