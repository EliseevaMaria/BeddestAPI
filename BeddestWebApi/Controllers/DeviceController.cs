using BusinessLayer;
using Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeddestWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class DeviceController : ApiController
    {
        [HttpGet]
        public List<DeviceDTO> GetDevices()
        {
            List<DeviceDTO> result = CtrlToDal.GetDevices();
            return result;
        }
    }
}
