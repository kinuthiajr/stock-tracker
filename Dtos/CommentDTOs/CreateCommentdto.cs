using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDTOs
{
    public class CreateCommentdto
    {
        public string? Title {get;set;}
        public string? Content {get; set;}
        public int? StockId { get;set; }
    }
}