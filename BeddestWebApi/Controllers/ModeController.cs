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
    public class ModeController : ApiController
    {
        [HttpGet]
        public List<ModeDTO> GetModes(int userId)
        {
            List<ModeDTO> modes = CtrlToDal.GetModes(userId);
            return modes;
        }

        [HttpGet]
        // [HttpPost]
        public async Task AddMode(int userId, int bedId, string name)
        {
            await CtrlToDal.AddModeForIotAsync(userId, bedId, name);
        }

        [HttpGet]
        // [HttpPut]
        public async Task SelectMode(int modeId, int bedId)
        {
            await CtrlToDal.SelectModeForIotAsync(modeId, bedId);
        }

        [HttpGet]
        // [HttpPut]
        public void SetModeForDb(int modeId, int bedId, int head, int legs)
        {
            CtrlToDal.SelectModeForDB(modeId, bedId, head, legs);
        }
    }
}
