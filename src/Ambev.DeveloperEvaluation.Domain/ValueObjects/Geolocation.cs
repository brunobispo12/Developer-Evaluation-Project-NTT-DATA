namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents geolocation details.
    /// </summary>
    public class Geolocation
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public string Lat { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public string Long { get; set; } = string.Empty;
    }
}
