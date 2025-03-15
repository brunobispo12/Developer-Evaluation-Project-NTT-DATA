using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    public partial class ChangeCustomerBranchForGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Sales\" " +
                "SET \"Customer\" = '00000000-0000-0000-0000-000000000000' " +
                "WHERE NOT (\"Customer\" ~* '^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$');");

            migrationBuilder.Sql(
                "UPDATE \"Sales\" " +
                "SET \"Branch\" = '00000000-0000-0000-0000-000000000000' " +
                "WHERE NOT (\"Branch\" ~* '^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$');");

            migrationBuilder.Sql(
                "ALTER TABLE \"Sales\" ALTER COLUMN \"Customer\" TYPE uuid USING \"Customer\"::uuid;");

            migrationBuilder.Sql(
                "ALTER TABLE \"Sales\" ALTER COLUMN \"Branch\" TYPE uuid USING \"Branch\"::uuid;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Sales\" ALTER COLUMN \"Customer\" TYPE character varying(100) USING \"Customer\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"Sales\" ALTER COLUMN \"Branch\" TYPE character varying(100) USING \"Branch\"::text;");
        }
    }
}
