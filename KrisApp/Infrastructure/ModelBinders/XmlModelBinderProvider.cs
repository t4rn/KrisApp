using System;
using System.Web;
using System.Web.Mvc;

namespace KrisApp.Infrastructure.ModelBinders
{
    public class XmlModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            var contentType = HttpContext.Current.Request.ContentType.ToLower();

            if (contentType == "text/xml")
            {
                return new XmlModelBinder();
            }
            else
            {
                return null;
            }
        }
    }
}