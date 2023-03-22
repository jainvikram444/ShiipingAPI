ShiipingAPI: 

1) Used belwow service
    - .NET core 6.0
    - SQL Server

2) Change the SQL connection string set n secrets.json file or set as per below:
- Like:  
        <Profile_Path>/secrets.json:

        {
            "SqlConnectingString": "Server=(localdb)\\mssqllocaldb;Database=ShiipingAPI.Data;Trusted_Connection=True;MultipleActiveResultSets=true"
        }

        Or

         - Set in command prompt in Windows:
        > dotnet user-secrets list --project "<Project Path>\ShiipingAPI\ShiipingAPI.csproj"
            => List of all secrets.
        > dotnet user-secrets set SqlConnectingString "Server=(localdb)\mssqllocaldb;Database=ShiipingAPI.Data;Trusted_Connection=True;MultipleActiveResultSets=true" --project "<Project Path>\ShiipingAPI\ShiipingAPI.csproj"
            => Set the SqlConnectingString string in secrets.

        Note: Change the SQL connection string and Project Path as per your location.
        Ref: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows

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
        
5) Test Project download from "https://github.com/jainvikram444/ShiipingAPI.Tests/tree/master" and add current solution. Update projet dependecies as per project location.

6) CI: Integrated with the solution in GIT Action for the build and test.
    - It's just for the demo.
    - Verify at: https://github.com/jainvikram444/ShiipingAPI/actions

7) Docker:
         - Two way is there for the running docker images:

         Solution 1:

            a) Right click on Shiiping.APi folder and select "add->Add container orchestration support". And select option as per system like Linux and Windows. 

            b) Rebuild soltion and execute below command for the build image,

            c) Update sql connection string from the appsetting.json: 
                Shiiping API(folder of APP) > docker run -it --rm -p 8080:80 ytdocker:v1

            d) wait for build image and check image name like:
            -> docker images

            e) Update the mapping port and run docker image:
                > docker run -it --rm -p 8080:80 ytdocker:v1

            7) Open the swagger api in broser as per port config:
                - http://localhost:8080/swagger/index.html

        Soltion-2:
            - Download and biuild docker image from the Shiiping folder like docker-compose.yml, appsetting.json, .dockignore, Dockerfile.

        Solution-3:
            - Download the docker imgae from:
            > docker pull jainvikram444/shiipping-api

8) Only for ref: 
 -      Database seeding it's auto deployment in development mode if records not available in the Port and Ship tables as per below:
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

    -  Ship Data:

            new Ship
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
                
        


