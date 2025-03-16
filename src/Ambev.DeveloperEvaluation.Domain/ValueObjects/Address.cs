namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents a user's address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        public string Street { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the street number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode.
        /// </summary>
        public string Zipcode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the geolocation information.
        /// </summary>
        public Geolocation Geolocation { get; set; } = new Geolocation();
    }
}
