using ISHCatalogServiceBLL.Services;
using ISHCatalogServiceDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddDbContext<CatalogContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CatalogContext>();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://login.microsoftonline.com/289ae2c6-a47c-442a-820e-cdf651a45830/v2.0";
    options.Audience = "2e98bea5-5315-43b5-8dd0-4dc012751687";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = $"https://login.microsoftonline.com/289ae2c6-a47c-442a-820e-cdf651a45830/v2.0",
        ValidateAudience = true,
        ValidAudience = "2e98bea5-5315-43b5-8dd0-4dc012751687",
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
    options.AddPolicy("BuyerPolicy", policy => policy.RequireRole("Buyer"));
});

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();


