using System;
using api.Dtos;
using api.Dtos.StockDTOs;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static Stockdto ToStockDto(this Stock stockModel)
        {
            return new Stockdto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }  

        public static Stock ToStockFromCreateDto(this CreateStockRequestdto Stockdto)
        {
            return new Stock
            {
                Symbol = Stockdto.Symbol,
                CompanyName = Stockdto.CompanyName,
                Purchase = Stockdto.Purchase,
                LastDiv = Stockdto.LastDiv,
                Industry = Stockdto.Industry,
                MarketCap = Stockdto.MarketCap
            };
        }
    }


}