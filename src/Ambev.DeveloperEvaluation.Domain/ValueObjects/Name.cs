namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents a user's name.
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;
    }
}
