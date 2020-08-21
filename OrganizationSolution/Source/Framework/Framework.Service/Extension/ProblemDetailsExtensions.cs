namespace Framework.Service.Extension
{
    using Microsoft.AspNetCore.Mvc;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ProblemDetailsExtensions" />.
    /// </summary>
    public static class ProblemDetailsExtensions
    {
        /// <summary>
        /// The ToFormattedString.
        /// </summary>
        /// <param name="problemDetails">The problemDetails<see cref="ProblemDetails"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToFormattedString(this ProblemDetails problemDetails)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type: {problemDetails.Type}");
            stringBuilder.AppendLine($"Title: {problemDetails.Title}");
            stringBuilder.AppendLine($"Status: {problemDetails.Status.ToString()}");
            stringBuilder.AppendLine($"Detail: {problemDetails.Detail}");
            stringBuilder.AppendLine($"Instance: {problemDetails.Instance}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The ToFormattedString.
        /// </summary>
        /// <param name="validationProblemDetails">The validationProblemDetails<see cref="ValidationProblemDetails"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToFormattedString(this ValidationProblemDetails validationProblemDetails)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type: {validationProblemDetails.Type}");
            stringBuilder.AppendLine($"Title: {validationProblemDetails.Title}");
            stringBuilder.AppendLine($"Status: {validationProblemDetails.Status.ToString()}");
            stringBuilder.AppendLine($"Detail: {validationProblemDetails.Detail}");
            stringBuilder.AppendLine($"Instance: {validationProblemDetails.Instance}");
            stringBuilder.AppendLine($"Instance: {validationProblemDetails.Errors.ToString()}");

            return stringBuilder.ToString();
        }
    }
}
