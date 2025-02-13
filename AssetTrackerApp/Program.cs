using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AssetTrackerApp
{
    internal class Program
    {
        static void Main()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
        .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AssetTrackerDB;Integrated Security=True")
        .Options;

            using var db = new AssetDbContext(options);
            db.Database.EnsureCreated(); // Ensure database exists

            // ✅ Seed data ONLY if the database is empty
            if (!db.Offices.Any() || !db.Assets.Any())
            {
                SeedData(db);
                Console.WriteLine(" Assets and offices added to the database.");
            }

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1 - Display Assets");
                Console.WriteLine("2 - Update an Asset");
                Console.WriteLine("3 - Delete an Asset");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("_______________________");
                Console.WriteLine("\n");


                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayAssets(db);
                        break;
                    case "2":
                        Console.Write("Enter Asset ID to Update: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.Write("Enter New Model Name: ");
                            string newModel = Console.ReadLine();
                            Console.Write("Enter New Price in USD: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            {
                                UpdateAsset(db, updateId, newModel, newPrice);
                            }
                            else Console.WriteLine("Invalid price format.");
                        }
                        else Console.WriteLine(" Invalid ID.");
                        break;

                    case "3":
                        Console.Write("Enter Asset ID to Delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            DeleteAsset(db, deleteId);
                        }
                        else Console.WriteLine(" Invalid ID.");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine(" Invalid choice. Try again.");
                        break;
                }
            }

        }

        static void SeedData(AssetDbContext db)
        {
            if (!db.Offices.Any())
            {
                db.Offices.AddRange(new List<Office>
            {
                new Office { Country = "Sweden", Currency = "SEK", ExchangeRate = 10.5M },
                new Office { Country = "Germany", Currency = "EUR", ExchangeRate = 0.92M },
                new Office { Country = "France", Currency = "EUR", ExchangeRate = 0.92M }
            });
                db.SaveChanges();
            }


            if (!db.Assets.Any()) //Comment out this IF state if you want to add more data after first migration. Temporary solution
            {
                db.Assets.AddRange(new List<Asset>
            {
                new MacBook { Model = "MacBook Pro", PurchaseDate = DateTime.Now.AddYears(-2), PriceUSD = 1500, OfficeId = 1 },
                new Asus { Model = "Asus ROG", PurchaseDate = DateTime.Now.AddYears(-1), PriceUSD = 1200, OfficeId = 2 },
                new Lenovo { Model = "ThinkPad X1", PurchaseDate = DateTime.Now.AddYears(-3).AddMonths(-2), PriceUSD = 1300, OfficeId = 3 },
                new Dell { Model = "Dell XPS 13", PurchaseDate = DateTime.Now.AddYears(-2).AddMonths(-4), PriceUSD = 1400, OfficeId = 1 },
                new HP { Model = "HP Spectre x360", PurchaseDate = DateTime.Now.AddYears(-1).AddMonths(-6), PriceUSD = 1350, OfficeId = 2 },
                new Iphone { Model = "iPhone 13", PurchaseDate = DateTime.Now.AddYears(-2).AddMonths(-3), PriceUSD = 1100, OfficeId = 3 },
                new Samsung { Model = "Galaxy S22", PurchaseDate = DateTime.Now.AddYears(-2).AddMonths(-6), PriceUSD = 900, OfficeId = 1 },
                new Nokia { Model = "Nokia 3310", PurchaseDate = DateTime.Now.AddYears(-4), PriceUSD = 50, OfficeId = 2 },
                new GooglePixel { Model = "Pixel 6", PurchaseDate = DateTime.Now.AddYears(-1).AddMonths(-2), PriceUSD = 800, OfficeId = 3 },
                new OnePlus { Model = "OnePlus 9", PurchaseDate = DateTime.Now.AddYears(-2).AddMonths(-1), PriceUSD = 750, OfficeId = 1 },
                new Lenovo { Model = "Lenovo Legion 5", PurchaseDate = DateTime.Now.AddYears(-3).AddMonths(3), PriceUSD = 1111, OfficeId = 2 },
                new Iphone { Model = "iPhone 5", PurchaseDate = DateTime.Now.AddYears(-3).AddMonths(2), PriceUSD = 900, OfficeId = 3 },
                new Iphone { Model = "iPhone 6", PurchaseDate = DateTime.Now.AddYears(-3).AddMonths(4), PriceUSD = 900, OfficeId = 3 }
             });
                db.SaveChanges();
            }

        }

        static void DisplayAssets(AssetDbContext db)
        {
            var assets = db.Assets.Include(a => a.Office).ToList()
                 .OrderBy(a => a is Laptop) // Phones first, then laptops
                 .ThenBy(a => a.Office.Country) // Then by office
                 .ThenBy(a => a.PurchaseDate) // Finally by purchase date
                 .ToList();

            foreach (var asset in assets)
            {
                asset.IsNearEndOfLife(out string color);
                decimal localPrice = asset.PriceUSD * asset.Office.ExchangeRate;
                Console.ForegroundColor = color == "Red" ? ConsoleColor.Red : color == "Yellow" ? ConsoleColor.Yellow : ConsoleColor.White;
                Console.WriteLine($"{asset.GetAssetType()} - {asset.Model} ({asset.Office.Country}) - Purchased: {asset.PurchaseDate.ToShortDateString()} - {localPrice} {asset.Office.Currency}");
            }
            Console.ResetColor();

        }

        static void UpdateAsset(AssetDbContext db, int assetId, string newModel, decimal newPrice)
        {
            var asset = db.Assets.Find(assetId);
            if (asset != null)
            {
                asset.Model = newModel;
                asset.PriceUSD = newPrice;
                db.SaveChanges();
                Console.WriteLine($" Updated Asset ID {assetId}: New Model = {newModel}, New Price = {newPrice} USD");
            }
            else
            {
                Console.WriteLine($" Asset with ID {assetId} not found.");
            }
        }

        static void DeleteAsset(AssetDbContext db, int assetId)
        {
            var asset = db.Assets.Find(assetId);
            if (asset != null)
            {
                db.Assets.Remove(asset);
                db.SaveChanges();
                Console.WriteLine($" Deleted Asset ID {assetId} ({asset.Model}).");
            }
            else
            {
                Console.WriteLine($" Asset with ID {assetId} not found.");
            }
        }
    }
}