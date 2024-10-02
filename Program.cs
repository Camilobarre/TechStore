var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Define the HTML route online
app.MapGet("/", () => Results.Content(@"
    <!DOCTYPE html>
    <html lang='es'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>TechStore</title>
        <style>
            body {
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                height: 100vh;
                margin: 0;
                text-align: center;
            }
            h1 {
                border: red solid;
            }
            p {
                border: blue solid;
            }
        </style>
    </head>
    <body>
        <h1>Welcome to TechStore API</h1>
        <p>Consult the documentation in <a href='/swagger/index.html'>Swagger</a>.</p>
    </body>
    </html>
", "text/html"));

app.MapControllers();

app.Run();
