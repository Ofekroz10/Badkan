using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a04dd93-01f2-42e0-8448-d5e1c5e92336");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "386e5700-51b0-4e94-af0e-c846bdf1bba3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8492fd86-a247-4e72-9f5d-7f08e4b39c52");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40bc10b7-ce2a-47f5-acbb-22d88ed4fc77", "bfd2f5d5-4f86-4815-b654-ad0f21a18281", "0", "0" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2709e507-aeba-42a8-90b0-d63b6684eb67", "213b78b7-4d8b-4df2-8c96-525714946379", "1", "1" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5ecfce4-0d57-4467-9056-161558f828e0", "59428d97-2f77-4cec-ba48-8420ddb5c37e", "2", "2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2709e507-aeba-42a8-90b0-d63b6684eb67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40bc10b7-ce2a-47f5-acbb-22d88ed4fc77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5ecfce4-0d57-4467-9056-161558f828e0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "386e5700-51b0-4e94-af0e-c846bdf1bba3", "0c83e9b1-21d6-4863-ab61-6b7394d30edf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1a04dd93-01f2-42e0-8448-d5e1c5e92336", "fb627b2c-0655-4432-bdb4-221a76bb24b5", "Lecturer", "LECTURER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8492fd86-a247-4e72-9f5d-7f08e4b39c52", "4392513c-46ee-4202-a1c5-f36b72b7b88c", "Admin", "ADMIN" });
        }
    }
}
