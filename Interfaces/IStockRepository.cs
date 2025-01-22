using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.StockDTOs;
using api.Models;
using api.Queries;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestdto stockdto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}