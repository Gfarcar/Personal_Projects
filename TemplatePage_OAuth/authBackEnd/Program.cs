using authbackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using authbackend.Interface;
using authbackend.Service;
using authbackend.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión y el contexto de base de datos
var connString = builder.Configuration.GetConnectionString("UsersDb");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connString)); //Cambiar a develop o produccion
builder.Services.AddScoped<ItokenService, TokenService>(); 
builder.Services.AddScoped<IRoleClaimAssignService, RoleClaimAssignService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();



// Configura la autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme = 
    options.DefaultScheme =
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme; 
})  
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
        ValidateLifetime = true
    };
});

// Agrega soporte para controladores
builder.Services.AddControllers().AddNewtonsoftJson();

// Configura políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

// Agrega autorización
builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Editor", policy => policy.RequireRole("Editor"));
    options.AddPolicy("Viewer", policy => policy.RequireRole("Viewer"));
    options.AddPolicy("Moderator", policy => policy.RequireRole("Moderator"));
}); 

var app = builder.Build();
app.UseRouting();

// Aplica políticas de CORS
app.UseCors("NuevaPolitica");

// Usa autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configura el enrutamiento y las rutas de los controladores
app.MapControllers(); // Mapea las rutas de los controladores

await app.MigrateDbAsync(); // Asegúrate de que este método sea parte de tu lógica de inicialización


app.Run();
