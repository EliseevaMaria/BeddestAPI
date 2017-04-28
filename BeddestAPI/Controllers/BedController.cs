using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BeddestAPI.Controllers
{
    public class BedController : ApiController
    {
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
        public BedDTO AddBed(int userId, int bedId, int blockId1, int blockId2, int blockId3, int blockId4)
        {
            int[] blockIds = { blockId1, blockId2, blockId3, blockId4 };
            int[] hardnessLevels = { 3, 3, 3, 3 };
            CtrlToDal.AddBed(userId, bedId, blockIds, hardnessLevels);
            BedDTO result = CtrlToDal.GetBed(bedId);
            return result;
        }

        [HttpGet]
        // [HttpPut]
        public void SetTemperature(int bedId, int newTemp)
        {
            CtrlToDal.SetTemp(bedId, newTemp);
        }

        [HttpGet]
        // [HttpPut]
        public void SetHeatingTime(int bedId, int newTime)
        {
            CtrlToDal.SetHeatingTime(bedId, newTime);
        }
    }
}
