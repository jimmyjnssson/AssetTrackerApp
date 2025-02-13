using Microsoft.EntityFrameworkCore;

namespace AssetTrackerApp { 
public class Laptop : Asset
{
    public override string GetAssetType() => "Laptop";
}
public class MacBook : Laptop { }
public class Asus : Laptop { }
public class Lenovo : Laptop { }
public class Dell : Laptop { }
public class HP : Laptop { }

}