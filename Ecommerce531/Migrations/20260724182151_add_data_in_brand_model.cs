using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce531.Migrations
{
    /// <inheritdoc />
    public partial class add_data_in_brand_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Brands (name, description, status) values ('Apple', 'Reusable gel ice pack for injuries and cooling.', 1);insert into Brands (name, description, status) values ('Samsung', 'Aromatic fresh basil, perfect for Italian cooking.', 0);insert into Brands (name, description, status) values ('Xiaomi', 'Rich and creamy Caesar dressing for salads and wraps.', 0);insert into Brands (name, description, status) values ('Sony', 'A light and tangy dressing with poppy seeds, perfect for salads.', 0);insert into Brands (name, description, status) values ('Dell', 'Soft and breathable pillowcase for body pillows.', 0);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Brands");
        }


    }
}
