using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.CommentDTOs
{
    public class UpdateCommentdto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Title must be 5 characters")]
        [MaxLength(280,ErrorMessage= "Max title should not exceed 280 characters")]
        public string? Title {get;set;}
        
        [Required]
        [MinLength(5,ErrorMessage ="Content must be 5 characters")]
        [MaxLength(280,ErrorMessage= "Max content should not exceed 280 characters")]
        public string? Content {get; set;}   
    }
}