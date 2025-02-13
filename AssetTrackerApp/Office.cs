using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetTrackerApp { 
public class Office
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string Currency { get; set; }
    public decimal ExchangeRate { get; set; } // Conversion from USD
    public List<Asset> Assets { get; set; }
}
}
