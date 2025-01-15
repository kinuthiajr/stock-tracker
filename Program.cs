using api.Interfaces;
using api.Models.Data;
using api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers(); // Added this after making the Controller
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options => 
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}); // Prevents object cycles 

builder.Services.AddDbContext<ApplicationDbContext> (options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // Added this after making the Data folder so as to connect with sqlite

builder.Services.AddScoped<IStockRepository, StockRepository>(); // Registered this Interface&Repo
builder.Services.AddScoped<ICommentRepository, CommentRepository>(); // Registered this Interface&Repo

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Added 
app.Run();
