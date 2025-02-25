# Project: C# and Node.js API for Managing People

## Overview

This project consists of a **C# console application** that interacts with a **Node.js API** to manage a list of people stored in a JSON file. Users can **view, search, add, and delete** people from the list through the API.

### Features:

- **C# Console Application**: Interacts with the API via HTTP requests.
- **Node.js Express API**: Handles data storage and retrieval.
- **JSON File Storage**: Stores the list of people persistently.
- **Transliteration Support**: Converts Norwegian characters (`æ`, `ø`, `å`) to ASCII-friendly versions.

## Project Structure

```
/project-root
│── assets/
│   ├── data.json       # JSON file storing people data
│   ├── server.js       # Node.js Express API
│── src/
│   ├── Program.cs      # Main C# console application
│   ├── ApiHandler.cs   # Handles API communication
│   ├── Person.cs       # Defines the Person class
│── README.md           # Project documentation (this file)
```

## Getting Started

### 1️⃣ Setup Node.js API

#### **Install dependencies:**

Run the following command inside the project folder:

```sh
npm install express
```

#### **Start the API server:**

```sh
node assets/server.js
```

The API will run at `http://localhost:3000`.

### 2️⃣ Run the C# Console Application

#### **Build and Run the C# App:**

```sh
dotnet run
```

Follow the on-screen menu to interact with the API.

## API Endpoints

### 📌 **Get All People**

```http
GET /people
```

Response:

```json
[{ "id": 1, "name": "Ola", "age": 30, "city": "Oslo", "country": "Norway" }]
```

### 🔍 **Get a Specific Person by ID**

```http
GET /people/:id
```

Example:

```http
GET /people/1
```

Response:

```json
{ "id": 1, "name": "Ola", "age": 30, "city": "Oslo", "country": "Norway" }
```

### ➕ **Add a New Person**

```http
POST /people
```

Example Request Body:

```json
{
  "name": "Kari",
  "age": 25,
  "city": "Bergen",
  "country": "Norway"
}
```

Response:

```json
{ "id": 2, "name": "Kari", "age": 25, "city": "Bergen", "country": "Norway" }
```

### ❌ **Delete a Person by ID**

```http
DELETE /people/:id
```

Example:

```http
DELETE /people/1
```

Response:

```json
{ "message": "Person med ID 1 slettet." }
```

## Known Issues & Debugging

### **1. API Returning Garbled Characters (æøå issues)**

- Ensure **UTF-8 encoding** is set in `server.js`:
  ```javascript
  res.setHeader("Content-Type", "application/json; charset=utf-8");
  ```
- Check PowerShell encoding:
  ```powershell
  [console]::OutputEncoding = [System.Text.Encoding]::UTF8
  ```

### **2. API Crashes on Startup**

- Ensure **Node.js is installed**.
- Verify `data.json` exists (`[]` if empty).
- Run:
  ```sh
  npm install express
  ```

## Contributors

- **Your Name** – Developer

---

Happy coding! 🚀
