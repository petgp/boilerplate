using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boilerplate.Migrations
{
  public partial class Testing : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      Down(migrationBuilder);
      migrationBuilder.CreateTable(
          name: "Breeds",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            Title = table.Column<string>(nullable: true),
            PetId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Breeds", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Listings",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            Date = table.Column<string>(nullable: true),
            TimeoutDate = table.Column<string>(nullable: true),
            UserId = table.Column<string>(nullable: true),
            PetId = table.Column<int>(nullable: false),
            Title = table.Column<string>(nullable: true),
            Description = table.Column<string>(nullable: true),
            ToUserId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Listings", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Messages",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            DateSent = table.Column<string>(nullable: true),
            SenderId = table.Column<string>(nullable: true),
            RecipientId = table.Column<string>(nullable: true),
            ListingId = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Messages", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Pets",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            Owner_id = table.Column<string>(nullable: true),
            Name = table.Column<string>(nullable: true),
            Type = table.Column<string>(nullable: true),
            Img_url = table.Column<string>(nullable: true),
            Breed = table.Column<string>(nullable: true),
            Age = table.Column<int>(nullable: false),
            Ownership_length = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Pets", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Breeds");

      migrationBuilder.DropTable(
          name: "Listings");

      migrationBuilder.DropTable(
          name: "Messages");

      migrationBuilder.DropTable(
          name: "Pets");
    }
  }
}
