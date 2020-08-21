namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using Microsoft.AspNetCore.Mvc;
    using Service;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the <see cref="XmlExceptionContentType" />.
    /// </summary>
    public class XmlExceptionContentType : AbstractExceptionContentType
    {
        /// <summary>
        /// The CreateExceptionResponse.
        /// </summary>
        /// <param name="problemDetails">The problemDetails<see cref="ProblemDetails"/>.</param>
        /// <returns>The <see cref="ExceptionResponse"/>.</returns>
        public override ExceptionResponse CreateExceptionResponse(ProblemDetails problemDetails)
        {
            string stringWriter;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProblemDetails));
            using (StringWriter writer = new StringWriter())
            {
                xmlSerializer.Serialize(writer, problemDetails);
                stringWriter = writer.ToString();
            }

            return new ExceptionResponse(SupportedContentTypes.ProblemDetailsXml, stringWriter);
        }
    }
}
