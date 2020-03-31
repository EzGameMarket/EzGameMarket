using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemRequirement.API.Migrations
{
    public partial class InitSysReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPUNeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMDType = table.Column<string>(nullable: false),
                    IntelType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUNeeds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GPUNeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AMDType = table.Column<string>(nullable: false),
                    NVIDIAType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPUNeeds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NetworkNeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bandwidth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkNeeds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RAMNeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMNeeds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StorageNeeds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageNeeds", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SysReqs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    CPUID = table.Column<int>(nullable: false),
                    RAMID = table.Column<int>(nullable: false),
                    GPUID = table.Column<int>(nullable: false),
                    StorageID = table.Column<int>(nullable: false),
                    NetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysReqs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SysReqs_CPUNeeds_CPUID",
                        column: x => x.CPUID,
                        principalTable: "CPUNeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysReqs_GPUNeeds_GPUID",
                        column: x => x.GPUID,
                        principalTable: "GPUNeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysReqs_NetworkNeeds_NetworkID",
                        column: x => x.NetworkID,
                        principalTable: "NetworkNeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysReqs_RAMNeeds_RAMID",
                        column: x => x.RAMID,
                        principalTable: "RAMNeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysReqs_StorageNeeds_StorageID",
                        column: x => x.StorageID,
                        principalTable: "StorageNeeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysReqs_CPUID",
                table: "SysReqs",
                column: "CPUID");

            migrationBuilder.CreateIndex(
                name: "IX_SysReqs_GPUID",
                table: "SysReqs",
                column: "GPUID");

            migrationBuilder.CreateIndex(
                name: "IX_SysReqs_NetworkID",
                table: "SysReqs",
                column: "NetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_SysReqs_RAMID",
                table: "SysReqs",
                column: "RAMID");

            migrationBuilder.CreateIndex(
                name: "IX_SysReqs_StorageID",
                table: "SysReqs",
                column: "StorageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysReqs");

            migrationBuilder.DropTable(
                name: "CPUNeeds");

            migrationBuilder.DropTable(
                name: "GPUNeeds");

            migrationBuilder.DropTable(
                name: "NetworkNeeds");

            migrationBuilder.DropTable(
                name: "RAMNeeds");

            migrationBuilder.DropTable(
                name: "StorageNeeds");
        }
    }
}
