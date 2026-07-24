using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce531.Migrations
{
    /// <inheritdoc />
    public partial class AddDataInBrandModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Brands (name, description, status) values ('Realme', 'Duis consequat dui nec nisi volutpat eleifend.', 0);insert into Brands (name, description, status) values ('Dell', 'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.', 1);insert into Brands (name, description, status) values ('Realme', 'Duis mattis egestas metus.', 1);insert into Brands (name, description, status) values ('Oppo', 'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.', 1);insert into Brands (name, description, status) values ('Apple', 'Ut tellus.', 0);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Brands");
        }
    }
}
