using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce531.Migrations
{
    /// <inheritdoc />
    public partial class AddDataInCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (name, description, status) values ('Laptops', 'Nunc rhoncus dui vel sem.', 1);insert into Categories (name, description, status) values ('Tablets', 'Donec vitae nisi.', 1);insert into Categories (name, description, status) values ('Tablets', 'Praesent lectus.', 1);insert into Categories (name, description, status) values ('Tablets', 'Ut tellus.', 1);insert into Categories (name, description, status) values ('Airbuds', 'Suspendisse potenti.', 0);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categories");
        }
    }
}
