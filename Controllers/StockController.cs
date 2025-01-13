using System;
using api.Dtos;
using api.Dtos.StockDTOs;
using api.Mappers;
using api.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stock.ToListAsync();
            var Stockdto = stocks.Select(s => s.ToStockDto()); // Select is a mapper that returns a dto object of stock class
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if(stock ==null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestdto Stockdto)
        {
            var stockmodel = Stockdto.ToStockFromCreateDto();
           await _context.Stock.AddAsync(stockmodel);
            await _context.SaveChangesAsync();
            return  CreatedAtAction(nameof(GetById), new{id = stockmodel.Id},stockmodel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestdto updatedto)
        {
            var stockmodel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stockmodel == null)
            {
                return NotFound();
            }

            stockmodel.Symbol = updatedto.Symbol;
            stockmodel.CompanyName = updatedto.CompanyName;
            stockmodel.Purchase = updatedto.Purchase;
            stockmodel.Industry = updatedto.Industry;
            stockmodel.LastDiv = updatedto.LastDiv;
            stockmodel.MarketCap = updatedto.MarketCap;

           await _context.SaveChangesAsync();

            return Ok(stockmodel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)  
        {
            var stockmodel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stockmodel == null)
            {
                return NotFound();
            }

            _context.Stock.Remove(stockmodel);
           await _context.SaveChangesAsync();

            return NoContent();
        } 
    }
}