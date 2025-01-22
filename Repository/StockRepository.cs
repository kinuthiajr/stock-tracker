
using api.Dtos.StockDTOs;
using api.Interfaces;
using api.Models;
using api.Models.Data;
using api.Queries;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var allStocks = _context.Stock.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                allStocks = allStocks.Where(name => name.CompanyName.Contains(query.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                allStocks = allStocks.Where(sym => sym.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {   
                    allStocks = (bool)query.IsDecsending ? allStocks.OrderByDescending(sym => sym.Symbol) 
                    : allStocks.OrderBy(sym => sym.Symbol);
                }
            }

            var SkipNum = (query.PageNumber -1) * query.PageSize;

            return await allStocks.Skip(SkipNum).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }    

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(s => s.Id == id);
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