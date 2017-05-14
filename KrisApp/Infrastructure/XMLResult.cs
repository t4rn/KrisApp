using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace KrisApp.Infrastructure
{
    public class XMLResult : ActionResult
    {
        private readonly object _data;

        public XMLResult(object data)
        {
            _data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            XmlSerializer serializer = new XmlSerializer(_data.GetType());
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "text/xml";
            serializer.Serialize(response.Output, _data);
        }
    }
}