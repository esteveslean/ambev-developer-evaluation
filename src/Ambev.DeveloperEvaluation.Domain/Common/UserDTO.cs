namespace Ambev.DeveloperEvaluation.Domain.Common;

public class NameDTO
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
}

public class AddressDTO
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Zipcode { get; set; } = string.Empty;
    public GeolocationDTO? Geolocation { get; set; }
}

public class GeolocationDTO
{
    public string Lat { get; set; } = string.Empty;
    public string Long { get; set; } = string.Empty;
}