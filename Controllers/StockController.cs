using System;
using api.Dtos;
using api.Dtos.StockDTOs;
using api.Interfaces;
using api.Mappers;
using api.Models.Data;
using api.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController(ApplicationDbContext context, IStockRepository stockRepo) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IStockRepository _stockRepo = stockRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetAllAsync(query);
            var Stockdto = stocks.Select(s => s.ToStockDto()); // Select is a mapper that returns a dto object of stock class
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var stock = await _stockRepo.GetByIdAsync(id);
            if(stock ==null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestdto Stockdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockmodel = Stockdto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockmodel);
            return  CreatedAtAction(nameof(GetById), new{id = stockmodel.Id},stockmodel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestdto updatedto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockmodel = await _stockRepo.UpdateAsync(id, updatedto);

            if(stockmodel == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();

            return Ok(stockmodel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)  
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockmodel = await _stockRepo.DeleteAsync(id);

            if(stockmodel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        } 
    }
}