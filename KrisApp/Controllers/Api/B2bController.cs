using KrisApp.Models.Calc;
using System.Web.Http;

namespace KrisApp.Controllers.Api
{
    public class B2bController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("OK from API");
        }

        [HttpPost]
        public IHttpActionResult Post(B2bAmountModel model)
        {
            decimal result = (model.NettoAmount - (model.ZdroAmount + model.SpolAmount)) * 0.81M;
            //System.Threading.Thread.Sleep(1000);

            return Ok(result);
        }

        //[HttpPost]
        //public IHttpActionResult Post(B2bAmountModel model)
        //{
        //    decimal podstawa = model.NettoAmount - model.SpolAmount;
        //    decimal podatek = podstawa * 0.19M;

        //    decimal result = model.NettoAmount - podatek - model.SpolAmount - model.ZdroAmount;

        //    return Ok(result);
        //}
    }
}
