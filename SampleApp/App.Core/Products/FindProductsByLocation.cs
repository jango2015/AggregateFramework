namespace App.Core.Products
{
    public class FindProductsByLocation
    {
        public FindProductsByLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
