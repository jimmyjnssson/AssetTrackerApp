using Microsoft.EntityFrameworkCore;

namespace AssetTrackerApp { 
public class Phone : Asset
{
    public override string GetAssetType() => "Phone";
}
public class Iphone : Phone { }
public class Samsung : Phone { }
public class Nokia : Phone { }
public class GooglePixel : Phone { }
public class OnePlus : Phone { }
}