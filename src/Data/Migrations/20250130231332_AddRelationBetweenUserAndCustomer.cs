using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenUserAndCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_customers_customer_id",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_customers_customer_id",
                table: "AspNetUsers",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_customers_customer_id",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_customers_customer_id",
                table: "AspNetUsers",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id");
        }
    }
}
