using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersistenceLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCostinDeliverymethodtoprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "DeliveryMethod",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DeliveryMethod",
                newName: "Cost");
        }
    }
}
