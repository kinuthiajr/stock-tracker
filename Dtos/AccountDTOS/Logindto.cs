using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AccountDTOS
{
    public class Logindto
    {
        [Required]
        public required string UserName {get; set;}

        [Required]
        public required string Password {get; set;}
    }
}