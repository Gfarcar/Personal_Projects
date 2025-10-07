using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace authBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class NuevosPermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuPermissions",
                columns: new[] { "MenuItemId", "RoleId", "CanRender" },
                values: new object[,]
                {
                    { 2, 1, true },
                    { 3, 1, true },
                    { 4, 1, true }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$U3rsxH46k0eJuTGuOPZOF..1woP00W8Z6YGWfLPXdC24t.dY/uyue");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$kqZ4lbgHRywi4rMNisOjJ.g9q3WizwxGWFremPWve92joXqUX1W7y");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$NZiKQpluxdVNEyr5KhdDhePIECmZwZpYB3WkDc0eiGtKS1Lmo/Av6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$cXUmYSkNoFlP5BgYEFnWsuAXdOPa1j70wR0x0NpEaXwk5mTUK6tay");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$WH7a/4/rmIQIzdxPrPQ8Cu95hLU1oNnbhAZH4lESNVMbu1XO3aHnG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$h1j5rqFAzGAopfVT4VxZMepaKc.W1isqDKetu37b8UlfqdvuCab/W");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$Oq02fKMzNXp4XXZDcQ.GB.4Dgc8yH3ycJ.E46OvVmU/ezWFJH53wC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$IFS18c.TVk7zoAChH6/dZOP8I29.RvYfv7WhxszMkUt3XytL/k5OW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$v88j.UPXrHVotvJUoA.au.a6c1wAOAjMRWZ.JF.wipAmrlnW.G/86");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$ZbjM0iWcaOTAIEUPUxaDr.omOCH5CgSBu4UoP1AkyWlqDtI3tFUDq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$NQkxi2phNx74SmNW0eb5NOrPiJLV2kH1ugTE27AVW1WVWLC1KeWfC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$B.QR/EIas8g7lErNNWldpuO2iY/dX.4WaVwGE07rtoFMjn/aXaVQu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "PasswordHash",
                value: "$2a$11$vtWw6vqq/bvIBl9x2GMEHOnUB71jNrT24Xvvij1Cjt41GJmM.FovS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "PasswordHash",
                value: "$2a$11$mK43xzFcIo/w3YyKi5Yk3OOW0Z9qr6diWaoKUTS7GWd7dzZEJ5vmq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "PasswordHash",
                value: "$2a$11$k7aHpm//ZL8dvuNuzwBZFOjemoWsHg/VrS7/Wl6dkO4RRlwr3WzNi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuPermissions",
                keyColumns: new[] { "MenuItemId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "MenuPermissions",
                keyColumns: new[] { "MenuItemId", "RoleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "MenuPermissions",
                keyColumns: new[] { "MenuItemId", "RoleId" },
                keyValues: new object[] { 4, 1 });

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
        }
    }
}
