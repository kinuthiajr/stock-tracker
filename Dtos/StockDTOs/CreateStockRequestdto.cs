using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class CreateStockRequestdto
    {
        [Required]
        [MaxLength(10,ErrorMessage ="Symbol must be 10 charactors")]
        public string Symbol {get; set;} = string.Empty;

        [Required]
        [MaxLength(40,ErrorMessage ="Name must be 40 charactors")]
        public string CompanyName {get; set;} = string.Empty;

        [Required]
        [Range(1,1000000000)]
        public decimal Purchase {get;set;}

        [Required]
        [Range(0.001,100)]
        public decimal LastDiv {get;set;}

        [Required]
        [MaxLength(40,ErrorMessage ="Industry must be 40 charactors")]
        public string Industry {get; set;} = string.Empty;

        [Required]
        [Range(1,5000000000000)]
        public long MarketCap {get;set;}
    }
    
}