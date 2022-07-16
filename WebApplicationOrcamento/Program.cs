using Microsoft.EntityFrameworkCore;
using WebApplicationOrcamento;
using WebApplicationOrcamento.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OrcamentoService, OrcamentoService>();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
builder => builder.MigrationsAssembly("WebApplicationOrcamento")));
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(x => x.First());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
                      builder => builder
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          );
});


var app = builder.Build();
app.UseCors("Policy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
