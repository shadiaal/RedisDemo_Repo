# CachingAndDynamicUI

This project demonstrates how to use caching (Redis and Memory Cache) in web applications, with a dynamic user interface for displaying data based on the selected cache type. The project uses ASP.NET Core to provide an API that interacts with the frontend using JavaScript.

## Description
In this project, users can select the cache type (Redis or Memory Cache) from the frontend, and user data will be fetched based on the selected option. The data is fetched from the application's API.

### Features
- **Data Caching**: Uses **Redis** or **Memory Cache** for temporary data storage to enhance the application's performance.
- **Dynamic UI**: The frontend allows users to choose the cache type and display user data accordingly.
- **Performance Enhancement**: Caching speeds up data retrieval, avoiding repeated database queries.


## How to Run the Project
### 1. Setting Up the Environment:
- Make sure .NET 8.0 or higher is installed on your machine.
- If using **Redis** as a cache, ensure it's installed and running locally.

### 2. Running the Project:
- Open the project using **Visual Studio** or **VS Code**.
- Build the project using the following command:
    ```bash
    dotnet build
    ```
- To run the project locally:
    ```bash
    dotnet run
    ```

### 3. Accessing the UI:
- After running the application, open your browser and go to `https://localhost:7135/` to access the user interface.
- You can choose the cache type (Memory or Redis) via the buttons provided on the page.

## Project Structure
- `User.html`: Page to display user information based on the selected cache type.
- `Image.html`: Page to display images using Lazy Loading techniques.
- `Program.cs`: Main entry point for the project.


