using authbackend.Entities;
using authbackend.Interface;
using Microsoft.EntityFrameworkCore;

namespace authbackend.Data;

public class UserContext : DbContext 
{
    private readonly IPasswordHashingService _passwordHashingService;


    public UserContext(DbContextOptions<UserContext> options, IPasswordHashingService passwordHashingService) : base(options)
    {
        _passwordHashingService = passwordHashingService;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<RoleClaim> RoleClaim { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AppClaim> Claims { get; set; }
    public DbSet<Nomina> Nominas { get; set; }
    public DbSet<HorasTrabajadas> HorasTrabajadas { get; set; }
    public DbSet<Salario> Salarios { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; } // Para la tabla de MenuItem
    public DbSet<MenuPermissionsTable> MenuPermissions { get; set; } // Para la tabla de MenuPermissionsTable



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<MenuPermissionsTable>()
        .HasKey(mp => new { mp.RoleId, mp.MenuItemId });

        modelBuilder.Entity<MenuPermissionsTable>()
            .HasOne(mp => mp.Role)
            .WithMany(r => r.MenuPermissions)
            .HasForeignKey(mp => mp.RoleId);

        modelBuilder.Entity<MenuPermissionsTable>()
            .HasOne(mp => mp.MenuItem)
            .WithMany(mi => mi.MenuPermissions)
            .HasForeignKey(mp => mp.MenuItemId);

        modelBuilder.Entity<Salario>()
            .Property(s => s.Monto)
            .HasColumnType("decimal(18, 2)"); 
            
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        // Configurar UserRoles (muchos a muchos)
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        // Configurar UserClaims (uno a muchos
        modelBuilder.Entity<UserClaim>()
        .HasKey(uc => uc.Id); // Define la clave primaria

        modelBuilder.Entity<UserClaim>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserClaims)
            .HasForeignKey(uc => uc.UserId);

        // Configurar UserTokens (uno a muchos)
        modelBuilder.Entity<UserToken>()
            .HasOne(ut => ut.User)
            .WithMany(u => u.UserTokens)
            .HasForeignKey(ut => ut.UserId);

        // Configurar UserRefreshTokens (uno a muchos)
        modelBuilder.Entity<UserRefreshToken>()
            .HasOne(urt => urt.User)
            .WithMany(u => u.UserRefreshToken)
            .HasForeignKey(urt => urt.UserId);
        
        // Configurar RoleClaims (muchos a muchos)
        modelBuilder.Entity<RoleClaim>()
            .HasKey(rc => new { rc.RoleId, rc.ClaimType });

        modelBuilder.Entity<RoleClaim>()
            .HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaim)
            .HasForeignKey(rc => rc.RoleId);        

        modelBuilder.Entity<HorasTrabajadas>()
            .HasOne(hr => hr.User)
            .WithMany(u => u.HorasTrabajadas)
            .HasForeignKey(hr => hr.UserId); // Asegúrate de definir la clave foránea

        modelBuilder.Entity<Salario>() 
            .HasOne(hr => hr.User)
            .WithMany(u => u.Salarios)
            .HasForeignKey(hr => hr.UserId); // Asegúrate de definir la clave foránea


