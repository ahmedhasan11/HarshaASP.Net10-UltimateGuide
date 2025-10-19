using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class sp_InsertPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_InsertPerson = @"CREATE PROCEDURE [dbo].[InsertPerson](@PersonID uniqueidentifier,@PersonName nvarchar(40)
                                      ,@EmailAddress nvarchar(40),@DateOfBirth datetime2(7),
                                       @Gender nvarchar(10),@Address nvarchar(300),@CountryID uniqueidentifier
                                       ,@ReccivenewsLetters bit)
                                      AS BEGIN
                                        INSERT INTO [dbo].[Persons](PersonID,PersonName,EmailAddress,DateOfBirth,
                                       Gender,Address,CountryID,ReccivenewsLetters)
                                        VALUES (@PersonID,@PersonName,@EmailAddress,@DateOfBirth,
                                       @Gender,@Address,@CountryID,@ReccivenewsLetters)
                                      END";

            migrationBuilder.Sql(sp_InsertPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			string sp_InsertPerson = @"DROP PROCEDURE [dbo].[InsertPerson](@PersonID,@PersonName,@EmailAddress,@DateOfBirth,
                                       @Gender,@Address,@CountryID,@ReccivenewsLetters)
                                      AS BEGIN
                                        INSERT INTO [dbo].[Persons](PersonID,PersonName,EmailAddress,DateOfBirth,
                                       Gender,Address,CountryID,ReccivenewsLetters)
                                        VALUES (@PersonID,@PersonName,@EmailAddress,@DateOfBirth,
                                       @Gender,@Address,@CountryID,@ReccivenewsLetters)
                                      END";

			migrationBuilder.Sql(sp_InsertPerson);
		}
    }
}
