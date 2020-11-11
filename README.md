# dotnet-bakery

This is the starting point for teaching an intro to .NET Core WebAPI project. The models and controllers are stubbed out but empty.

## Lecture Outline

  - Have students do some research on their own around some of the upcoming topics: database migrations, entity framework, code-first database design, dbContext, .NET package manager, enumerated types, etc. Students report back what they find.
  - Outline the overall flow of what we're going to work on. Define the high level requirements:
    - Manage bread inventory for a bakery. Bread can have a bread type, baker_id, baked time, etc.
    - Bread can be baked by a Baker. Bakers can have a name.
    - The React App is already done for us. It allows us to bake bread, add bakers, etc.
  - Get a feel for the project:
    - Postgres is set up already. Look at the Startup.cs file for details.
    - Models/ folder has all your models. Models need to be hooked into the dbContext to work
    - Controllers/ folder has your controllers. Dont forget to restart dotnet if you add controllers or models!
    - Migrations/ folder has all your migrations. Its OK to reset the DB and start over.
    - ClientApp/ has the react app. We've updated the repo so that you must run the client app separately (npm start)
    - Startup.cs has been updated to proxy our view to localhost:3000 -- you can go the other way too with webpack's proxy!
