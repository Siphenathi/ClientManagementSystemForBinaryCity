# ClientManagementSystemForBinaryCity

  ## This is a MVC project
  * Built with C# Asp.Net core 8

1. Clone the repo
2. Open the project first, look for .sln file inside the root folder.
3. double click and open it.
5. Restore the nuggets
6. Run database script in ClientManagementSystem.Data/DbScript/. There are two scripts, one for MsSQL n another for MySQL. If you prefer MsSql update the createRepository function in each service to use MsSql DatabaseProvider. The defaullt is MySQL.
7. Update the connection string in appsettings.json file to use the Db connection you created.
8. Run the tests and the project
