# ProductCategorySolution 🎉

ProductCategorySolution three projects which are 
* Infrastructure (contains models about connection database, cache etc.)
* Service (contains business models where presentation and infrastructure projects meet.)
* Presentation (contains endpoint where users connects the application.)

Some features of the solution ✨

* Project uses MongoDB as database. (You can configure your MongoDB connection string in appsettings.json under Presentation.csproj)
* Project uses Redis for caching. (You can configure your Redis endpoint in appsettings.json under Presentation.csproj)
* Healthcheck of system can be accessed with _/hc_ path.
* API has swagger documentation.

What am I going to do next ? 🚧

* I am going to add JWT authentication to API.
* I am gonna add an exception middleware to system.
* I am going to record exception logs in a text file.
* I am going to create an unit test project.

## Diagrams 📸

![Screenshot_2](https://user-images.githubusercontent.com/47561392/135548140-62e9c222-01db-421f-a084-24988144b828.png)
