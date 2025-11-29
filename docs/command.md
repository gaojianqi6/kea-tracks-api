# 🛠️ KeaTracks: Command Cheat Sheet & Explanations

This document tracks every command used to build the **KeaTracks.API** backend, explaining the purpose behind each step.

---

## 1. Project Initialization

### 📂 Create the Folder Structure
```bash
mkdir kea-tracks-api
cd kea-tracks-api
git init
dotnet new gitignore
```
**Why?**
*   `mkdir`: Creates the root directory.
*   `git init`: Starts version control so we can save history.
*   `dotnet new gitignore`: Generates a special file that tells Git to ignore junk files (like `bin/`, `obj/`, and `node_modules/`), keeping the repo clean.

### 🔨 Create the .NET API Project
```bash
dotnet new webapi -n KeaTracks.API
```
**Why?**
*   `dotnet new`: The standard command to create new projects.
*   `webapi`: Tells .NET we want a **REST API** (No frontend views, just JSON endpoints).
*   `-n KeaTracks.API`: Names the project using **PascalCase** (C# Standard).

### 🔗 Solution File Setup (Optional but Professional)
```bash
dotnet new sln -n KeaTracks
dotnet sln add KeaTracks.API/KeaTracks.API.csproj
```
**Why?**
*   `.sln` (Solution) files act as a "Container" for multiple projects.
*   Even though we only have one API now, later we might add a `KeaTracks.Tests` project. The Solution file links them all together for Visual Studio.

---

## 2. Installing Libraries (NuGet Packages)

*Run these inside the `KeaTracks.API` folder.*

### 🌉 Database Drivers
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```
**Why?**
*   By default, .NET speaks C#. PostgreSQL speaks SQL.
*   This package is the **Translator (Driver)**. It allows Entity Framework to connect specifically to a Postgres database.

### 🛠️ Database Design Tools
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```
**Why?**
*   This installs the logic needed to look at your C# code and generate SQL scripts. Without this, you cannot run `dotnet ef migrations`.

### 📄 API Documentation (Swagger)
```bash
dotnet add package Swashbuckle.AspNetCore
```
**Why?**
*   This generates the **Swagger UI** (the page at `/swagger`).
*   It scans your code for API endpoints and creates a visual testing website automatically.

---

## 3. Database Infrastructure (PostgreSQL)

*Run these in your terminal to talk directly to the Postgres Server on your Mac.*

### 🐘 Create the Database
```bash
psql postgres
CREATE DATABASE kea_tracks_db;
\q
```
**Why?**
*   Your code needs a "bucket" to store data.
*   `psql`: Logs you into the database engine.
*   `CREATE DATABASE`: Allocates the storage space on your hard drive.

---

## 4. Entity Framework (EF Core) Migrations

*This is the "Code-First" workflow.*

### 🧰 Install the Global Tool
```bash
dotnet tool install --global dotnet-ef
```
**Why?**
*   The `dotnet ef` command is not installed on your Mac by default. This downloads the command-line tool needed to run migrations.

### 📝 Create the Migration Script
```bash
dotnet ef migrations add InitialCreate
```
**Why?**
*   **The Check:** It looks at your `Track.cs` file.
*   **The Compare:** It looks at your Database (which is currently empty).
*   **The Plan:** It writes a C# script (in the `Migrations` folder) containing the instructions to build the `Tracks` table.

### 🚀 Execute the Migration
```bash
dotnet ef database update
```
**Why?**
*   It takes the "Plan" from the step above and actually **runs the SQL** on the database.
*   After this command, your `Tracks` table officially exists.

---

## 5. Running the Application

### ▶️ Start the Server
```bash
dotnet watch run
```
**Why?**
*   `dotnet run`: Starts the web server (Kestrel).
*   `watch`: A special mode that "watches" your files. If you change a line of code and hit Save, it automatically restarts the server. Perfect for development.

---

## 💡 Daily Workflow Summary

When you wake up tomorrow to work on this, here is all you need to do:

1.  Open VS Code: `code .`
2.  Open Terminal (`Ctrl+~`).
3.  Enter project folder: `cd KeaTracks.API`.
4.  Start App: `dotnet watch run`.
5.  Open Browser: `http://localhost:5xxx/swagger`.
```