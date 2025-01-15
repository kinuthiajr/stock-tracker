using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDTOs
{
    public class Commentdto
    {
        public int Id {get;set;}
        public string? Title {get;set;}
        public string? Content {get; set;}
        public DateTime CreatedOn = DateTime.Now;
        public int? StockId {get; set;}
    }
}