namespace vAirline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airports",
                c => new
                    {
                        AirportID = c.Int(nullable: false, identity: true),
                        AirportName = c.String(nullable: false, maxLength: 250),
                        AirportCode = c.String(nullable: false, maxLength: 50),
                        AirportCountry = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.AirportID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingID = c.Int(nullable: false, identity: true),
                        BookingDateTime = c.DateTime(nullable: false),
                        BookingPrice = c.Double(nullable: false),
                        PassengerID = c.Int(),
                        FlightID = c.Int(),
                        SeatID = c.Int(),
                    })
                .PrimaryKey(t => t.BookingID)
                .ForeignKey("dbo.Flights", t => t.FlightID)
                .ForeignKey("dbo.Passengers", t => t.PassengerID)
                .ForeignKey("dbo.Seats", t => t.SeatID)
                .Index(t => t.PassengerID)
                .Index(t => t.FlightID)
                .Index(t => t.SeatID);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        FlightCode = c.String(maxLength: 50),
                        DepartureAirportCode = c.String(nullable: false, maxLength: 50),
                        DepartureDateTime = c.DateTime(nullable: false),
                        ArrivalAirportCode = c.String(nullable: false, maxLength: 50),
                        ArrivalDateTime = c.DateTime(nullable: false),
                        PricePerSeat = c.Double(nullable: false),
                        LeftSeat = c.Int(nullable: false),
                        MiddleSeat = c.Int(nullable: false),
                        RightSeat = c.Int(nullable: false),
                        TotalSeatsAvailable = c.Int(nullable: false),
                        PlaneID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Planes", t => t.PlaneID, cascadeDelete: true)
                .Index(t => t.PlaneID);
            
            CreateTable(
                "dbo.Planes",
                c => new
                    {
                        PlaneID = c.Int(nullable: false, identity: true),
                        PlaneModel = c.String(nullable: false, maxLength: 50),
                        LeftSeat = c.Int(nullable: false),
                        MiddleSeat = c.Int(nullable: false),
                        RightSeat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlaneID);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatNumber = c.Int(nullable: false),
                        SeatLocation = c.String(nullable: false, maxLength: 50),
                        SeatAvailable = c.Boolean(nullable: false),
                        FlightID = c.Int(),
                        PassengerID = c.Int(),
                    })
                .PrimaryKey(t => t.SeatID)
                .ForeignKey("dbo.Flights", t => t.FlightID)
                .ForeignKey("dbo.Passengers", t => t.PassengerID)
                .Index(t => t.FlightID)
                .Index(t => t.PassengerID);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        PassengerID = c.Int(nullable: false, identity: true),
                        PassengerUserName = c.String(nullable: false, maxLength: 100),
                        PassengerFullName = c.String(nullable: false, maxLength: 250),
                        PassengerPassportNo = c.String(nullable: false, maxLength: 250),
                        PassengerAge = c.Int(nullable: false),
                        PassengerGender = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.PassengerID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookings", "SeatID", "dbo.Seats");
            DropForeignKey("dbo.Seats", "PassengerID", "dbo.Passengers");
            DropForeignKey("dbo.Bookings", "PassengerID", "dbo.Passengers");
            DropForeignKey("dbo.Seats", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.Flights", "PlaneID", "dbo.Planes");
            DropForeignKey("dbo.Bookings", "FlightID", "dbo.Flights");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Seats", new[] { "PassengerID" });
            DropIndex("dbo.Seats", new[] { "FlightID" });
            DropIndex("dbo.Flights", new[] { "PlaneID" });
            DropIndex("dbo.Bookings", new[] { "SeatID" });
            DropIndex("dbo.Bookings", new[] { "FlightID" });
            DropIndex("dbo.Bookings", new[] { "PassengerID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Passengers");
            DropTable("dbo.Seats");
            DropTable("dbo.Planes");
            DropTable("dbo.Flights");
            DropTable("dbo.Bookings");
            DropTable("dbo.Airports");
        }
    }
}
