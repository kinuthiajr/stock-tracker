using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AccountDTOS
{
    public class NewUserdto
    {
        public string Name {get; set;}
        public string Email {get; set;}
        public string Token {get; set;}
    }
}