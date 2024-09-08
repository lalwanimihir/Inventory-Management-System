using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inventory9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53434386-27b6-44ad-b5df-60f2330a0f76",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbd7806b-acdc-47df-9304-132da6cb7b91", "AQAAAAIAAYagAAAAEBHnt50wYX2HVRz78nAep0Gsk1x1PSzqdUF5oXjvo8D9JtAinuYx3XnkvaLRhQhURg==", "027d5e3b-377a-4f2e-a3c4-dcdc7d4186d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3e40240-48f5-4e68-b371-3d628468993f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8de200c0-687c-42ab-9f2a-821c034998c8", "AQAAAAIAAYagAAAAEKZtZnbhdPhxCaeIViz+k1deHeM7aWMimKV5y7BhcV9QwGGsxQYbJjD7QXtzv8stdQ==", "11f83456-0f03-4fe8-b8b9-92df7594638a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "53434386-27b6-44ad-b5df-60f2330a0f76",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfcdb3a1-365e-4d39-a08d-20721d7e5212", "AQAAAAIAAYagAAAAEM7+7PIscipYZqpebxGodRCxpg+xuwdXYs5v2DXZk9ZOsxo7iiWn4JxJRVfVesBIjQ==", "32c4d3cb-01d0-4d25-9a36-60c2f3b04ea3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3e40240-48f5-4e68-b371-3d628468993f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3f9a603-0930-4864-8701-fa9e06b6ec89", "AQAAAAIAAYagAAAAEEI/hXH1RXqi1sP0VWITPtCy9I3B2uILLDnbgW7t8IwgP4O8fHdeG3S0PCSfcGHlWQ==", "80b8809a-f19b-4f85-bef0-6bf8f7e90ab7" });
        }
    }
}
