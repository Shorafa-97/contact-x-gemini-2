using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsX.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_en = table.Column<string>(type: "text", nullable: false),
                    name_ar = table.Column<string>(type: "text", nullable: true),
                    entity_type = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "text", nullable: true),
                    sector = table.Column<string>(type: "text", nullable: true),
                    registration_id = table.Column<string>(type: "text", nullable: true),
                    parent_entity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    addresses = table.Column<string>(type: "jsonb", nullable: false),
                    contact_points = table.Column<string>(type: "jsonb", nullable: false),
                    profile_completeness = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.id);
                    table.ForeignKey(
                        name: "FK_Entities_Entities_parent_entity_id",
                        column: x => x.parent_entity_id,
                        principalTable: "Entities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_parent_entity_id",
                table: "Entities",
                column: "parent_entity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
