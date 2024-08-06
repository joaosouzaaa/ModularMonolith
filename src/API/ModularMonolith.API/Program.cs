using ModularMonolith.API.Constants;
using ModularMonolith.API.DependencyInjection;
using ModularMonolith.API.Filters;
using ModularMonolith.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers(options => options.Filters.AddService<NotificationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<UnexpectedErrorMiddleware>();
}

app.UseHttpsRedirection();
app.UseCors(CorsPoliciesNamesConstants.CorsPolicy);
app.UseAuthorization();
app.MapControllers();
app.UseDependencyInjection();

app.Run();
