using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Name_Lastname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name_Firstname",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Address_Zipcode",
                table: "Users",
                newName: "Zipcode");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Users",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_Number",
                table: "Users",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Address_Geolocation_Long",
                table: "Users",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Address_Geolocation_Lat",
                table: "Users",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Users",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Zipcode",
                table: "Users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Users",
                newName: "Address_Zipcode");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Users",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Users",
                newName: "Address_Number");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Users",
                newName: "Address_Geolocation_Long");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Users",
                newName: "Address_Geolocation_Lat");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Name_Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Name_Firstname");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Users",
                newName: "Address_City");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Zipcode",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Street",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Geolocation_Long",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Geolocation_Lat",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address_City",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
