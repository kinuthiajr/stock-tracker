using System;
using api.Dtos;
using api.Mappers;
using api.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController :ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }   

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList()
            .Select(s => s.ToStockDto()); // Select is a mapper that returns a dto object of stock class
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);
            if(stock ==null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestdto Stockdto)
        {
            var stockmodel = Stockdto.ToStockFromCreateDto();
            _context.Stock.Add(stockmodel);
            _context.SaveChanges();
            return  CreatedAtAction(nameof(GetById), new{id = stockmodel.Id},stockmodel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestdto updatedto)
        {
            var stockmodel = _context.Stock.FirstOrDefault(x => x.Id == id);

            if(stockmodel == null)
            {
                return NotFound();
            }
        }

    }
}