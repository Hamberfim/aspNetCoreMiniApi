using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SandwichStore.Models;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// db connection
var connectionString = builder.Configuration.GetConnectionString("Sandwiches") ?? "Data Source=Sandwiches.db";

// add the needed services
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddDbContext<SandwichDB>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSqlite<SandwichDB>(connectionString);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SandwichStore API", Description = "Making a Sandwich you will love!", Version = "V1" });
});

// TODO: needs configured
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
      builder =>
      {
          builder.WithOrigins("*");
      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// add swagger and the swagger UI endpoints
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SandwichStore API v1");
});

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "head over to /swagger");
// to SandwichDB : DbContext in SandwichModel.cs file for a persistent SQLite db
app.MapGet("/sandwiches", async (SandwichDB db) => await db.Sandwiches.ToListAsync());
app.MapPost("/sandwich", async (SandwichDB db, SandwichModel sandwich) =>
{
    await db.Sandwiches.AddAsync(sandwich);
    await db.SaveChangesAsync();
    return Results.Created($"/sandwich/{sandwich.Id}", sandwich);
});
app.MapGet("/sandwich/{id}", async (SandwichDB db, int id) => await db.Sandwiches.FindAsync(id));
app.MapPut("/sandwich/{id}", async (SandwichDB db, SandwichModel updatesandwich, int id) =>
{
    var sandwich = await db.Sandwiches.FindAsync(id);
    if (sandwich is null)
    {
        return Results.NotFound();
    }
    sandwich.Name = updatesandwich.Name;
    sandwich.Description = updatesandwich.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/sandwich/{id}", async (SandwichDB db, int id) =>
{
    var sandwich = await db.Sandwiches.FindAsync(id);
    if (sandwich is null)
    {
        return Results.NotFound();
    }
    db.Sandwiches.Remove(sandwich);
    await db.SaveChangesAsync();
    return Results.Ok();
});


// old in-mem to root file inMemSandwichDB.cs containing model and the crud methods
//app.MapGet("/sandwiches/{id}", (int id) => SandwichDB.GetSandwich(id));
//app.MapGet("/sandwiches", () => SandwichDB.GetSandwiches());
//app.MapPost("/sandwiches", (Sandwich sandwich) => SandwichDB.CreateSandwich(sandwich));
//app.MapPut("/sandwiches", (Sandwich sandwich) => SandwichDB.UpdateSandwich(sandwich));
//app.MapDelete("/sandwiches/{id}", (int id) => SandwichDB.RemoveSandwich(id));

app.Run();
