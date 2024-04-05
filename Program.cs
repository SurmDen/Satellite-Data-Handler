using SQLite_Parser.Interfaces;
using SQLite_Parser.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<ISQLiteHandler, SQLiteHandler>();

builder.Services.AddTransient<IExcelParser, ExcelParser>();

var app = builder.Build();

app.UseStaticFiles();

app.UseDefaultFiles();

app.UseRouting();

app.UseSession();

app.MapRazorPages();

app.Run();
