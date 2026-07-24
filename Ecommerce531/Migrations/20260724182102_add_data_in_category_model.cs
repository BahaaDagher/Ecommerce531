using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce531.Migrations
{
    /// <inheritdoc />
    public partial class add_data_in_category_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (name, description, status) values ('Mobiles', 'Fast and convenient air pump for inflating toys and furniture.', 1);insert into Categories (name, description, status) values ('Laptops', 'Savory sausage links with a hint of maple flavor.', 1);insert into Categories (name, description, status) values ('Tablets', 'Stylish watering can for plants with easy pouring nozzle.', 1);insert into Categories (name, description, status) values ('Accessories', 'Ultrasonic essential oil diffuser with LED lights.', 0);insert into Categories (name, description, status) values ('Gaming', 'Creamy yogurt with probiotics, available in various flavors.', 0);"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categories");
        }

    }
}
