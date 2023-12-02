using dotnetCorrelationId.Infra;
using dotnetCorrelationId.Infra.Middleware;
using dotnetCorrelationId.Models;
using dotnetCorrelationId.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [DI]
builder.Services.AddCorrelationIdGenerator();

builder.Services.AddTransient<CreditCardPayment>();
builder.Services.AddTransient<PayPalPayment>();
builder.Services.AddTransient<BitcoinPayment>();
builder.Services.AddTransient(typeof(BaseLogger<>));



builder.Services.AddTransient<IPaymentService, PaymentService>();


#endregion

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

#region [Middler]
app.UseCorrelationMiddleware();
#endregion

app.Run();
