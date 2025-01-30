using DbTracker.CollectorService.Services;

var builder = WebApplication.CreateBuilder(args);

// Port ayarını ekle
builder.WebHost.UseUrls("http://localhost:5201", "https://localhost:7201");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servis kayıtları
builder.Services.AddSingleton<IEventProducerService, KafkaEventProducerService>();
builder.Services.AddHostedService<DatabaseActivitySimulator>();

var app = builder.Build();

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
