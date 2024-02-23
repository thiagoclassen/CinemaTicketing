using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "movie");

            migrationBuilder.EnsureSchema(
                name: "booking");

            migrationBuilder.EnsureSchema(
                name: "theater");

            migrationBuilder.EnsureSchema(
                name: "screening");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                schema: "theater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                schema: "movie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenresId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "movie",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "movie",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "theater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Rows = table.Column<int>(type: "int", nullable: false),
                    Columns = table.Column<int>(type: "int", nullable: false),
                    TheaterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "theater",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                schema: "screening",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "movie",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Screenings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "theater",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                schema: "theater",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "theater",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    ReservedUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreeningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalSchema: "screening",
                        principalTable: "Screenings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatReservations",
                schema: "booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "booking",
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatReservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalSchema: "theater",
                        principalTable: "Seats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MovieId",
                schema: "movie",
                table: "GenreMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScreeningId",
                schema: "booking",
                table: "Reservations",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                schema: "booking",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TheaterId",
                schema: "theater",
                table: "Rooms",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                schema: "screening",
                table: "Screenings",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_RoomId",
                schema: "screening",
                table: "Screenings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_ReservationId",
                schema: "booking",
                table: "SeatReservations",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatReservations_SeatId",
                schema: "booking",
                table: "SeatReservations",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RoomId",
                schema: "theater",
                table: "Seats",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovie",
                schema: "movie");

            migrationBuilder.DropTable(
                name: "SeatReservations",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "movie");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "booking");

            migrationBuilder.DropTable(
                name: "Seats",
                schema: "theater");

            migrationBuilder.DropTable(
                name: "Screenings",
                schema: "screening");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "movie");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "theater");

            migrationBuilder.DropTable(
                name: "Theaters",
                schema: "theater");
        }
    }
}
