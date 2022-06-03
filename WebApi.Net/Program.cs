using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionStrig = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DbApiContext>(options => options.UseSqlServer(connectionStrig));  

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1"});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}

app.MapFallback(() => Results.Redirect("/swagger"));

app.MapGet("/", () => "Hello Web Api.NET");

var request = new Request(99, "method get");
app.MapGet("/test", (DbApiContext db) => 
            ToData.DataProcessing(request, db, "get"));

app.MapPost("/test", (Request request, DbApiContext db) => 
            ToData.DataProcessing(request, db, "pos"));

app.MapPut("/test", (Request request, DbApiContext db) =>
            ToData.DataProcessing(request, db, "put"));

request.request_id = 102;
request.message = "method delete";
app.MapDelete("/test", (DbApiContext db) => 
            ToData.DataProcessing(request, db, "del"));

app.Run();

public class ToData
{
    public async static Task<IResult> DataProcessing(Request request, DbApiContext db, string method)
    {
        if (request is Request rqt)
        {
            SqlParameter param = new SqlParameter("@params", System.Data.SqlDbType.VarChar);
            param.Value = request.RequestToJson();

            var responses = await db.Responses
                            .FromSqlRaw($"EXECUTE {method}.test @params", param)
                            .ToListAsync();

            return responses.Count > 0
                ? Results.Content(responses[0].result!)
                : Results.NoContent();
        }

        return Results.NoContent();

    }
}
