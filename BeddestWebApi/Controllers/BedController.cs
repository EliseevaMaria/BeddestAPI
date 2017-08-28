using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BeddestWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BedController : ApiController
    {
        [HttpGet]
        public List<BedDTO> GetAllBeds()
        {
            List<BedDTO> result = CtrlToDal.GetAllBeds();
            return result;
        }

        [HttpGet]
        public List<BedDTO> GetBeds(int userId)
        {
            List<BedDTO> result = CtrlToDal.GetBeds(userId);
            return result;
        }

        [HttpGet]
        public BedDTO GetBedData(int bedId)
        {
            BedDTO result = CtrlToDal.GetBed(bedId);
            return result;
        }

        [HttpGet]
        // [HttpPost]
        public async Task AddBed(int userId, int hardness1 = 3, int hardness2 = 3, int hardness3 = 3, int hardness4 = 3)
        {
            int[] hardnessLevels = { hardness1, hardness2, hardness3, hardness4 };
            await CtrlToDal.AddBed(userId, hardnessLevels);
        }

        [HttpGet]
        public async Task RemoveBed(int bedId)
        {
            await CtrlToDal.RemoveBed(bedId);
        }

        [HttpGet]
        // [HttpPut]
        public async Task SetTemperature(int bedId, int newTemp)
        {
            await CtrlToDal.SetTemp(bedId, newTemp);
        }

        [HttpGet]
        // [HttpPut]
        public async Task SetHeatingTime(int bedId, int newTime)
        {
            await CtrlToDal.SetHeatingTime(bedId, newTime);
        }
    }
}
