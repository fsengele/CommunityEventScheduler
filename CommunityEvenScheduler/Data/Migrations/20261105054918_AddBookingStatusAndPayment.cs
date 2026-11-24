
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityEventScheduler.Data.Migrations
{
    public partial class AddBookingStatusAndPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Limit the organizer name to 50 characters.
            migrationBuilder.AlterColumn<string>(
                name: "Organizer",
                table: "Events",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Limit the description to 500 characters.
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Save the UTC date for existing bookings.
            migrationBuilder.AddColumn<DateTime>(
                name: "BookingCreatedAt",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            // New bookings start with Pending status.
            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending");

            // New bookings start with Unpaid status.
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Unpaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the booking date column.
            migrationBuilder.DropColumn(
                name: "BookingCreatedAt",
                table: "Events");

            // Remove the booking status column.
            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Events");

            // Remove the payment status column.
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Events");

            // Restore the original organizer column.
            migrationBuilder.AlterColumn<string>(
                name: "Organizer",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // Restore the original description column.
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
