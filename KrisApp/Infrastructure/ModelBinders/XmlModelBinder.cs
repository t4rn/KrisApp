using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace KrisApp.Infrastructure.ModelBinders
{
    public class XmlModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                // expected type in controller's action
                Type modelType = bindingContext.ModelType;
                XmlSerializer serializer = new XmlSerializer(modelType);

                Stream inputStream = controllerContext.HttpContext.Request.InputStream;

                // no validation against DataAnnotiations on model
                return serializer.Deserialize(inputStream);
            }
            catch
            {
                bindingContext.ModelState.AddModelError("", "The item couldn't be serialized.");
                return null;
            }
        }
    }
}