        base.OnModelCreating(modelBuilder); 

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Editor" },
            new Role { Id = 3, Name = "Viewer" },
            new Role { Id = 4, Name = "Moderator" }
        );

        // Seed Claims
        modelBuilder.Entity<AppClaim>().HasData(
            new AppClaim { Id = 1, Type = "CanManageUsers", Value = "true" },
            new AppClaim { Id = 2, Type = "CanEditSettings", Value = "true" },
            new AppClaim { Id = 3, Type = "CanCreateContent", Value = "true" },
            new AppClaim { Id = 4, Type = "CanEditContent", Value = "true" },
            new AppClaim { Id = 6, Type = "CanViewContent", Value = "true" },
            new AppClaim { Id = 7, Type = "CanModerateContent", Value = "true" },
            new AppClaim { Id = 8, Type = "CanBanUsers", Value = "true" }
        );

        // Seed RoleClaims
        modelBuilder.Entity<RoleClaim>().HasData(
            new RoleClaim { RoleId = 1, ClaimType = "CanManageUsers", ClaimValue = "true" },
            new RoleClaim { RoleId = 1, ClaimType = "CanEditSettings", ClaimValue = "true" },
            new RoleClaim { RoleId = 2, ClaimType = "CanCreateContent", ClaimValue = "true" },
            new RoleClaim { RoleId = 2, ClaimType = "CanEditContent", ClaimValue = "true" },
            new RoleClaim { RoleId = 2, ClaimType = "CanDeleteContent", ClaimValue = "true" },
            new RoleClaim { RoleId = 3, ClaimType = "CanViewContent", ClaimValue = "true" },
            new RoleClaim { RoleId = 4, ClaimType = "CanModerateContent", ClaimValue = "true" },
            new RoleClaim { RoleId = 4, ClaimType = "CanBanUsers", ClaimValue = "true" }
        );

        modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Names = "Ana Luisa", LastName = "Martinez", SecondLastName = "Gomez", Email = "ana.martinezgomez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 2, Names = "Carla", LastName = "Gomez", SecondLastName = "Lopez", Email = "carla.gomezlopez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 3, Names = "Miguel", LastName = "Torres", SecondLastName = "Ramirez", Email = "miguel.torresramirez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 4, Names = "Laura", LastName = "Rodriguez", SecondLastName = "Fernandez", Email = "laura.rodriguezfernandez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 5, Names = "Juan Carlos", LastName = "Sanchez", SecondLastName = "Martinez", Email = "juan.sanchezmartinez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 6, Names = "Sofia", LastName = "Ramirez", SecondLastName = "Castillo", Email = "sofia.ramirezcastillo@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 7, Names = "Andres", LastName = "Lopez", SecondLastName = "Castro", Email = "andres.lopezcastro@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 8, Names = "Valentina", LastName = "Castro", SecondLastName = "Perez", Email = "valentina.castroperez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 9, Names = "Pedro", LastName = "Jimenez", SecondLastName = "Morales", Email = "pedro.jimenezmorales@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 10, Names = "Mariana", LastName = "Diaz", SecondLastName = "Herrera", Email = "mariana.diaherrera@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 11, Names = "Javier", LastName = "Morales", SecondLastName = "Jimenez", Email = "javier.moralesjimenez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 12, Names = "Gabriela", LastName = "Fernandez", SecondLastName = "Torres", Email = "gabriela.fernandeztorres@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 13, Names = "Tomas", LastName = "Herrera", SecondLastName = "Mendoza", Email = "tomas.herrera.mendoza@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 14, Names = "Natalia", LastName = "Mendoza", SecondLastName = "Rodriguez", Email = "natalia.mendozarodriguez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") },
                new User { Id = 15, Names = "Luis Fernando", LastName = "Ruiz", SecondLastName = "Sanchez", Email = "fernando.ruizsanchez@example.com", PasswordHash = _passwordHashingService.HashPassword("Mexicali#11") }
        );

        modelBuilder.Entity<Nomina>().HasData(
           new Nomina { Id = 1, Mes = new DateOnly(2024, 1, 1), Cantidad = 3377 },
            new Nomina { Id = 2, Mes = new DateOnly(2024, 2, 1), Cantidad = 3656 },
            new Nomina { Id = 3, Mes = new DateOnly(2024, 3, 1), Cantidad = 2503 },
            new Nomina { Id = 4, Mes = new DateOnly(2024, 4, 1), Cantidad = 9722 },
            new Nomina { Id = 5, Mes = new DateOnly(2024, 5, 1), Cantidad = 5409 },
            new Nomina { Id = 6, Mes = new DateOnly(2024, 6, 1), Cantidad = 9961 },
            new Nomina { Id = 7, Mes = new DateOnly(2024, 7, 1), Cantidad = 4853 },
            new Nomina { Id = 8, Mes = new DateOnly(2024, 8, 1), Cantidad = 3020 },
            new Nomina { Id = 9, Mes = new DateOnly(2024, 9, 1), Cantidad = 5529 },
            new Nomina { Id = 10, Mes = new DateOnly(2024, 10, 1), Cantidad = 2683 },
            new Nomina { Id = 11, Mes = new DateOnly(2024, 11, 1), Cantidad = 6661 },
            new Nomina { Id = 12, Mes = new DateOnly(2024, 12, 1), Cantidad = 1193 }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 }, // Ana Luisa - Admin
            new UserRole { UserId = 2, RoleId = 2 }, // Carla - Editor
            new UserRole { UserId = 3, RoleId = 3 }, // Miguel - Viewer
            new UserRole { UserId = 4, RoleId = 4 }, // Laura - Moderator
            new UserRole { UserId = 5, RoleId = 3 }, // Juan Carlos - Viewer
            new UserRole { UserId = 6, RoleId = 2 }, // Sofia - Editor
            new UserRole { UserId = 7, RoleId = 3 }, // Andres - Viewer
            new UserRole { UserId = 8, RoleId = 4 }, // Valentina - Moderator
            new UserRole { UserId = 9, RoleId = 3 }, // Pedro - Viewer
            new UserRole { UserId = 10, RoleId = 2 }, // Mariana - Editor
            new UserRole { UserId = 11, RoleId = 3 }, // Javier - Viewer
            new UserRole { UserId = 12, RoleId = 4 }, // Gabriela - Moderator
            new UserRole { UserId = 13, RoleId = 3 }, // Tomas - Viewer
            new UserRole { UserId = 14, RoleId = 2 }, // Natalia - Editor
            new UserRole { UserId = 15, RoleId = 1 }  // Luis Fernando - Admin
        );

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem{Id = 1, Title = "Empleados", Icon = "IconAperture", Href= "/empleados"}, 
            new MenuItem{Id = 2, Title = "Nomina", Icon = "IconAperture", Href= "/nomina"}, 
            new MenuItem{Id = 3, Title = "Salarios", Icon = "IconAperture", Href= "/salarios"}, 
            new MenuItem{Id = 4, Title = "Horas Trabajadas", Icon = "IconAperture", Href= "/horasTrabajadas"}
        ); 

        modelBuilder.Entity<MenuPermissionsTable>().HasData(
            new MenuPermissionsTable{RoleId = 1, MenuItemId = 1, CanRender = true}, 
            new MenuPermissionsTable{RoleId = 2, MenuItemId = 2, CanRender = true}, 
            new MenuPermissionsTable{RoleId = 3, MenuItemId = 3, CanRender = true}, 
            new MenuPermissionsTable{RoleId = 4, MenuItemId = 4, CanRender = true},
            new MenuPermissionsTable{RoleId = 1, MenuItemId = 2, CanRender = true}, 
            new MenuPermissionsTable{RoleId = 1, MenuItemId = 3, CanRender = true}, 
            new MenuPermissionsTable{RoleId = 1, MenuItemId = 4, CanRender = true}
        ); 

        modelBuilder.Entity<UserClaim>().HasData(
            new UserClaim { Id = 1, UserId = 1, ClaimType = "CanManageUsers", ClaimValue = "true" }, // Ana Luisa - Admin
            new UserClaim { Id = 2, UserId = 1, ClaimType = "CanEditSettings", ClaimValue = "true" },
            
            new UserClaim { Id = 3, UserId = 2, ClaimType = "CanCreateContent", ClaimValue = "true" }, // Carla - Editor
            new UserClaim { Id = 4, UserId = 2, ClaimType = "CanEditContent", ClaimValue = "true" },
            new UserClaim { Id = 5, UserId = 2, ClaimType = "CanDeleteContent", ClaimValue = "true" },
            
            new UserClaim { Id = 6, UserId = 3, ClaimType = "CanViewContent", ClaimValue = "true" }, // Miguel - Viewer
            
            new UserClaim { Id = 7, UserId = 4, ClaimType = "CanModerateContent", ClaimValue = "true" }, // Laura - Moderator
            new UserClaim { Id = 8, UserId = 4, ClaimType = "CanBanUsers", ClaimValue = "true" },
            
            new UserClaim { Id = 9, UserId = 5, ClaimType = "CanViewContent", ClaimValue = "true" }, // Juan Carlos - Viewer
            
            new UserClaim { Id = 10, UserId = 6, ClaimType = "CanCreateContent", ClaimValue = "true" }, // Sofia - Editor
            new UserClaim { Id = 11, UserId = 6, ClaimType = "CanEditContent", ClaimValue = "true" },
            new UserClaim { Id = 12, UserId = 6, ClaimType = "CanDeleteContent", ClaimValue = "true" },
            
            new UserClaim { Id = 13, UserId = 7, ClaimType = "CanViewContent", ClaimValue = "true" }, // Andres - Viewer
            
            new UserClaim { Id = 14, UserId = 8, ClaimType = "CanModerateContent", ClaimValue = "true" }, // Valentina - Moderator
            new UserClaim { Id = 15, UserId = 9, ClaimType = "CanViewContent", ClaimValue = "true" }, // Pedro - Viewer
            
            new UserClaim { Id = 16, UserId = 10, ClaimType = "CanCreateContent", ClaimValue = "true" }, // Mariana - Editor
            new UserClaim { Id = 17, UserId = 10, ClaimType = "CanEditContent", ClaimValue = "true" },
            new UserClaim { Id = 18, UserId = 11, ClaimType = "CanViewContent", ClaimValue = "true" }, // Javier - Viewer
            
            new UserClaim { Id = 19, UserId = 12, ClaimType = "CanModerateContent", ClaimValue = "true" }, // Gabriela - Moderator
            
            new UserClaim { Id = 20, UserId = 13, ClaimType = "CanViewContent", ClaimValue = "true" }, // Tomas - Viewer
            
            new UserClaim { Id = 21, UserId = 14, ClaimType = "CanCreateContent", ClaimValue = "true" }, // Natalia - Editor
            
            new UserClaim { Id = 22, UserId = 15, ClaimType = "CanManageUsers", ClaimValue = "true" }, // Luis Fernando - Admin
            new UserClaim { Id = 23, UserId = 15, ClaimType = "CanEditSettings", ClaimValue = "true" }
        );

        modelBuilder.Entity<HorasTrabajadas>().HasData(
            new HorasTrabajadas { Id = 1, UserId = 1, Fecha = new DateOnly(2024, 1, 31), Horas = 8 }, // Ana Luisa
            new HorasTrabajadas { Id = 2, UserId = 2, Fecha = new DateOnly(2024, 1, 31), Horas = 6 }, // Carla
            new HorasTrabajadas { Id = 3, UserId = 3, Fecha = new DateOnly(2024, 1, 31), Horas = 7 }, // Miguel
            new HorasTrabajadas { Id = 4, UserId = 4, Fecha = new DateOnly(2024, 1, 31), Horas = 5 }, // Laura
            new HorasTrabajadas { Id = 5, UserId = 5, Fecha = new DateOnly(2024, 1, 31), Horas = 8 }, // Juan Carlos
            new HorasTrabajadas { Id = 6, UserId = 6, Fecha = new DateOnly(2024, 1, 31), Horas = 9 }, // Sofia
            new HorasTrabajadas { Id = 7, UserId = 7, Fecha = new DateOnly(2024, 1, 31), Horas = 6 }, // Andres
            new HorasTrabajadas { Id = 8, UserId = 8, Fecha = new DateOnly(2024, 1, 31), Horas = 7 }, // Valentina
            new HorasTrabajadas { Id = 9, UserId = 9, Fecha = new DateOnly(2024, 1, 31), Horas = 8 }, // Pedro
            new HorasTrabajadas { Id = 10, UserId = 10, Fecha = new DateOnly(2024, 1, 31), Horas = 7 }, // Mariana
            new HorasTrabajadas { Id = 11, UserId = 11, Fecha = new DateOnly(2024, 1, 31), Horas = 8 }, // Luis
            new HorasTrabajadas { Id = 12, UserId = 12, Fecha = new DateOnly(2024, 1, 31), Horas = 9 }, // Teresa
            new HorasTrabajadas { Id = 13, UserId = 13, Fecha = new DateOnly(2024, 1, 31), Horas = 6 }, // Sergio
            new HorasTrabajadas { Id = 14, UserId = 14, Fecha = new DateOnly(2024, 1, 31), Horas = 7 }, // Gabriela
            new HorasTrabajadas { Id = 15, UserId = 15, Fecha = new DateOnly(2024, 1, 31), Horas = 8 } // Fernando
        );

        modelBuilder.Entity<Salario>().HasData(
            new Salario { Id = 1, Monto = 1500.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 1 }, // Ana Luisa
            new Salario { Id = 2, Monto = 1600.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 2 }, // Carla
            new Salario { Id = 3, Monto = 1700.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 3 }, // Miguel
            new Salario { Id = 4, Monto = 1800.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 4 }, // Laura
            new Salario { Id = 5, Monto = 1900.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 5 }, // Juan Carlos
            new Salario { Id = 6, Monto = 2000.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 6 }, // Sofia
            new Salario { Id = 7, Monto = 2100.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 7 }, // Andres
            new Salario { Id = 8, Monto = 2200.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 8 }, // Valentina
            new Salario { Id = 9, Monto = 2300.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 9 }, // Pedro
            new Salario { Id = 10, Monto = 2400.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 10 }, // Mariana
            new Salario { Id = 11, Monto = 2500.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 11 }, // Luis
            new Salario { Id = 12, Monto = 2600.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 12 }, // Teresa
            new Salario { Id = 13, Monto = 2700.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 13 }, // Sergio
            new Salario { Id = 14, Monto = 2800.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 14 }, // Gabriela
            new Salario { Id = 15, Monto = 2900.00m, Fecha = new DateOnly(2024, 1, 31), UserId = 15 } // Fernando
        );



        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetSchema(null);
        }

    }
}