using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.UnitOfWork;
using Web.Models.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", build =>
{
    build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
}));


/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();

app.UseCors("CorsPolicy");

/*app.UseCors();
*/
app.UseAuthorization();

app.Run();
