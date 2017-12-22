using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;

namespace KrisApp.Tests
{
    public static class MockHelpers
    {
        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id",
                new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), 10, true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }

        public static IHttpActionResult CallWithModelValidation<C, R, T>(this C controller,
            Func<C, R> action, T model)
            where C : ApiController
            where R : IHttpActionResult
            where T : class
        {
            var provider = new DataAnnotationsModelValidatorProvider();
            IEnumerable<ModelMetadata> metadata = ModelMetadataProviders.Current.GetMetadataForProperties(model, typeof(T));
            foreach (ModelMetadata modelMetadata in metadata)
            {
                IEnumerable<ModelValidator> validators = provider
                    .GetValidators(modelMetadata, new ControllerContext());
                foreach (ModelValidator validator in validators)
                {
                    IEnumerable<ModelValidationResult> results = validator.Validate(model);
                    foreach (ModelValidationResult result in results)
                        controller.ModelState.AddModelError(modelMetadata.PropertyName, result.Message);
                }
            }
            return action(controller);
        }
    }
}
