using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuPermissions",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    CanRender = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermissions", x => new { x.RoleId, x.MenuItemId });
                    table.ForeignKey(
                        name: "FK_MenuPermissions_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Href", "Icon", "Title" },
                values: new object[,]
                {
                    { 1, "/empleados", "IconAperture", "Empleados" },
                    { 2, "/nomina", "IconAperture", "Nomina" },
                    { 3, "/salarios", "IconAperture", "Salarios" },
                    { 4, "/horasTrabajadas", "IconAperture", "Horas Trabajadas" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$yXJFxry8Sxa8tCuAGoyouuI9hx.lkg1ogG7hpFfKX7nvoibjCe6gK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$upfeMzPqgnhZYYOP/kXNs.d2BkUNRgRSPVnZnECOctXNQyblG0uB6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$snInsWmum2ImY1dgyDCwgOt6RXXmElxO54rJogd.ntQ2km7uekiYe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$Swf0Cs0vDDFaN2yl4fZaEOxuI04JPskAe4fcb/Po8OllBoy16bE.2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$2ikBB.PAWwIhWZu8i2dlkuU0S0gQJBlYddR.31rRi4ozoXiwNH.AO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$am8D6Xj.mDd/HOZm5UXJmuYN/4z6DUl7wuLpp1bEvrm2DmQX/7xuG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$CQBNKjmHWPZg4N0nwOKC/OKa9KNk.D73BcmOMPFyY9AanVZ5/r4qS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$St5i2/2NgLnJAj9zHgOZ3OkJOBgNCez9yoLDmXQlMF2Uan0J1x2AG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$pkESCATiy7di52Zg/syUK.oP6n30ufQ7RWkxkx614xbmWyiJLG666");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$tpY66iZ6gujxoYsyzWa3u.7jrO60sQ/v5z.Z9SyuSZqaKV/uylWdy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$MwmRf2cVwIGf/yWCQ.L5veRqwuEinCxVCa.op3NSyCAUPzfAo42QO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$cWdRDFCFyoMatb/Ed2VjheLtVFte2aT6GUPfn6u2dAlc9oRJuYifO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "PasswordHash",
                value: "$2a$11$Fld/kPm5q1ax6gqgV4Blk.WldHMcbhkDHIsT/Po2ix/pA5hm3VFea");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "PasswordHash",
                value: "$2a$11$uMXeiyTV9iJIy3z.kdvSRe5LLA.CPh7.MYMcPLKCo7A7TlHBinKni");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "PasswordHash",
                value: "$2a$11$2hoT.dqzki9I07L4UHoDr.CIpjkfYq39oqaLHzLn/0VVIXLYcu9Ne");

            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "MenuItemId", "RoleId", "CanRender" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 2, true },
                    { 3, 3, true },
                    { 4, 4, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermissions_MenuItemId",
                table: "MenuPermissions",
                column: "MenuItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuPermissions");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$8GbFzvTb685Mw/gHz8GA0O6hWagtc9OqGFqIAkssX5wjRaO2s3kYK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$TvUDqiOqoVB6k7ezCzL1..ANocmRX8zKC7zWBUYdRcPwbMEnpJk.q");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$Ss92CixVmKHzXDwbIUQyq.EQqj0Zjtp3willndK/enm4sJhjVxjQy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$bafwibzDQ.FlrN5uvoHYPOOzC7KnganzKFIiqbMMG.I7c8H9JmW0K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$21fu9PLRgBD5JjpOCILP1u2a09u0iPtYu1ppYXV/Ae3qgXi4DtAie");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$DDY9mGl/lxqGIf2xg15dFOlnndeiCHw04F525RF1jYWVrFrZ.qUYi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$MOXMZDGvybd/wwWVdR//tO9GCyYE5.9Ytkiqz3CNte0BRwubUyrvO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$OPEC7dJt2gKWQpQTxOjk1eijTz8bW1APV/Bsf/8Z0e/Pqkvvi.n/6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$3C1yL94Yj3/skILDJEWu1u3834GDRYtBxvl0WSsXdeA6CtFAV.WJq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$B5vGwBU6isSGLkmh7a7FUeR0eVnPiOPtyGEsJxKrsQGqJ.V5B1FvC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$Sng6dX9jQ0GzwgtpFzaib.Iz/j73/M94HJZKlfUzhiIZrVbL1akm.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$JzGFen3SJ330.Km0fUtLS.0Pgqgo1BxHDl85ErC9ItTB6XaNR8tNS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "PasswordHash",
                value: "$2a$11$x59/dB5GtmCfbw9MNvzuv.LxabN3R/JcXOmVbFL6M4SQ4TtX/sS/S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "PasswordHash",
                value: "$2a$11$pQDPSJvwD08mDb08fPl3B.Im/a5bTCAKWwUraFqqGQBJhLBSYONSC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "PasswordHash",
                value: "$2a$11$BYx2qOa3O/l041Xjr7SK2.r.6e2HQkOKMRFYYvJqRcDD/ViQhdxUy");
        }
    }
}
