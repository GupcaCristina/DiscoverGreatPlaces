using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Places.DAL.Migrations
{
    public partial class AddedLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "EventLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    LogLevel = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLog", x => x.Id);
                });

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropTable(
                name: "EventLog");

        
        }
    }
}
