using BikeShopREST;
using BikeShopREST.Data;
using BikeShopREST.Interfaces;
using BikeShopREST.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IBikeRepository, BikeRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IReviewRepository,ReviewRepository>();
builder.Services.AddScoped<IContactRepository,ContactRepository>();
builder.Services.AddScoped<IBikeCategoryRepository,BikeCategoryRepository>();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddDbContext<DataContext>(options=>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

//idk
if (args.Length == 1 && args[0].ToLower() == "seeddata")
	SeedData(app);

void SeedData(IHost app)
{
	var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

	using (var scope = scopedFactory.CreateScope())
	{
		var service = scope.ServiceProvider.GetService<Seed>();
		service.SeedDataContext();
	}
}
//idk


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
