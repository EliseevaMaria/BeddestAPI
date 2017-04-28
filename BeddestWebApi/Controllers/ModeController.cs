using BusinessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BeddestWebApi.Controllers
{
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
        public void AddMode(int userId, int bedId, string name)
        {
            CtrlToDal.AddModeForIotAsync(userId, bedId, name);
        }

        [HttpGet]
        // [HttpPut]
        public void SelectMode(int modeId, int bedId)
        {
            CtrlToDal.SelectModeForIotAsync(modeId, bedId);
        }
    }
}
