# AzureBlobProject

# Azurite Local Emulator Setup

This project uses [Azurite](https://github.com/Azure/Azurite), a local emulator for Azure Blob, Queue, and Table storage.

## 📦 Prerequisites

- [Node.js](https://nodejs.org/) (v14 or later)
- npm (Node Package Manager) install in pc

# Open CMD and Run Command Step By Step
1. Install Azurite Globally
   npm install -g azurite

2. Create Data Directory
   mkdir azurite-data

3. Create Data Directory
  mkdir azurite-data

5. Run Azurite
  azurite --location ./azurite-data --debug ./azurite-data/debug.log --skipApiVersionCheck

Azurite will start on:

Blob Service: http://127.0.0.1:10000

Queue Service: http://127.0.0.1:10001

Table Service: http://127.0.0.1:10002

📂 Directory Structure
.
├── azurite-data/        # Azurite data and logs
├── .env                 # Optional environment config
├── package.json         # Project metadata (if using npm)
└── README.md            # You're here!

note: local Connection String For Azure is "UseDevelopmentStorage=true" Replace this Connectionsting in Appsettings.

## 🚀 Getting Started

