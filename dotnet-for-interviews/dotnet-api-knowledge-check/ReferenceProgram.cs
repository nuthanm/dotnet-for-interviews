/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// hint: This one adds controller actions methods in swagger or even accessible directly but gives you 404 if no app.MapControllers();
// Todo: Remove this line while sharing this code for interviee
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

// hint: Optional for basic controller test
//app.UseAuthorization();

// hint: Once you add this along wiht builder.Services.AddController() then it works as expected.
// Todo: Remove this line while sharing this code for interviee
app.MapControllers();

app.Run();
*/