using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AccountDTOS
{
    public class NewUserdto
    {
        public required string Name {get; set;}
        public required string Email {get; set;}
        public required string Token {get; set;}
    }
}