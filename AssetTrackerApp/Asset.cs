using System;
using Microsoft.EntityFrameworkCore;
namespace AssetTrackerApp { 
public abstract class Asset
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal PriceUSD { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }

    public abstract string GetAssetType();

        /*public bool IsNearEndOfLife(out string warningColor)
        {
            var age = (DateTime.Now - PurchaseDate).TotalDays / 365;
            if (age >= 2.75 && age < 3)
            {
                warningColor = "Yellow";
                return true;
            }
            if (age >= 3)
            {
                warningColor = "Red";
                return true;
            }
            warningColor = "None";
            return false;
        } */
        public bool IsNearEndOfLife(out string warningColor)
        {
            var age = (DateTime.Now - PurchaseDate).TotalDays / 365;

            if (age >= 2.75 && age < 3) // Yello
            {
                warningColor = "Yellow";
                return true;
            }
            if (age >= 3) //  Red
            {
                warningColor = "Red";
                return true;
            }

            warningColor = "None";
            return false;
        }
    }
}