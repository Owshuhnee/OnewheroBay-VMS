using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VMS.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEventSessionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Events_EventID",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AvailableTickets",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "events");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "events",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "events",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "events",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "events",
                newName: "event_id");

            migrationBuilder.RenameColumn(
                name: "NumberOfTickets",
                table: "Bookings",
                newName: "UserID");

            migrationBuilder.AddColumn<string>(
                name: "EventImage",
                table: "events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_events",
                table: "events",
                column: "event_id");

            migrationBuilder.CreateTable(
                name: "event_sessions",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_sessions", x => x.session_id);
                    table.ForeignKey(
                        name: "FK_event_sessions_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_event_sessions_event_id",
                table: "event_sessions",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_events_EventID",
                table: "Bookings",
                column: "EventID",
                principalTable: "events",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_events_EventID",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "event_sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_events",
                table: "events");

            migrationBuilder.DropColumn(
                name: "EventImage",
                table: "events");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "events");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "events",
                newName: "Events");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Events",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Events",
                newName: "EventName");

            migrationBuilder.RenameColumn(
                name: "event_id",
                table: "Events",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Bookings",
                newName: "NumberOfTickets");

            migrationBuilder.AddColumn<int>(
                name: "AvailableTickets",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "Events",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Events_EventID",
                table: "Bookings",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
