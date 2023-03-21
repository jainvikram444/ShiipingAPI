using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiipingAPI.Migrations
{
    public partial class spGetPortNearByShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE OR ALTER PROCEDURE [dbo].[GetPortNearByShip]                     
                        @ShipId int
                        AS
                        BEGIN
                             SELECT TOP 1 S.*,  CAST(geography::Point(P.latitude, P.longitude, 4326).STDistance(CONCAT('POINT(', S.Longitude, ' ', S.Latitude, ')'))/1000 as int) as distance,
                                P.id as PortId, P.name as PortName FROM Ship as S
                                LEFT OUTER JOIN dbo.Port as P ON 1=1
                                WHERE S.ID=@ShipId and P.Status=1
                                ORDER BY distance ASC;
                        END
                        GO ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE IF EXISTS [dbo].[GetPortNearByShip];";
            migrationBuilder.Sql(sp);
        }
    }
}
