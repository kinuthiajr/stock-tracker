using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public required DbSet<Stock> Stock {get;set;}
        public required DbSet<Comment> Comments{get;set;}
    }
}