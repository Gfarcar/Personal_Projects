using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mes = table.Column<DateOnly>(type: "date", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppClaimId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => new { x.RoleId, x.ClaimType });
                    table.ForeignKey(
                        name: "FK_RoleClaim_Claims_AppClaimId",
                        column: x => x.AppClaimId,
                        principalTable: "Claims",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleClaim_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HorasTrabajadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Horas = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasTrabajadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorasTrabajadas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salarios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "CanManageUsers", "true" },
                    { 2, "CanEditSettings", "true" },
                    { 3, "CanCreateContent", "true" },
                    { 4, "CanEditContent", "true" },
                    { 6, "CanViewContent", "true" },
                    { 7, "CanModerateContent", "true" },
                    { 8, "CanBanUsers", "true" }
                });

            migrationBuilder.InsertData(
                table: "Nominas",
                columns: new[] { "Id", "Cantidad", "Mes" },
                values: new object[,]
                {
                    { 1, 3377, new DateOnly(2024, 1, 1) },
                    { 2, 3656, new DateOnly(2024, 2, 1) },
                    { 3, 2503, new DateOnly(2024, 3, 1) },
                    { 4, 9722, new DateOnly(2024, 4, 1) },
                    { 5, 5409, new DateOnly(2024, 5, 1) },
                    { 6, 9961, new DateOnly(2024, 6, 1) },
                    { 7, 4853, new DateOnly(2024, 7, 1) },
                    { 8, 3020, new DateOnly(2024, 8, 1) },
                    { 9, 5529, new DateOnly(2024, 9, 1) },
                    { 10, 2683, new DateOnly(2024, 10, 1) },
                    { 11, 6661, new DateOnly(2024, 11, 1) },
                    { 12, 1193, new DateOnly(2024, 12, 1) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" },
                    { 3, "Viewer" },
                    { 4, "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Names", "PasswordHash", "SecondLastName" },
                values: new object[,]
                {
                    { 1, "ana.martinezgomez@example.com", "Martinez", "Ana Luisa", "$2a$11$8GbFzvTb685Mw/gHz8GA0O6hWagtc9OqGFqIAkssX5wjRaO2s3kYK", "Gomez" },
                    { 2, "carla.gomezlopez@example.com", "Gomez", "Carla", "$2a$11$TvUDqiOqoVB6k7ezCzL1..ANocmRX8zKC7zWBUYdRcPwbMEnpJk.q", "Lopez" },
                    { 3, "miguel.torresramirez@example.com", "Torres", "Miguel", "$2a$11$Ss92CixVmKHzXDwbIUQyq.EQqj0Zjtp3willndK/enm4sJhjVxjQy", "Ramirez" },
                    { 4, "laura.rodriguezfernandez@example.com", "Rodriguez", "Laura", "$2a$11$bafwibzDQ.FlrN5uvoHYPOOzC7KnganzKFIiqbMMG.I7c8H9JmW0K", "Fernandez" },
                    { 5, "juan.sanchezmartinez@example.com", "Sanchez", "Juan Carlos", "$2a$11$21fu9PLRgBD5JjpOCILP1u2a09u0iPtYu1ppYXV/Ae3qgXi4DtAie", "Martinez" },
                    { 6, "sofia.ramirezcastillo@example.com", "Ramirez", "Sofia", "$2a$11$DDY9mGl/lxqGIf2xg15dFOlnndeiCHw04F525RF1jYWVrFrZ.qUYi", "Castillo" },
                    { 7, "andres.lopezcastro@example.com", "Lopez", "Andres", "$2a$11$MOXMZDGvybd/wwWVdR//tO9GCyYE5.9Ytkiqz3CNte0BRwubUyrvO", "Castro" },
                    { 8, "valentina.castroperez@example.com", "Castro", "Valentina", "$2a$11$OPEC7dJt2gKWQpQTxOjk1eijTz8bW1APV/Bsf/8Z0e/Pqkvvi.n/6", "Perez" },
                    { 9, "pedro.jimenezmorales@example.com", "Jimenez", "Pedro", "$2a$11$3C1yL94Yj3/skILDJEWu1u3834GDRYtBxvl0WSsXdeA6CtFAV.WJq", "Morales" },
                    { 10, "mariana.diaherrera@example.com", "Diaz", "Mariana", "$2a$11$B5vGwBU6isSGLkmh7a7FUeR0eVnPiOPtyGEsJxKrsQGqJ.V5B1FvC", "Herrera" },
                    { 11, "javier.moralesjimenez@example.com", "Morales", "Javier", "$2a$11$Sng6dX9jQ0GzwgtpFzaib.Iz/j73/M94HJZKlfUzhiIZrVbL1akm.", "Jimenez" },
                    { 12, "gabriela.fernandeztorres@example.com", "Fernandez", "Gabriela", "$2a$11$JzGFen3SJ330.Km0fUtLS.0Pgqgo1BxHDl85ErC9ItTB6XaNR8tNS", "Torres" },
                    { 13, "tomas.herrera.mendoza@example.com", "Herrera", "Tomas", "$2a$11$x59/dB5GtmCfbw9MNvzuv.LxabN3R/JcXOmVbFL6M4SQ4TtX/sS/S", "Mendoza" },
                    { 14, "natalia.mendozarodriguez@example.com", "Mendoza", "Natalia", "$2a$11$pQDPSJvwD08mDb08fPl3B.Im/a5bTCAKWwUraFqqGQBJhLBSYONSC", "Rodriguez" },
                    { 15, "fernando.ruizsanchez@example.com", "Ruiz", "Luis Fernando", "$2a$11$BYx2qOa3O/l041Xjr7SK2.r.6e2HQkOKMRFYYvJqRcDD/ViQhdxUy", "Sanchez" }
                });

            migrationBuilder.InsertData(
                table: "HorasTrabajadas",
                columns: new[] { "Id", "Fecha", "Horas", "UserId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 31), 8, 1 },
                    { 2, new DateOnly(2024, 1, 31), 6, 2 },
                    { 3, new DateOnly(2024, 1, 31), 7, 3 },
                    { 4, new DateOnly(2024, 1, 31), 5, 4 },
                    { 5, new DateOnly(2024, 1, 31), 8, 5 },
                    { 6, new DateOnly(2024, 1, 31), 9, 6 },
                    { 7, new DateOnly(2024, 1, 31), 6, 7 },
                    { 8, new DateOnly(2024, 1, 31), 7, 8 },
                    { 9, new DateOnly(2024, 1, 31), 8, 9 },
                    { 10, new DateOnly(2024, 1, 31), 7, 10 },
                    { 11, new DateOnly(2024, 1, 31), 8, 11 },
                    { 12, new DateOnly(2024, 1, 31), 9, 12 },
                    { 13, new DateOnly(2024, 1, 31), 6, 13 },
                    { 14, new DateOnly(2024, 1, 31), 7, 14 },
                    { 15, new DateOnly(2024, 1, 31), 8, 15 }
                });

            migrationBuilder.InsertData(
                table: "RoleClaim",
                columns: new[] { "ClaimType", "RoleId", "AppClaimId", "ClaimValue" },
                values: new object[,]
                {
                    { "CanEditSettings", 1, null, "true" },
                    { "CanManageUsers", 1, null, "true" },
                    { "CanCreateContent", 2, null, "true" },
                    { "CanDeleteContent", 2, null, "true" },
                    { "CanEditContent", 2, null, "true" },
                    { "CanViewContent", 3, null, "true" },
                    { "CanBanUsers", 4, null, "true" },
                    { "CanModerateContent", 4, null, "true" }
                });

            migrationBuilder.InsertData(
                table: "Salarios",
                columns: new[] { "Id", "Fecha", "Monto", "UserId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 1, 31), 1500.00m, 1 },
                    { 2, new DateOnly(2024, 1, 31), 1600.00m, 2 },
                    { 3, new DateOnly(2024, 1, 31), 1700.00m, 3 },
                    { 4, new DateOnly(2024, 1, 31), 1800.00m, 4 },
                    { 5, new DateOnly(2024, 1, 31), 1900.00m, 5 },
                    { 6, new DateOnly(2024, 1, 31), 2000.00m, 6 },
                    { 7, new DateOnly(2024, 1, 31), 2100.00m, 7 },
                    { 8, new DateOnly(2024, 1, 31), 2200.00m, 8 },
                    { 9, new DateOnly(2024, 1, 31), 2300.00m, 9 },
                    { 10, new DateOnly(2024, 1, 31), 2400.00m, 10 },
                    { 11, new DateOnly(2024, 1, 31), 2500.00m, 11 },
                    { 12, new DateOnly(2024, 1, 31), 2600.00m, 12 },
                    { 13, new DateOnly(2024, 1, 31), 2700.00m, 13 },
                    { 14, new DateOnly(2024, 1, 31), 2800.00m, 14 },
                    { 15, new DateOnly(2024, 1, 31), 2900.00m, 15 }
                });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "CanManageUsers", "true", 1 },
                    { 2, "CanEditSettings", "true", 1 },
                    { 3, "CanCreateContent", "true", 2 },
                    { 4, "CanEditContent", "true", 2 },
                    { 5, "CanDeleteContent", "true", 2 },
                    { 6, "CanViewContent", "true", 3 },
                    { 7, "CanModerateContent", "true", 4 },
                    { 8, "CanBanUsers", "true", 4 },
                    { 9, "CanViewContent", "true", 5 },
                    { 10, "CanCreateContent", "true", 6 },
                    { 11, "CanEditContent", "true", 6 },
                    { 12, "CanDeleteContent", "true", 6 },
                    { 13, "CanViewContent", "true", 7 },
                    { 14, "CanModerateContent", "true", 8 },
                    { 15, "CanViewContent", "true", 9 },
                    { 16, "CanCreateContent", "true", 10 },
                    { 17, "CanEditContent", "true", 10 },
                    { 18, "CanViewContent", "true", 11 },
                    { 19, "CanModerateContent", "true", 12 },
                    { 20, "CanViewContent", "true", 13 },
                    { 21, "CanCreateContent", "true", 14 },
                    { 22, "CanManageUsers", "true", 15 },
                    { 23, "CanEditSettings", "true", 15 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 3, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 3, 9 },
                    { 2, 10 },
                    { 3, 11 },
                    { 4, 12 },
                    { 3, 13 },
                    { 2, 14 },
                    { 1, 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorasTrabajadas_UserId",
                table: "HorasTrabajadas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_AppClaimId",
                table: "RoleClaim",
                column: "AppClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Salarios_UserId",
                table: "Salarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorasTrabajadas");

            migrationBuilder.DropTable(
                name: "Nominas");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "Salarios");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
