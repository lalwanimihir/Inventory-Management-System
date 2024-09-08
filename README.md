# Inventory Management System

The Inventory Management System is a full-stack application designed to help businesses manage their product inventory efficiently. The system provides features for employees to manage product details and for admins to update inventory status. This project utilizes ASP.NET Core Web API for the backend and React for the frontend.

## Features

Product Management: Employees can view, add, edit, and delete products.
Inventory Status Updates: Admins can update the status of the inventory.
Role-Based Access: Different functionalities are accessible based on user roles.

## Technologies Used

  1. Frontend: React
  2. Backend: ASP.NET Core Web API
  3. Database: SQL Server (via Entity Framework Core)

## Installation

 ### Prerequisites
 
    1. .NET SDK (version 6.0 or later)
    2. Node.js (version 18.x or later)
    3. SQL Server or any compatible database

## Backend Setup

  1. Clone the repository:
     
     git clone https://github.com/yourusername/inventory-management-system.git

  2. Navigate to the backend directory:

     cd inventory-management-system/Backend

  3. Restore the dependencies:

     dotnet restore

  4. Update the appsettings.json file with your database connection string.

  5. Apply database migrations:

     dotnet ef database update

  6. Start the API server:

     dotnet run

## Frontend Setup

   1. Navigate to the frontend directory:

      cd inventory-management-system/Frontend

   2. Install the dependencies:

       npm install

   3. Start the development server:

      npm start

   4. The application will be available at http://localhost:3000.


## Usage

   1. Login: Access the system with appropriate credentials for either employee or admin roles.
   2. Manage Products: Employees can view and manage products through the user interface.
   3. Update Inventory: Admins can update the inventory status via the admin dashboard.


## API Documentation

   API documentation is available at /swagger when the backend server is running. It provides details on all available endpoints and their usage.
