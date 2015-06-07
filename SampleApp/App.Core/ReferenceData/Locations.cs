using System.Data.Entity.Spatial;

namespace App.Core.ReferenceData
{
    public static class Locations
    {
        public static DbGeography DtJuneau = DbGeography.FromText(
                string.Format("POLYGON(({0} {1}, {0} {2}, {3} {2}, {3} {1}, {0} {1}))",
                         -134.438184,
                         58.308787,
                         58.290704,
                         -134.393692), 4326);
    }
}
