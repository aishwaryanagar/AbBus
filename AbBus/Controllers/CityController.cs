using AbBus.BAL;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace AbBus.Controllers
{
    [JWTAuthenticationFilter]
    [System.Web.Mvc.Route("api/[controller]")]
    public class CityController : ApiController
    {
        private readonly CityManagement _cityManagement;
        CityController()
        {
            _cityManagement = new CityManagement();
        }

        [System.Web.Mvc.Route("add")]
        [System.Web.Http.HttpPost()]
        public ActionResult Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Name not entered");
            try
            {
                var isUserSignedUp = _cityManagement.Add(name);
                if (!isUserSignedUp)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "City already exists");
                return new HttpStatusCodeResult(HttpStatusCode.OK, "City added successfully");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Unable to save changes. Try again, and if the problem persists, see your system administrator." + ex.Message);
            }
        }

    }
}
