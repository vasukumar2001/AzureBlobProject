# AzureBlobProject

# Azurite Local Emulator Setup

This project uses [Azurite](https://github.com/Azure/Azurite), a local emulator for Azure Blob, Queue, and Table storage.

## 📦 Prerequisites

- [Node.js](https://nodejs.org/) (v14 or later)
- npm (Node Package Manager) install in pc

# Open CMD and Run Command Step By Step
1. Install Azurite Globally

    ```bash
    npm install -g azurite
    ```

3. Create Data Directory

   ```bash
    mkdir azurite-data
   ```
   
5. Create Data Directory

   ```bash
    mkdir azurite-data
   ```
  
7. Run Azurite

   ```bash
    azurite --location ./azurite-data --debug ./azurite-data/debug.log --skipApiVersionCheck
   ```
  
#Azurite will start on:

Blob Service: http://127.0.0.1:10000

Queue Service: http://127.0.0.1:10001

Table Service: http://127.0.0.1:10002

📂 Directory Structure

.
├── azurite-data/        # Azurite data and logs

├── .env                 # Optional environment config

├── package.json         # Project metadata (if using npm)

└── README.md            # You're here!


## Projects Overview

### 1. TangyAzureFunc
- **Type:** Azure Functions (.NET 8)
- **Purpose:** Handles blob triggers, queue triggers, and updates the database when blobs are uploaded or processed.
- **Key Features:**
  - Blob-triggered functions for image processing and status updates.
  - Queue-triggered functions for database updates.
  - API endpoints for grocery item CRUD operations.

### 2. AzureFunctionTangyWeb
- **Type:** ASP.NET Core Razor Pages (.NET 8)
- **Purpose:** Web frontend for managing grocery items.
- **Key Features:**
  - List, create, edit, and delete grocery items.
  - Communicates with the Azure Functions API via HTTP.
  - Uses `IHttpClientFactory` for API calls.

### 3. AzureBlobProject
- **Type:** ASP.NET Core MVC (.NET 8)
- **Purpose:** Handles Blob Storage operations.
- **Key Features:**
  - Upload, list, and manage blobs.
  - Uses `BlobService` for Azure Blob Storage integration.
  - Connection string configured in `appsettings.json`.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Storage Emulator](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-emulator) or [Azurite](https://github.com/Azure/Azurite) for local development
- Visual Studio 2022 or later

## Configuration
### AzureBlobProject/appsettings.json
"BlobConnectionStrings": "UseDevelopmentStorage=true"
- For local development, this uses the Azure Storage Emulator.
- For production, replace with your Azure Storage connection string.

## How to Run

## Database Setup

Before running the solution, you need to create the required tables in your SQL Server database.

> **Note:**  
> You must create these tables in your database before running the solution.  
> Set your Azure SQL Database connection string in `TangyAzureFunc/local.settings.json` under the appropriate key (e.g., `"AzureSqlDatabase"`):

### Table Scripts
#### SalesRequests Table
```sql
 CREATE TABLE [dbo].[SalesRequests]
(
    [Id] [varchar](500) NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [Email] [nvarchar](max) NOT NULL,
    [Phone] [nvarchar](max) NOT NULL,
    [Status] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_SalesRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
)
ON [PRIMARY] 
TEXTIMAGE_ON [PRIMARY];
```
#### GroceryItems Table
```sql
    CREATE TABLE [dbo].[GroceryItems](
        [Id] [nvarchar](450) NOT NULL,
        [Name] [nvarchar](max) NOT NULL,
        [CreatedDate] [datetime],
        CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
```
### 1. Start Azure Storage Emulator or Azurite
- Ensure local storage is running for blob operations.

### 2. Build the Solution
### 3. Run Azure Functions (TangyAzureFunc)
Or use Visual Studio to start the function project.

### 4. Run Web Projects
- **AzureFunctionTangyWeb (Razor Pages):**
- **AzureBlobProject (MVC):**


### 5. Access the Applications
- **Razor Pages Web App:** [http://localhost:5000](http://localhost:5000) (or your configured port)
- **Blob Storage Web App:** [http://localhost:5001](http://localhost:5001)
- **Azure Functions API:** [http://localhost:7139/api/GroceryList](http://localhost:7139/api/GroceryList) (or your configured port)

## Usage

- Use the Razor Pages app to manage grocery items (create, edit, delete, list).
- Blob uploads and triggers are handled by the Azure Functions project.
- Blob storage operations are managed in the MVC project.

## Technologies Used

- .NET 8
- ASP.NET Core Razor Pages
- ASP.NET Core MVC
- Azure Functions
- Azure Blob Storage
- Entity Framework Core
- Newtonsoft.Json

## Notes

- Ensure all connection strings and ports are correctly configured in `appsettings.json` and launch settings.
- For production, update storage connection strings and API URLs as needed.

---

## 🚀 Getting Started

