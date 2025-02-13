# AssetTrackerApp

## Overview
AssetTrackerApp is a console-based application for tracking company assets such as **laptops and mobile phones**. The app utilizes **Entity Framework Core** and **MS SQL Server** to store asset data, allowing users to perform **CRUD operations** (Create, Read, Update, Delete). Assets are also categorized by office locations (**Sweden, Germany, and France**) with currency conversion applied.

## Features
- **Track Assets** (Laptops & Mobile Phones) with details like purchase date, price, and office location.
- **Sort Assets** by category (Phones first, then Laptops), office, and purchase date.
- **Highlight Near End-of-Life Assets**:
  - **Red**: Less than 3 months away from 3-year lifespan.
  - **Yellow**: Less than 6 months away from 3-year lifespan.
- **CRUD Operations**:
  - **Create** assets (initial seeding happens only once).
  - **Read** and display all assets.
  - **Update** existing assets.
  - **Delete** assets from the database.
- **Multi-Currency Support**: Prices are stored in **USD** and converted based on the respective office's local currency.
- **Entity Framework Core** for database management with **MS SQL Server**.

## Technologies Used
- **.NET 8**
- **C#**
- **Entity Framework Core**
- **MS SQL Server (localdb)**

## Installation & Setup
### Prerequisites
- Install **.NET SDK 8**
- Install **MS SQL Server** or use **SQL Server LocalDB**
- Install **Entity Framework Core CLI**
  ```sh
  dotnet tool install --global dotnet-ef
  ```

### Setting Up the Database
1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-repo/AssetTrackerApp.git
   cd AssetTrackerApp
   ```

2. **Restore Dependencies**:
   ```sh
   dotnet restore
   ```

3. **Apply Migrations & Create Database**:
   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**:
   ```sh
   dotnet run
   ```
5. Could also migrate and add DB through the Package Manager Console.
   
## Usage Guide
### Main Menu Options
1. **Display Assets** (Sorted by type, office, and date)
2. **Update an Asset** (Modify model name & price)
3. **Delete an Asset** (Remove asset from DB)
4. **Exit**

### Example Output
```
Choose an option:
1 - Display Assets
2 - Update an Asset
3 - Delete an Asset
0 - Exit
> 1



## Database Schema
- **Asset**
  - Id (Primary Key)
  - Model (string)
  - PurchaseDate (DateTime)
  - PriceUSD (decimal)
  - OfficeId (Foreign Key to Office)
- **Office**
  - Id (Primary Key)
  - Country (string)
  - Currency (string)
  - ExchangeRate (decimal)



## License
MIT License

---
Developed by **Jimmy Jönsson** 🚀

