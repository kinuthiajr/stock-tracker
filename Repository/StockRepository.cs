
using api.Dtos.StockDTOs;
using api.Interfaces;
using api.Models;
using api.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDbContext context) : IStockRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
           await _context.SaveChangesAsync();
           return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestdto updatedto)
        {
            var existing_stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if( existing_stock == null)
            {
                return null;
            }

            existing_stock.Symbol = updatedto.Symbol;
            existing_stock.CompanyName = updatedto.CompanyName;
            existing_stock.Purchase = updatedto.Purchase;
            existing_stock.Industry = updatedto.Industry;
            existing_stock.LastDiv = updatedto.LastDiv;
            existing_stock.MarketCap = updatedto.MarketCap;

            await _context.SaveChangesAsync();
            return existing_stock;
        }
    }
}