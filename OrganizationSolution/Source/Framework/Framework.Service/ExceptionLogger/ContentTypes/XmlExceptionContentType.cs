namespace Framework.Service.ExceptionLogger.ContentTypes
{
    using System.IO;
    using System.Xml.Serialization;
    using Microsoft.AspNetCore.Mvc;
    using Service;

    public class XmlExceptionContentType : AbstractExceptionContentType
    {
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
