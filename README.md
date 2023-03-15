ShiipingAPI: 

1) Used belwow service
- .NET core 6.0
- SQL Server

2) Change the SQL connection string inside of "appconfig.json" file:
- Like:  
  "ConnectionStrings": {
    "ShiipingAPIContext": "Server=(localdb)\\mssqllocaldb;Database=ShiipingAPI.Data;Trusted_Connection=True;MultipleActiveResultSets=true"
  }

3) Database migration as below:
- Enter below commnand in package manager for the database migration:
    PM> Update-Database


4) Selcect Shiiping API and run the application. Application open the default Swagger API page.  Where you can test the api of Ship.
- Ref swagger link: https://localhost:7180/swagger/index.html
- As per below API of Ships:
    a)
        GET
        ​/api​/Ships
        Get Ship List.

    b) 
        POST
        ​/api​/Ships
        Create Ship.

    c) 
        GET
        ​/api​/Ships​/{id}
        Get Ship Details.

    d) 
        PUT
        ​/api​/Ships​/{id}
        Update Ship Velocity

    e) 
        DELETE
        ​/api​/Ships​/{id}
        Delete Ship.

5) Only for ref: 
 - Database seeding is auto in development if records not available in Port and Ship tables as peer below:
- Only for the reference:  
    new Port
    {
        Name = "Mumbai Port",
        Description = "Mumbai, India",
        Latitude = 19.298350,
        Longitude = 72.870584,
        Status = 1
    },

    new Port
    {
        Name = "Singapore Port",
        Description = "Singapore",
        Latitude = 1.268087,
        Longitude = 103.841283,
        Status = 1
    },

    new Port
    {
        Name = "South Korea Port",
        Description = "South Korea",
        Latitude = 34.274665,
        Longitude = 126.639220,
        Status = 1
    },

    new Port
    {
        Name = "Mumbai Port",
        Description = "Japan",
        Latitude = 35.453730,
        Longitude = 140.830014,
        Status = 1
    }

-   new Ship
    {
        Name = "Ship-1",
        Description = "Near by Sri Lanka",
        Latitude = 4.730407,
        Longitude = 77.477244,
        Velocity = 30,
        Status = 1
    },

    new Ship
    {
        Name = "Ship-2",
        Description = "Near by Malasia",
        Latitude = 6.778805,
        Longitude = 97.0366607,
        Velocity = 40,
        Status = 1
    },

    new Ship
    {
        Name = "Ship-3",
        Description = "South Taiwan",
        Latitude = 23.796708,
        Longitude = 123.639633,
        Velocity = 45,
        Status = 1
    },

    new Ship
    {
        Name = "Ship-4",
        Description = "Near by Japan",
        Latitude = 41.422623,
        Longitude = 148.508807,
        Velocity = 50,
        Status = 1
    }
                
 


